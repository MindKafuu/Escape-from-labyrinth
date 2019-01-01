using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Pickup : MonoBehaviour {

	public static bool StopTime ;
	// ใส่ใน canvas
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!H_Timer_use.StopTime) {
			if (Input.GetKeyDown ("w")) {
				H_list.Instance.Add ("w");
			} else if (Input.GetKeyDown ("a")) {
				H_list.Instance.Add ("a");
			} else if (Input.GetKeyDown ("d")) {
				H_list.Instance.Add ("d");
			} else if (Input.GetKeyDown ("f")) {
//			Debug.Log ("remove");
				H_list.Instance.Remove ();
			} else if (Input.GetKeyDown ("c")) {
				H_list.Instance.Clear ();
			}
		}


	}
}
