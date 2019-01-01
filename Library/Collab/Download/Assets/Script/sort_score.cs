using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;

// Ranking

public class sort_score : MonoBehaviour {

    
    //List<string> name_score_list = new List<string>();
    List<string[]> name_score_split = new List<string[]>();
    
    //List<int> score_list = new List<int>();
    int num_row = 0;
    int score_crr,index;
    int score_next,Rank=0;

    public Transform parent;
    public GameObject POS;
    public GameObject Nickname;
    public GameObject User_Score;

    // Use this for initialization
    void Start()
    {    
        Add_name_score_list();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("u"))
        {

            sort_data();
            var index_find = find_index("hhh");
            Debug.Log("Find_Index = " + index_find);
            show_ranking();
        }
    }
    void Add_name_score_list()
    {
        string filepath = @"D:\Unity_work\Keyboard_2D\Assets\Resources\game_ranking.csv";
        StreamReader file_reader = new StreamReader(filepath);
        while (!file_reader.EndOfStream)
        {
            var read = file_reader.ReadLine();
            //name_score_list.Add(read);
            var split_data = read.Split(',');
            Debug.Log(split_data.GetType());
            name_score_split.Add(split_data);

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

    void show_ranking()
    {
        for (var i = 0; i < 6; i++)
        {
            Rank++;
            if (Rank < 6)
            {
                GameObject Pos = (GameObject)Instantiate(POS);
                Pos.transform.SetParent(parent);
                Pos.gameObject.transform.position = new Vector2(790, 560 - 55 * i);
                Text rank = Pos.transform.GetComponent<Text>();
                 
                GameObject name = (GameObject)Instantiate(Nickname);
                name.transform.SetParent(parent);
                name.gameObject.transform.position = new Vector2(900, 560 - 55 * i);
                Text Name = name.transform.GetComponent<Text>();

                GameObject score_gameobject = (GameObject)Instantiate(User_Score);
                score_gameobject.transform.SetParent(parent);
                score_gameobject.gameObject.transform.position = new Vector2(1130, 560 - 55 * i);
                Text Score = score_gameobject.transform.GetComponent<Text>();

                rank.text = "" + Rank;
                Name.text = "                        "+name_score_split[name_score_split.Count-1-i][0];
                Score.text = name_score_split[name_score_split.Count - 1 - i][1];
            }
        
        }
          
        
    }
    
   
    
    
}
