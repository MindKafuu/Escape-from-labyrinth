using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EPlayerControl : MonoBehaviour
{
    [SerializeField] EMap map;
    [SerializeField] List<GameObject> hearts;
    [SerializeField] EGameController gameController;
    [SerializeField] TimerText time;
    [SerializeField] Text startText;
    [SerializeField] Text timerText;
    [SerializeField] Text failText;
    [SerializeField] float rotate = 90.0f;
    [SerializeField] int maxLife = 3;
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private float startRotation;
    public static int life;
    public static bool start;
    public static bool check;
    private enum State { Wait, Move, Win }
    private State state;
    int x, z;
    float CountingTime;

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
        CountingTime = 0;
    }

    void Update()
    {

        if (start && state == State.Move)
        {
            Timer.StopTime = true;

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
                check = false;
            }
        }

        UpdateHeartUI();
        Debug.Log(state);
        if (life <= 0)
        {
            Timer.StopTime = true;
            Timer.Die = true;
            failText.gameObject.SetActive(true);
        }

        if (Timer.TimeUp || life <= 0)
            return;

        if (state == State.Win)
        {
            start = false;
            Timer.StopTime = true;
            Timer.playPass = true;
            Debug.Log(Timer.playPass);
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
            CountingTime = 0;
        }

        transform.position = new Vector3(x, transform.position.y, z);

        if (state == State.Wait)
        {
            Timer.StopTime = true;
            CountingTime += Time.deltaTime;
            failText.gameObject.SetActive(false);
            start = false;
            //Debug.Log(CountingTime);
            if (CountingTime > 3.5f)
            {
                startText.gameObject.SetActive(true);
                timerText.gameObject.SetActive(true);
                time.StartTimer();
                timerText.text = (int)time.CountingTime + " ";
                if ((int)time.CountingTime == 0)
                {
                    time.StopTimer();
                    start = true;
                    state = State.Move;
                }
            }
            else if (check)
            {
                time.StartTimer();
                timerText.text = (int)time.CountingTime + " ";
                if ((int)time.CountingTime == 0)
                {
                    time.StopTimer();
                    start = true;
                    state = State.Move;
                }
            }
        }
        if (state == State.Move)
        {
            Timer.StopTime = false;
            startText.gameObject.SetActive(false);
            timerText.gameObject.SetActive(false);
        }

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
