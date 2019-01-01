using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int searchingDistance = 3;
    private EMap map;
    private EPlayerControl target;
    private Vector2 positionInMap;
    private bool isMove;

    private void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<EMap>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<EPlayerControl>();

    }

    private void Update()
    {
        if (FindTarget() & !isMove && EPlayerControl.start)
        {
            Debug.Log("Found Target.!!!!");

            // Move to target.
            StartCoroutine(Move());

        }
        if(positionInMap == target.PositionInMap)
        {
            Destroy(gameObject);
            EPlayerControl.life--;
        }

        //Debug.Log(positionInMap + " " + transform.position);
    }

    private IEnumerator Move()
    {
        // Move
        isMove = true;
        Vector2 oldPosition = positionInMap;
        int currentDistance = CalculateDistance(target.PositionInMap, positionInMap);

        if (CalculateDistance(target.PositionInMap, positionInMap + Vector2.up) < currentDistance
            && (int)positionInMap.y + 1 < map.TileMap.GetLength(0)
            && (map.TileMap[(int)positionInMap.y + 1, (int)positionInMap.x] != '1'))
        {
            positionInMap = positionInMap + Vector2.up;
        }
        else if (CalculateDistance(target.PositionInMap, positionInMap + Vector2.left) < currentDistance
            && (int)positionInMap.x - 1 >= 0
            && (map.TileMap[(int)positionInMap.y, (int)positionInMap.x - 1] != '1'))
        {
            positionInMap = positionInMap + Vector2.left;
        }
        else if (CalculateDistance(target.PositionInMap, positionInMap + Vector2.right) < currentDistance
            && (int)positionInMap.x + 1 < map.TileMap.GetLength(1)
            && (map.TileMap[(int)positionInMap.y, (int)positionInMap.x + 1] != '1'))
        {
            positionInMap = positionInMap + Vector2.right;
        }
        else if (CalculateDistance(target.PositionInMap, positionInMap + Vector2.down) < currentDistance
            && (int)positionInMap.y - 1 >= 0
            && (map.TileMap[(int)positionInMap.y - 1, (int)positionInMap.x] != '1'))
        {
            positionInMap = positionInMap + Vector2.down;
        }

        // set position of emeny.
        transform.position = new Vector3(positionInMap.x, 0, positionInMap.y);

        yield return new WaitForSeconds(1.0f);
        isMove = false;
    }

    private bool FindTarget()
    {
        int distance;
        distance = CalculateDistance(target.PositionInMap, positionInMap);

        //Debug.Log(distance + " " + target.PositionInMap + " " + positionInMap);
        if (distance <= searchingDistance)
            return true;
        else
            return false;

    }

    private int CalculateDistance(Vector2 t1, Vector3 t2)
    {
        return (int)(Mathf.Abs(t1.x - t2.x) + Mathf.Abs(t1.y - t2.y));
    }

    // Set position of emeny when spawn it.
    public void SetPosition(int xInMap, int yInMap)
    {
        positionInMap = new Vector2(xInMap, yInMap);
    }
}
