using Netrunner.ModuleComponents;
using Netrunner.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightCheck : MonoBehaviour
{
    public float sightRadius;
    public float sightAngle;
    public float susp;
    public float suspThreshold;
    public float suspDecrease;
    public float suspMax = 100f;
    public GameObject playerInSight;
    public GameObject parent;

    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.566f * sightRadius, transform.localScale.y, transform.localScale.z);
        layerMask = (1 << LayerMask.NameToLayer("Enemies"));
        layerMask = ~layerMask;
        susp = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        DrawView();
        EnemySight();
        CheckSusp();
    }

    void EnemySight()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, sightRadius);
        int i = 0;
        playerInSight = null;

        while (i < hitColliders.Length)
        {
            GameObject foundObject = hitColliders[i].gameObject;
            if (inSightAngle(foundObject) && foundObject.tag == "Player")
            {
                //Debug.DrawLine(transform.position, foundObject.transform.position, Color.red);
                for (float tempAngle = -(sightAngle / 2); tempAngle <= (sightAngle / 2); tempAngle++) 
                {
                    Vector3 sightRay = parent.GetComponent<EnemyMovement>().right
                        ? new Vector3(Mathf.Cos(tempAngle * Mathf.Deg2Rad), -Mathf.Sin(tempAngle * Mathf.Deg2Rad), 0).normalized
                        : new Vector3(-Mathf.Cos(tempAngle * Mathf.Deg2Rad), -Mathf.Sin(tempAngle * Mathf.Deg2Rad), 0).normalized;

                    RaycastHit2D hit = Physics2D.Raycast(transform.position, sightRay, sightRadius, layerMask);
                    if (hit.collider != null && hit.collider.gameObject.tag == "Player")
                    {
                        //Debug.Log("find player");
                        Debug.DrawRay(transform.position, sightRay*sightRadius, Color.red);
                        //getAlarmfunction from player object
                        playerInSight = hit.collider.gameObject;
                        if (susp < suspMax)
                            susp += hit.collider.gameObject.GetComponent<ModuleManager>().GetSuspiciousness() * Time.deltaTime;
                        break;
                    }
                    if (hit.collider != null && hit.collider.gameObject.tag == "Can" && !hit.collider.gameObject.GetComponent<Can>().EnemyHasChecked)
                    {
                        Debug.Log("find can");
                        Debug.DrawRay(transform.position, sightRay * sightRadius, Color.red);
                        //getAlarmfunction from player object
                        playerInSight = hit.collider.gameObject;
                        hit.collider.gameObject.GetComponent<Can>().EnemyHasChecked = true;
                        if (susp < suspMax)
                            susp += suspThreshold;
                        break;
                    }
                }


            }
            i++;
        }
    }

    bool inSightAngle(GameObject target)
    {
        Vector3 targetPos = target.transform.position;
        Vector3 toTarget = (targetPos - transform.position).normalized;
        Vector3 toForward = parent.GetComponent<EnemyMovement>().right ? Vector3.right : Vector3.left;

        if (Vector3.Dot(toForward, toTarget) > Mathf.Cos((sightAngle / 2) * Mathf.Deg2Rad))
        {
            return true;
        }
        else
            return false;
    }

    void CheckSusp()
    {
        if (playerInSight != null)
        {
            if (susp > suspThreshold)
            {
                //if patrol, change to chase
                if (parent.GetComponent<EnemyMovement>().state == 0)
                {
                    parent.GetComponent<EnemyMovement>().state = 1;
                    parent.GetComponent<EnemyMovement>().chasePlayerObject = playerInSight;
                }
            }

            if (parent.GetComponent<EnemyMovement>().state == 1 && closeToAttack())
            {
                parent.GetComponent<EnemyMovement>().state = 3;
            }
        }
        else
        {
            if (susp > 0) 
                susp -= suspDecrease * Time.deltaTime;
            if (susp < 0) susp = 0;

            if (susp <= suspThreshold)
            {
                //if chase, change to return
                if (parent.GetComponent<EnemyMovement>().state == 1)
                {
                    parent.GetComponent<EnemyMovement>().state = 2;
                    parent.GetComponent<EnemyMovement>().chasePlayerObject = null;
                }

                //if return and arrive origin, change to patrol
                if (parent.GetComponent<EnemyMovement>().state == 2 && parent.transform.position.x == parent.GetComponent<EnemyMovement>().originalPos.x)
                {
                    parent.GetComponent<EnemyMovement>().state = 0;
                }
            }
        }
    }

    public bool closeToAttack()
    {
        if (playerInSight == null) 
            return false;
        if (Mathf.Abs(playerInSight.transform.position.x - parent.transform.position.x) 
            < parent.GetComponent<EnemyMovement>().AttackRange) 
            return true;
        else 
            return false;
    }

    public void DrawView()
    {
        Vector3 upEnd = parent.GetComponent<EnemyMovement>().right
            ? new Vector3(Mathf.Cos((sightAngle / 2) * Mathf.Deg2Rad), Mathf.Sin((sightAngle / 2) * Mathf.Deg2Rad), 0).normalized
            : new Vector3(-Mathf.Cos((sightAngle / 2) * Mathf.Deg2Rad), Mathf.Sin((sightAngle / 2) * Mathf.Deg2Rad), 0).normalized;
        Vector3 downEnd = parent.GetComponent<EnemyMovement>().right
    ? new Vector3(Mathf.Cos((sightAngle / 2) * Mathf.Deg2Rad), -Mathf.Sin((sightAngle / 2) * Mathf.Deg2Rad), 0).normalized
    : new Vector3(-Mathf.Cos((sightAngle / 2) * Mathf.Deg2Rad), -Mathf.Sin((sightAngle / 2) * Mathf.Deg2Rad), 0).normalized;
        Debug.DrawLine(transform.position, transform.position + upEnd * sightRadius, Color.blue);
        Debug.DrawLine(transform.position, transform.position + downEnd * sightRadius, Color.blue);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

    

}
