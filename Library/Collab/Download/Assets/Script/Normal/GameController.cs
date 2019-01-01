using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int level;
    [SerializeField] Map map;

    void Start()
    {
        level = 1;
        map.GenerateLevel(level);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelUp()
    {
        Destroy(GameObject.FindGameObjectWithTag("GameBoard"));
        level++;
        map.GenerateLevel(level);
    }
}
