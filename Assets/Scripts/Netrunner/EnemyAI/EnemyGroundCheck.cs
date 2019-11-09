using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            em.onGround = true;
            Debug.Log("on Ground");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            EnemyMovement em = gameObject.transform.parent.GetComponent<EnemyMovement>();
            em.onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            EnemyMovement em = gameObject.transform.parent.GetComponent<EnemyMovement>();
            em.onGround = false;
            Debug.Log("get away from ground");
        }
    }
}
