using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Pickup : MonoBehaviour {

	// ใส่ใน canvas
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!Timer.StopTime)
        {
            if (Input.GetKeyDown("w") && PlayerControl.start)
            {
                M_list.Instance.Add("w");
            }
            else if (Input.GetKeyDown("a") && PlayerControl.start)
            {
                M_list.Instance.Add("a");
            }
            else if (Input.GetKeyDown("d") && PlayerControl.start)
            {
                M_list.Instance.Add("d");
            }
            else if (Input.GetKeyDown("e") && PlayerControl.start)
            {
                //			Debug.Log ("remove");
                M_list.Instance.Remove();
            }
        }
        

	}
}
