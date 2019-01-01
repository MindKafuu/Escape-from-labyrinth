using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EPlayerControl : MonoBehaviour
{
    [SerializeField] float rotate = 90.0f;
    [SerializeField] Map map;
    [SerializeField] int maxLife = 3;
    [SerializeField] List<GameObject> hearts;
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private float startRotation;
    [SerializeField] EGameController gameController;
    private enum State { Wait, Move, Win }
    private State state;
    public static int life;
    int x, z;

    public Vector2 PositionInMap { get { return new Vector2(x, z); } }

    void Start()
    {
        foreach (GameObject heart in hearts)
        {
            heart.SetActive(true);
        }

        state = State.Wait;
        transform.rotation = Quaternion.Euler(0, startRotation, 0);
        transform.position = startPoint;
        x = Mathf.RoundToInt(transform.position.x);
        z = Mathf.RoundToInt(transform.position.z);
        life = maxLife;
    }

    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            transform.Rotate(Vector3.down * rotate);
        }
        else if (Input.GetKeyDown("d"))
        {
            transform.Rotate(Vector3.up * rotate);
        }
        else if (Input.GetKeyDown("w"))
        {
            if (x + 1 < map.TileMap.GetLength(1) && map.TileMap[z, x + 1] != '1' && transform.eulerAngles.y == 90.0f) x++;
            else if (x - 1 >= 0 && map.TileMap[z, x - 1] != '1' && transform.eulerAngles.y == 270.0f) x--;
            else if (z + 1 < map.TileMap.GetLength(0) && map.TileMap[z + 1, x] != '1' && transform.eulerAngles.y == 0.0f) z++;
            else if (z - 1 >= 0 && map.TileMap[z - 1, x] != '1' && transform.eulerAngles.y == 180) z--;
        }
        if (map.TileMap[z, x] == '2')
        {
            state = State.Win;
        }

        UpdateHeartUI();
        //Debug.Log(state);
        if (life <= 0)
        {
            Timer.StopTime = true;
            Timer.Die = true;
        }

        if (Timer.TimeUp || life <= 0)
            return;

        if (state == State.Win)
        {
            Timer.StopTime = true;
            Timer.playPass = true;
            foreach (GameObject heart in hearts)
            {
                heart.SetActive(true);
            }

            state = State.Wait;
            Debug.Log("S : " + state);

            gameController.LevelUp();
            transform.rotation = Quaternion.Euler(0, startRotation, 0);
            transform.position = startPoint;
            x = Mathf.RoundToInt(transform.position.x);
            z = Mathf.RoundToInt(transform.position.z);
            life = maxLife;
        }

        transform.position = new Vector3(x, transform.position.y, z);

    }

    public void UpdateHeartUI()
    {
        foreach (GameObject heart in hearts)
        {
            heart.SetActive(false);
        }

        for (int i = 0; i < life; i++)
        {
            hearts[i].SetActive(true);
        }
    }

}
