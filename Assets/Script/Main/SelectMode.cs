using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMode : MonoBehaviour {

    [SerializeField] Edit_CSV keyName;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey ("a") && enable_button.IsPlay) {
            SceneManager.LoadScene("Easy");
        } 
		else if (Input.GetKey ("w") && enable_button.IsPlay) {
			SceneManager.LoadScene ("Normal");
		} 
		else if (Input.GetKey ("d") && enable_button.IsPlay) {
			SceneManager.LoadScene ("Hard");
		}
        else if (Input.GetKey("p") && enable_button.IsPlay)
        {
            SceneManager.LoadScene("Menu");
        }
        else if (Input.GetKeyDown("s"))
        {
            keyName.delete_name();
            SceneManager.LoadScene("Start");
        }
    }
}
