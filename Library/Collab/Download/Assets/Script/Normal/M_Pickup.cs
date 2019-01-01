using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Pickup : MonoBehaviour {

	// ใส่ใน canvas
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("w")) {
			M_list.Instance.Add ("w");
		} else if (Input.GetKeyDown ("a")) {
			M_list.Instance.Add ("a");
		} else if (Input.GetKeyDown ("d")) {
			M_list.Instance.Add ("d");
		} else if (Input.GetKeyDown ("f")) {
//			Debug.Log ("remove");
			M_list.Instance.Remove ();
		} else if (Input.GetKeyDown ("c")) {
			M_list.Instance.Clear ();
		}


	}
}
