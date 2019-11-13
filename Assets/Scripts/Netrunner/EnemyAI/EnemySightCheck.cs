using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightCheck : MonoBehaviour
{
    public float sightRadius;
    public float sightAngle;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.566f * sightRadius, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        EnemySight();
    }

    void EnemySight()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, sightRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            GameObject foundObject = hitColliders[i].gameObject;
            if (foundObject.tag == "Player")
            {
                Debug.Log("player detected");
            }
            i++;
        }
    }


    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

    

}
