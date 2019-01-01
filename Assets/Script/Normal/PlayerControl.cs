 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] Camera birdEyeCamera;
    [SerializeField] Camera playerCamera;
    [SerializeField] Map map;
    [SerializeField] List<GameObject> hearts;
    [SerializeField] GameController gameController;
    [SerializeField] TimerText time;
    [SerializeField] Text startText;
    [SerializeField] Text timerText;
    public Text state_text;
    public Animator animation;
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private float startRotation;
    [SerializeField] int maxLife = 3;
    [SerializeField] float rotate = 90.0f;
    private enum State { Wait, Play, Move, Win }
    private State state;
    public static bool check;
    public static bool start;
    int x, z, life;
    float CountingTime;

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
        UpdateHeartUI();
        Debug.Log(state);
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
            CountingTime = 0;
        }

        if (Input.GetKeyDown("r") && state == State.Play)
            StartCoroutine(Pcontrol());

        transform.position = new Vector3(x, transform.position.y, z);

        if (state == State.Wait)
        {
            Timer.StopTime = true;
            CountingTime += Time.deltaTime;
            birdEyeCamera.gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(false);
            //Debug.Log(CountingTime);
            if (CountingTime > 4.0f)
            {
                start = false;
                startText.gameObject.SetActive(true);
                timerText.gameObject.SetActive(true);
                time.StartTimer();
                timerText.text = (int)time.CountingTime + " ";
                if ((int)time.CountingTime == 0)
                {
                    start = true;
                    time.StopTimer();
                    state = State.Play;
                }
            }
            else if (check)
            {
                start = false;
                time.StartTimer();
                timerText.text = (int)time.CountingTime + " ";
                if ((int)time.CountingTime == 0)
                {
                    start = true;
                    time.StopTimer();
                    state = State.Play;
                }
            }
        }
        else if (state == State.Move)
        {
            birdEyeCamera.gameObject.SetActive(false);
            playerCamera.gameObject.SetActive(true);
        }
        else if(state == State.Play)
        {
            Timer.StopTime = false;
            startText.gameObject.SetActive(false);
            timerText.gameObject.SetActive(false);
        }
    }

    IEnumerator Pcontrol()
    {
        state = State.Move;
        Timer.StopTime = true;
        foreach (var i in M_list.Instance.Commands)
        {
            yield return new WaitForSeconds(0.5f);
            switch (i)
            {
                case "a":
                    transform.Rotate(Vector3.down * rotate);
                    break;
                case "d":
                    transform.Rotate(Vector3.up * rotate);
                    break;
                case "w":
                    animation.Play("walk_shoot_ar", -1, 0f);
                    yield return new WaitForSeconds(.5f);
                    if (x + 1 < map.TileMap.GetLength(1) && map.TileMap[z, x + 1] != '1' && transform.eulerAngles.y == 90.0f) x++;
                    else if (x - 1 >= 0 && map.TileMap[z, x - 1] != '1' && transform.eulerAngles.y == 270.0f) x--;
                    else if (z + 1 < map.TileMap.GetLength(0) && map.TileMap[z + 1, x] != '1' && transform.eulerAngles.y == 0.0f) z++;
                    else if (z - 1 >= 0 && map.TileMap[z - 1, x] != '1' && transform.eulerAngles.y == 180) z--;
                    break;
            }
        }

        if (map.TileMap[z, x] == '2')
        {
            yield return new WaitForSeconds(2.0f);
            state = State.Win;
            check = false;
        }

        if (state != State.Win)
        {
            yield return new WaitForSeconds(2.0f);
            state_text.text = "ไม่ผ่าน";
            transform.rotation = Quaternion.Euler(0, startRotation, 0);
            yield return new WaitForSeconds(1.0f);
            state_text.text = "";
            life--;
            state = State.Wait;
            x = (int)startPoint.x;
            z = (int)startPoint.z;
            M_list.Instance.Clear();
        }

        Timer.StopTime = false;
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
