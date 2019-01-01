using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Flashtext : MonoBehaviour {
    
    public Text blinktext;
    bool blink = true;
    private float step = 0.4f;
    
	// Use this for initialization
	void Start ()
    {
        
        StartCoroutine(wait_second());
        
    }
    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            StartCoroutine(change_scence());
            StopCoroutine(wait_second());
            
        }
    }

    public IEnumerator wait_second()
    {

        while (blink)
        {
            blinktext.text = "";
            yield return new WaitForSeconds(step);

            blinktext.text = "กด     เพื่อเริ่มเกม";
            yield return new WaitForSeconds(step);
            
           
        }
        
    }
    IEnumerator change_scence()
    {
        blink = false;
        
        blinktext.text = "เริ่มเกม";
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Menu");
        
        
        
            
        
    }
}
