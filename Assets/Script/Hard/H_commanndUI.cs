using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class H_commanndUI : MonoBehaviour {

	//ใส่ใน canvas

	public Transform commandParent;

	H_list listCommand;

	H_commandSlot[] slots ;

//	public GameObject box;

	void Start () {
		listCommand = H_list.Instance;
		listCommand.OnCommandChangeCallback += UpdateUI;
		slots = commandParent.GetComponentsInChildren<H_commandSlot> ();


	}
	

	void Update () {
		
	}

	void UpdateUI(){
		slots = commandParent.GetComponentsInChildren<H_commandSlot> ();

		for (int i = 0; i < slots.Length; i++) {
			if (i < listCommand.Commands.Count) {
				slots [i].AddCommand (listCommand.Commands [i]);
			} else {
				slots [i].ClearSlot ();
			}
		}

	}
}
