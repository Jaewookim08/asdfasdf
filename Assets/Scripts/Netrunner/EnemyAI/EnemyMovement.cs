using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 기본적인 패트롤 AI
/// Enemy의 하위 오브젝트인 Dest를 순서대로 정찰합니다.
/// Dest 큐브를 복사해서 enemy가 움직일 경로를 설정할 수 있습니다.
/// 모든 Dest를 정찰하면, 처음부터 다시 정찰합니다.
/// 간단한 점프 기능이 있습니다. 아직은 좀 버벅거립니다.
/// 모든 움직이는 적은 기본적으로 생성 시 오른쪽을 바라보게 설정되어 있습니다.
/// </summary>


public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool facingWall;
    public bool onGround;
    public bool right;
    public int state;
    public GameObject chasePlayerObject;

    enum AI_State
    {Patrol, Chase, Return};


    Vector3 originalPos;
    List<Vector3> destPos;
    Vector3 currentDest;

    int iterator;
    //bool incr;
    Rigidbody2D rigid;


    void Start()
    {
        originalPos = transform.position;
        destPos = new List<Vector3>();
        destPos.Add(originalPos);

        Transform PatrolPath = transform.Find("PatrolPath");
        for (int i = 0; i < PatrolPath.childCount; i++)
        {
            Transform dest = PatrolPath.GetChild(i);
            destPos.Add(dest.position);
            dest.gameObject.SetActive(false);
        }

        currentDest = destPos[0];
        iterator = 0;
        //incr = true;

        right = true;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        state = (int)AI_State.Patrol;
    }

    void Update()
    {
        switch (state)
        {
            case ((int)AI_State.Patrol):
                EnemyPatrol();
                break;
            case ((int)AI_State.Chase):
                EnemyChase();
                break;
            case ((int)AI_State.Return):
                EnemyReturn();
                break;
        }
    }

    void EnemyPatrol()
    {
        //Debug.Log("Patrol...");
        if (transform.position.x == currentDest.x)
        {
            updateIterator();
            currentDest = destPos[iterator];
            bool previousDirection = right;
            right = currentDest.x > transform.position.x ? true : false;
            if (previousDirection != right)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        }

        if (facingWall && onGround)
        {
            rigid.AddForce(new Vector2(0f,1) * jumpForce, ForceMode2D.Impulse);
        }

        float speed = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(destPos[iterator].x, transform.position.y, originalPos.z), speed);
    }

    void updateIterator()
    {
        //if (iterator == 0)
        //incr = true;
        //else if (iterator == destPos.Count - 1)
        //incr = false;

        //iterator = incr ? iterator + 1 : iterator - 1;
        iterator = (iterator + 1) % destPos.Count;
    }

    void EnemyChase()
    {
        //Debug.Log("Chase...");
        currentDest = chasePlayerObject.transform.position;

        bool previousDirection = right;
        right = currentDest.x > transform.position.x ? true : false;
        if (previousDirection != right)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        if (facingWall && onGround)
        {
            rigid.AddForce(new Vector2(0f, 1) * jumpForce, ForceMode2D.Impulse);
        }

        float speed = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentDest.x, transform.position.y, originalPos.z), speed);
    }

    void EnemyReturn()
    {
        //Debug.Log("Return...");
        currentDest = originalPos;

        bool previousDirection = right;
        right = currentDest.x > transform.position.x ? true : false;
        if (previousDirection != right)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        if (facingWall && onGround)
        {
            rigid.AddForce(new Vector2(0f, 1) * jumpForce, ForceMode2D.Impulse);
        }

        float speed = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentDest.x, transform.position.y, originalPos.z), speed);

        if (transform.position.x == originalPos.x)
        {
            Debug.Log("Return original Pos");
            iterator = 0;
            state = (int)AI_State.Patrol;
        }
    }

}
