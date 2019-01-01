using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Ranking

public class sort_score : MonoBehaviour {

    [SerializeField] Edit_CSV keyName;
    List<string> name_score_list = new List<string>();
    List<string[]> name_score_split = new List<string[]>();
    string data_temp;
    //List<int> score_list = new List<int>();
    int num_row = 0;
    int score_crr,index;
    int score_next,Rank=0;
   

    public Transform parent;
    public GameObject POS_text;
    public GameObject Nickname_text;
    public GameObject User_Score_text;
    public GameObject Goal_BG, Silver_BG,Copper_BG;
    GameObject BG_color;

    // Use this for initialization
    void Start()
    {
        Add_name_score_list();
        sort_data();
        StartCoroutine(show_ranking());
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown("s"))
        {
            
            SceneManager.LoadScene("Start");
        }
        //string find_name = name_score_split[name_score_split.Count - 1][0];
       
        //var index_find = find_index(find_name);
        //Debug.Log("Find_Index = " + index_find);
        

    }
    void Add_name_score_list()
    {
        string filepath = @Application.dataPath + "/Resources" + "/" + "game_ranking1.csv";
        StreamReader file_reader = new StreamReader(filepath);
        while (!file_reader.EndOfStream)
        {
            var read = file_reader.ReadLine();
            //name_score_list.Add(read);
            if (read != "")
            {
                data_temp += read + ",";
                if (num_row % 2 != 0)
                {
                    
                    //Debug.Log(data_temp);   
                    var data_split = data_temp.Split(',');
                    data_temp = "";
                    //Debug.Log(data_split[0] + " : " +data_split[1]);
                    name_score_split.Add(data_split);
 
                }
                num_row++;
            }
            //var split_data = read.Split(',');
        }     
        
        
        



    }
    void sort_data()
    {
        for(var i=0;i <name_score_split.Count;i++)
        {
            for (var j = 0; j < name_score_split.Count - 1; j++)
            {
                score_crr = int.Parse(name_score_split[j][1]);
                //Debug.Log(score_crr.GetType());

                score_crr = int.Parse(name_score_split[j][1]);
                score_next = int.Parse(name_score_split[j + 1][1]);
                if (score_crr > score_next)
                {

                    /*var temp_data_list = name_score_list[j];
                    name_score_list[j] = name_score_list[j+1];
                    name_score_list[j+1] = temp_data_list;*/

                    
                    var temp_data_split_score = name_score_split[j][1];
                    var temp_data_split_name  = name_score_split[j][0];

                    name_score_split[j][1] = name_score_split[j + 1][1];
                    name_score_split[j][0] = name_score_split[j + 1][0];

                    name_score_split[j + 1][1] = temp_data_split_score;
                    name_score_split[j + 1][0] = temp_data_split_name;
                }
            }
            

        }
       
        
    }

    int find_index(string username)
    {
        for (var a = 0; a < name_score_split.Count; a++)
        {
            if (name_score_split[a][0] == username) 
            {
                 index = a+1; 
            }
        }
        return index;
    }

    IEnumerator show_ranking()
    {
        yield return new WaitForSeconds(1f);
        for (var i = 0; i < name_score_split.Count; i++)
        {
            Rank++;
            
            if (Rank < 4)
            {

                
                if (Rank == 1)
                {
                    BG_color = (GameObject)Instantiate(Goal_BG);
                    BG_color.transform.SetParent(parent);
                    BG_color.gameObject.transform.position = new Vector2(950, 550);
                }
                else if (Rank == 2)
                {
                    BG_color = (GameObject)Instantiate(Silver_BG);
                    BG_color.transform.SetParent(parent);
                    BG_color.gameObject.transform.position = new Vector2(950, 495);
                }
                else
                {
                    BG_color = (GameObject)Instantiate(Copper_BG);
                    BG_color.transform.SetParent(parent);
                    BG_color.gameObject.transform.position = new Vector2(950, 440);
                }
                

                GameObject Pos = (GameObject)Instantiate(POS_text);
                Pos.transform.SetParent(parent);
                Pos.gameObject.transform.position = new Vector2(770, 550 - (55*i));
                Debug.Log("i = " + i);
                Debug.Log(550 - (40 * i));
                Text rank = Pos.transform.GetComponent<Text>();

                GameObject name = (GameObject)Instantiate(Nickname_text);
                name.transform.SetParent(parent);
                name.gameObject.transform.position = new Vector2(950,550 - (55 * i));
                Text Name = name.transform.GetComponent<Text>();

                GameObject score_gameobject = (GameObject)Instantiate(User_Score_text);
                score_gameobject.transform.SetParent(parent);
                score_gameobject.gameObject.transform.position = new Vector2(1130,550- (55 * i));
                Text Score = score_gameobject.transform.GetComponent<Text>();

                rank.text = "" + Rank;
                Name.text = " " + name_score_split[name_score_split.Count -1 - i][0];
                Score.text = name_score_split[name_score_split.Count -1 - i][1];
                
            }
            /*if (Rank < 6 && Rank >3)
            {
                GameObject Pos = (GameObject)Instantiate(POS_text);
                Pos.transform.SetParent(parent);
                Pos.gameObject.transform.position = new Vector2(900, 560 - 55 * i);
                Text rank = Pos.transform.GetComponent<Text>();

                GameObject name = (GameObject)Instantiate(Nickname_text);
                name.transform.SetParent(parent);
                name.gameObject.transform.position = new Vector2(1000, 560 - 55 * i);
                Text Name = name.transform.GetComponent<Text>();

                GameObject score_gameobject = (GameObject)Instantiate(User_Score_text);
                score_gameobject.transform.SetParent(parent);
                score_gameobject.gameObject.transform.position = new Vector2(1300, 560 - 55 * i);
                Text Score = score_gameobject.transform.GetComponent<Text>();

                rank.text = "" + Rank;
                Name.text = " " + name_score_split[name_score_split.Count - 1 - i][0];
                Score.text = name_score_split[name_score_split.Count - 1 - i][1];
            }*/
            yield return new WaitForSeconds(1f);
        }
          
        
    }
    
   
    
    
}
