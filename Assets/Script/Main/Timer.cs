using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{



    //timer 
    [SerializeField] private Slider sliderG;        //หลอดเวลาสีเขียว 
    [SerializeField] private Slider sliderY;        //หลอดเวลาสีเหลือง
    [SerializeField] private Slider sliderR;        //หลอดเวลาสีแดง
    [SerializeField] Edit_CSV CSV;

    private float TimeCount;        //เวลาที่เหลือ
    public float TimeStart;     //เวลาเริ่มต้น
    public float TimeAdd;           //เวลาที่จะเพิ่มเมื่อหมดด่าน
    private float ValuesSlider;     //ค่าในหลอดเวลา
    private float TimeSet;          //เวลาที่เริ่มต้นในmap
    private float TimeCheck;       //เวลาใช้คำนวน

    //ส่งไปclassอื่น
    public static float TimeScore;  //เอาไปคิดคะแนน 
    public static bool TimeUp;  //หมดเวลา 

    //ดึงจากclassอื่น
    public static bool StopTime;    //หยุดเวลา  
    public static bool playPass;    //เล่นผ่านด่าน

    //กัน Bug
    private int Z;
    private int Y;

    public static int totalScore;


    // Use this for initialization
    void Start()
    {
        TimeCount = TimeStart;
        TimeSet = TimeCount;

        sliderG.value = 1;
        sliderY.value = 1;
        sliderR.value = 1;

        ValuesSlider = 1;

        sliderG.gameObject.SetActive(true);
        sliderY.gameObject.SetActive(false);
        sliderR.gameObject.SetActive(false);

        playPass = false;
        StopTime = false;
        TimeUp = false;

        Z = 1;
        Y = 1;

        totalScore = 0;

        ResetLife();
        ResetShowScore();
    }

    void Update()
    {

        if (StopTime)
        {   //หยุดเวลา

            if (playPass)
            {       //ถ้าเล่นผ่าน
                if (Z == 1)
                {               //กัน Bugไม่ให้ทำซ้ำ
                                //					Debug.Log ("TimeCount = " + TimeCount); 
                                //					Debug.Log ("TimeSet = " + TimeSet); 

                    TimeScore = TimeSet - TimeCount;
                    TimeCheck = TimeSet;

                    StartCoroutine(show());
                    float check = TimeCount + TimeAdd;
                    if (check <= TimeStart)
                    {
                        Debug.Log("add time");
                        TimeCount += TimeAdd;       //เพิ่มเวลา 
                    }
                    else
                    {
                        TimeCount = TimeStart;
                    }
                    TimeSet = TimeCount;
                    Z = 0;
                }

            }
            if (Ache)
            {
                if (Z == 1)
                {
                    ReduceLife();
                    Z = 0;
                }
            }

        }
        else
        {
            Z = 1;
            ResetShowScore();

            //นับถอยหลัง
            if (TimeCount >= 0)
            {
                //				Debug.Log (Time.deltaTime);
                TimeCount -= Time.deltaTime;
            }
            ValuesSlider = TimeCount / TimeStart;
            if (ValuesSlider > 0)
            {
                sliderG.value = ValuesSlider;   //ลดหลอดเวลา
                sliderY.value = ValuesSlider;
                sliderR.value = ValuesSlider;

                if (ValuesSlider >= 0.55)
                {               //เปลี่ยนสีหลอดเวลา
                    sliderG.gameObject.SetActive(true);
                    sliderY.gameObject.SetActive(false);
                    sliderR.gameObject.SetActive(false);
                }
                else if (ValuesSlider >= 0.25 && ValuesSlider < 0.55)
                {
                    sliderG.gameObject.SetActive(false);
                    sliderY.gameObject.SetActive(true);
                    sliderR.gameObject.SetActive(false);
                }
                else if (ValuesSlider < 0.25)
                {
                    sliderG.gameObject.SetActive(false);
                    sliderY.gameObject.SetActive(false);
                    sliderR.gameObject.SetActive(true);
                }
            }   //if ของการลดเวลา
        }   //end else นับถอยหลัง

        if (ValuesSlider < 0.005)
        {
            Debug.Log("time up");
            TimeUp = true;
        }
        if (TimeUp || Die)
        {           //เมื่อตาย
            if (Y == 1)
            {
                Debug.Log("DIE");
                CSV.save_score(totalScore);
                SceneManager.LoadScene("Rank");
                Y = 0;
            }
        }

    }


    //show Score
    [SerializeField] private Text score;
    [SerializeField] private GameObject showScores;
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    public static int numPress;         //รับจำนวนครั้งที่กด
    M_list listCommand;
    public int maxPress;


    IEnumerator show()
    {

        //		Debug.Log ("show score"); 
        showScores.SetActive(true);
        score.gameObject.SetActive(true);
        listCommand = M_list.Instance;

        if (listCommand != null)
            numPress = listCommand.Commands.Count;

        //		Debug.Log ("numPress = " + numPress);
        //		Debug.Log ("TimeScore = " + TimeScore); 
        //		Debug.Log ("TimeCheck = " + TimeCheck); 

        int P = Mathf.FloorToInt(1000 - (TimeScore * (1000 / TimeCheck)) - ((numPress - maxPress) * 3 / 2)); //คำนวนคะแนน

        if (P > 1000)
        {
            P = 1000;
        }
        else if (P < 0)
        {
            P = 0;
        }

        //		Debug.Log (P);

        for (int i = 0; i <= P; i += 5)
        {

            //			Debug.Log (i);

            yield return new WaitForSeconds(1 / 20);

            score.text = i.ToString();

            if (i < 150)
            {
                star1.SetActive(false);
                star2.SetActive(false);
                star3.SetActive(false);
            }
            else if (i >= 150 && i < 450)
            {
                star1.SetActive(true);
                star2.SetActive(false);
                star3.SetActive(false);
            }
            else if (i >= 450 && i < 750)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(false);
            }
            else if (i >= 750)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
            }
        }

        yield return new WaitForSeconds(0.5f);
        ResetShowScore();
        StopTime = false;
        playPass = false;

        if (M_list.Instance != null)
            M_list.Instance.Clear();
    }


    public void ResetShowScore()
    {
        showScores.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }


    //life
    [SerializeField] private GameObject heart1; //โชว์หัวใจ
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;

    private int haveLife;       //จำนวนชีวิตที่มีเหลือ

    //ส่งไปclassอื่น
    public static bool Die;     //ตาย

    //ดึงจากclassอื่น
    public static bool Ache;    //ต้องลดเลือด(เจ็บ)


    void ReduceLife()
    {

        haveLife -= 1;

        //ลดหัวใจ
        if (haveLife == 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            Die = false;
        }
        else if (haveLife == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
            Die = false;
        }
        else if (haveLife == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
            Die = false;
        }
        else if (haveLife == 0)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            Die = true;
        }

        M_list.Instance.Clear();
    }
    void ResetLife()
    {
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);

        haveLife = 3;
        Die = false;
    }


    //wait
    //	IEnumerator wait(int t){
    //		yield return new WaitForSeconds (t);
    //	}




}
