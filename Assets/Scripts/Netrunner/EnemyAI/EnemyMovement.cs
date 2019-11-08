using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 기본적인 패트롤 AI
/// Enemy의 하위 오브젝트인 Dest를 순서대로 정찰합니다.
/// Dest 큐브를 복사해서 enemy가 움직일 경로를 설정할 수 있습니다.
/// 모든 Dest를 정찰하면, 역순으로 다시 정찰합니다.
/// 현재는 같은 높이에, 장애물이 없는 경우에만 패트롤 AI가 작동하는데
/// 후에 점프 기능을 넣을 예정입니다.
/// </summary>


public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;

    Vector3 originalPos;
    List<Vector3> destPos;
    Vector3 currentDest;

    int iterator;
    bool incr;


    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;

        destPos = new List<Vector3>();
        Transform PatrolPath = transform.Find("PatrolPath");
        for (int i = 0; i < PatrolPath.childCount; i++)
        {
            Transform dest = PatrolPath.GetChild(i);
            destPos.Add(dest.position);
            dest.gameObject.SetActive(false);
        }

        currentDest = destPos[0];
        iterator = 0;
        incr = true;

    }

    // Update is called once per frame
    void Update()
    {
        EnemyPatrol();
    }

    void EnemyPatrol()
    {
        if (transform.position.x == currentDest.x)
        {
            Debug.Log("arrive dest, move to next dest");
            updateIterator();
            currentDest = destPos[iterator];
            Debug.Log("Current Dest: " + currentDest);
        }

        float speed = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(destPos[iterator].x, originalPos.y, originalPos.z), speed);
    }

    void updateIterator()
    {
        if (iterator == 0)
            incr = true;
        else if (iterator == destPos.Count - 1)
            incr = false;

        iterator = incr ? iterator + 1 : iterator - 1;
    }


}


static class ListExtension
{
    public static T pop<T>(this List<T> list, int index)
    {
        T r = list[index];
        list.RemoveAt(index);
        return r;
    }
}