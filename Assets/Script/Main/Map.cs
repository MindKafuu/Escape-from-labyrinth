using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    [SerializeField] private List<TextAsset> map;
    public string[] tileMap { get; private set; }
    public char[,] TileMap { get; private set; }

    //prefab
    [SerializeField] GameObject wall;
    [SerializeField] GameObject target;

    public void GenerateLevel(int level)
    {
        if ((level - 1) >= map.Count)
        {
            SceneManager.LoadScene("Rank");
        }
        else
        {
            tileMap = map[level - 1].text.Split('\n');
            GameObject gameBoard = new GameObject("GameBoard")
            {
                tag = "GameBoard"
            };

            //Tilemap[0].Length แนวนอน Tilemap.Length แนวตั้ง
            TileMap = new char[tileMap[0].Length, tileMap.Length];

            for (int i = 0; i < tileMap.Length; i++)
            {
                for (int j = 0; j < tileMap[i].Length; j++)
                {
                    TileMap[j, i] = tileMap[i][j];
                }
            }
            //access to array of map
            for (var i = 0; i < tileMap[0].Length; i++)
            {
                for (var j = 0; j < tileMap.Length; j++)
                {
                    if (TileMap[i, j] == '1')
                    {
                        GameObject.Instantiate(wall, new Vector3(j, 1.5f, i),
                            Quaternion.Euler(new Vector3(0, 0, 0))).transform.SetParent(gameBoard.transform);
                    }
                    else if (TileMap[i, j] == '2')
                    {
                        GameObject.Instantiate(target, new Vector3(j, 1.5f, i),
                            Quaternion.Euler(new Vector3(0, 0, 0))).transform.SetParent(gameBoard.transform);
                    }
                }
            }
        }

    }

}
