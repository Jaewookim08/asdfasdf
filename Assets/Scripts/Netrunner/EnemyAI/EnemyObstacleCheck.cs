using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObstacleCheck : MonoBehaviour
{
    //Transform Enemy;

    // Start is called before the first frame update
    void Start()
    {
        //Transform Enemy = gameObject.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            EnemyMovement em = gameObject.transform.parent.GetComponent<EnemyMovement>();
            em.facingWall = true;
            Debug.Log("facing wall");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            EnemyMovement em = gameObject.transform.parent.GetComponent<EnemyMovement>();
            em.facingWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            EnemyMovement em = gameObject.transform.parent.GetComponent<EnemyMovement>();
            em.facingWall = false;
            Debug.Log("get away from wall");
        }
    }
}
