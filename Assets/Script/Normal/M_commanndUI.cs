using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class M_commanndUI : MonoBehaviour {

	//ใส่ใน canvas

	public Transform commandParent;

	M_list listCommand;

	M_commandSlot[] slots ;

//	public GameObject box;

	void Start () {
		listCommand = M_list.Instance;
		listCommand.OnCommandChangeCallback += UpdateUI;
		slots = commandParent.GetComponentsInChildren<M_commandSlot> ();


	}
	

	void Update () {
		
	}

	void UpdateUI(){
		slots = commandParent.GetComponentsInChildren<M_commandSlot> ();

		for (int i = 0; i < slots.Length; i++) {
			if (i < listCommand.Commands.Count) {
				slots [i].AddCommand (listCommand.Commands [i]);
			} else {
				slots [i].ClearSlot ();
			}
		}

	}
}
