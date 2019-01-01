using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// save user name to CSV

public class Edit_CSV : MonoBehaviour
{

    string word, score;
    private List<string[]> rowData = new List<string[]>();
    private List<string> TempData = new List<string>();
    public Button enter_button;
    string[] rowDataTemp = new string[1];
    // Use this for initialization
    [SerializeField]
    public Text user_name;

    private void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            delete_name();
            SceneManager.LoadScene("Start");
            
        }
    }

    public void Save_name()
    {
        word = user_name.text;
        if (word == "Enter name")
        {
            user_name.text = "";
            word = user_name.text;
        }
        // Creating First row of titles manually..
        // You can add up the values in as many cells as you want.
        
            rowDataTemp = new string[1];
            rowDataTemp[0] = word; // ID

            rowData.Add(rowDataTemp);
            save_data_name();
            print("Success");
        
    }
    public void save_score(int score)
    {
        rowDataTemp = new string[1];
        string score_str = score.ToString();
        rowDataTemp[0] = score_str; // score
        rowData.Add(rowDataTemp);
        save_data_score();
    }
    void save_data_name()
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



        string filePath = @Application.dataPath + "/Resources" + "/" + "game_ranking1.csv";
        //string filePath = @"D:\Unity_work\Keyboard_2D\Assets\Resources\game_ranking.csv";

        StreamWriter outStream = new StreamWriter(filePath, true);
        outStream.Write(sb);
        outStream.Close();
    }
    
    // Following method is used to retrive the relative path as device platform
    void save_data_score()
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

        string filePath = @Application.dataPath + "/Resources" + "/" + "game_ranking1.csv";
        //string filePath = @"D:\Unity_work\Keyboard_2D\Assets\Resources\game_ranking.csv";

        StreamWriter outStream = new StreamWriter(filePath, true);
        outStream.WriteLine(sb);
        outStream.Close();
    }
    public void delete_name()
    {
        string filePath = @Application.dataPath + "/Resources" + "/" + "game_ranking1.csv";

        
        StreamReader outStream = new StreamReader(filePath);
        
        while (!outStream.EndOfStream)
        {
            var data_read = outStream.ReadLine();
            if (data_read != "")
            {
                TempData.Add(data_read);
            }

        }
        foreach (var j in TempData)
        {
            Debug.Log(j);

        }
        Debug.Log("before close");
        outStream.Close();
        Debug.Log("after close");
        write_data();
        

    }
    void write_data()
    {
        string filePath = @Application.dataPath + "/Resources" + "/" + "game_ranking1.csv";
        StreamWriter data_write = new StreamWriter(filePath);
        Debug.Log(TempData.Count);
        if (TempData.Count > 0)
        {
            Debug.Log("delete");
            
            for (var j=0;j<TempData.Count-1;j++)
            {
                data_write.WriteLine(TempData[j]);
                
            }
            Debug.Log("complete");
            data_write.Close();



        }
    }
}

