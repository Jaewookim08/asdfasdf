using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightCheck : MonoBehaviour
{
    public float sightRadius;
    public float sightAngle;
    public GameObject parent;

    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.566f * sightRadius, transform.localScale.y, transform.localScale.z);
        layerMask = (1 << LayerMask.NameToLayer("Enemies"));
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        DrawView();
        EnemySight();
    }

    void EnemySight()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, sightRadius);
        int i = 0;

        while (i < hitColliders.Length)
        {
            GameObject foundObject = hitColliders[i].gameObject;
            if (inSightAngle(foundObject) && foundObject.tag == "Player")
            {
                //Debug.DrawLine(transform.position, foundObject.transform.position, Color.red);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, foundObject.transform.position - transform.position, sightRadius, layerMask);
                if (hit.collider.gameObject.tag == "Player")
                {
                    //Debug.Log("hit something");
                    //Debug.Log(hit.collider.gameObject.tag);
                    //Debug.Log(hit.collider.transform.position);
                    Debug.DrawRay(transform.position, hit.collider.transform.position - transform.position, Color.red);
                    //Debug.DrawLine(transform.position, foundObject.transform.position, Color.red);
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
