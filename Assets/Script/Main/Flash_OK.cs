using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash_OK : MonoBehaviour {

    public Text blink_OK;
    bool blink = true;
    private float step = 0.4f;

    // Use this for initialization
    void Start()
    {

        StartCoroutine(wait_second());
       
    }
    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {

            StopCoroutine(wait_second());
            change_scence();
        }
    }

    public IEnumerator wait_second()
    {

        while (blink)
        {
            blink_OK.text = " ";
            yield return new WaitForSeconds(step);

            blink_OK.text = "OK";
            yield return new WaitForSeconds(step);


        }
    }
    void change_scence()
    {
        blink = false;
        blink_OK.text = "";
    }
}
