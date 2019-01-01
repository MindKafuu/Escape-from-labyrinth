using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;


// save user name to CSV

public class Edit_CSV : MonoBehaviour
{
    
    string word,score;
    private List<string[]> rowData = new List<string[]>();
    int num = 0;
    public Button enter_button;
    string[] rowDataTemp = new string[2];
    // Use this for initialization
    [SerializeField]
    public Text user_name;
    void Start()
    {
       
        
    }
    void create_colum()
    {
        
        rowDataTemp[0] = "Name";
        rowDataTemp[1] = "Score";
        rowData.Add(rowDataTemp);
        
        show_output();
    }
    public void Save_name()
    {
        word = user_name.text;
        num++;
        // Creating First row of titles manually..
        // You can add up the values in as many cells as you want.
        rowDataTemp = new string[3];
        rowDataTemp[0] = word; // ID
        
        rowData.Add(rowDataTemp);
        show_output();
        print("Success");
    }
    public void save_score(int score)
    {
        string score_str = score.ToString();
        rowDataTemp[1] = score_str; // score
        rowData.Add(rowDataTemp);
        show_output();
    }
    void show_output()
    {
        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

        //"D:\Unity_work\Keyboard_2D\Assets\Resources\game_ranking.csv";

        string filePath = @Application.dataPath + "/Resources" +"/" + "game_ranking1.csv";
        //string filePath = @"D:\Unity_work\Keyboard_2D\Assets\Resources\game_ranking.csv";

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }
    // Following method is used to retrive the relative path as device platform
       
}

