using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Visual Keyboard 2D

public class enable_button : MonoBehaviour
{

    public Text user_name;
    string word;
    public static bool IsPlay;

    Button mybutton_curr;
    Button mybutton_next;
    int button_index = 0;
    int wordIndex = 0;
    [SerializeField]
    private List<Button> button_list = new List<Button>();

    // Use this for initialization
    private void Start()
    {

        mybutton_curr = button_list[0];
        mybutton_curr.interactable = true;

        disable_all_button();

    }
    void disable_all_button()
    {
        for (int i = 1; i <= button_list.Count - 1; i++)
        {
            button_list[i].interactable = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Right_selet();
        Left_select();

        Up_select();
        if (Input.GetKeyDown("r"))
        {
            mybutton_curr.onClick.Invoke();

        }
        if (Input.GetKeyDown("e"))
        {
            delete_characcter();
        }
        
    }

    void Right_selet()
    {
        if (button_index <= button_list.Count)
        {
            if (Input.GetKeyDown("d"))
            {
                button_index++;

                mybutton_curr.interactable = !mybutton_curr.interactable;
                if (button_index == button_list.Count)
                {
                    button_index = 0;
                }
                mybutton_next = button_list[button_index];

                mybutton_next.interactable = !mybutton_next.interactable;
                mybutton_curr = mybutton_next;
            }
        }
    }
    void Left_select()
    {
        if (button_index >= 0)
        {
            if (Input.GetKeyDown("a"))
            {
                if (button_index == 0)
                {
                    button_index = button_list.Count - 1;
                }
                else
                {
                    button_index--;
                }
                mybutton_curr.interactable = !mybutton_curr.interactable;
                mybutton_next = button_list[button_index];
                mybutton_next.interactable = !mybutton_next.interactable;
                mybutton_curr = mybutton_next;
            }
        }
    }
    void Down_select()
    {
        if (Input.GetKeyDown("s"))
        {
            mybutton_curr.interactable = !mybutton_curr.interactable;
            if (button_index < 10)
            {
                button_index = button_index + 10;
            }
            else if (button_index < 19)
            {

                button_index = button_index + 9;
                if (button_index > button_list.Count - 1)
                {
                    button_index = button_list.Count - 1;
                }
            }
            mybutton_next = button_list[button_index];
            mybutton_next.interactable = !mybutton_next.interactable;
            mybutton_curr = mybutton_next;
        }
    }
    void Up_select()
    {
        if (Input.GetKeyDown("w"))
        {
            mybutton_curr.interactable = !mybutton_curr.interactable;
            if (button_index > 19)
            {

                button_index = button_index - 7;
            }
            else if (button_index >= 10)
            {
                button_index = button_index - 9;

            }
            else if (button_index < 10)
            {
                if (button_index < 2)
                {
                    button_index = button_list.Count - (8 - button_index);
                }
                else if (button_index == 2 || button_index == 3)
                {
                    button_index = 21;
                }
                else if (button_index == 4 || button_index == 5)
                {
                    button_index = 22;
                }
                else if (button_index == 6 || button_index == 7)
                {
                    button_index = 23;
                }
                else button_index = button_list.Count - (12 - button_index);
            }


            mybutton_next = button_list[button_index];
            mybutton_next.interactable = !mybutton_next.interactable;
            mybutton_curr = mybutton_next;

        }
    }
    public void get_character(string character)
    {
        wordIndex++;
        if (wordIndex == 1)
        {
            character = character.ToUpper();
        }
        word = word + character;
        user_name.text = word;
    }
    public void delete_characcter()
    {
        if (word != null)
            word = word.Remove(wordIndex - 1);
        wordIndex--;
        user_name.text = word;
    }

    public void change_scence()
    {
        IsPlay = true;
        //เปลี่ยน scene
        SceneManager.LoadScene("Mode");
    }
}
