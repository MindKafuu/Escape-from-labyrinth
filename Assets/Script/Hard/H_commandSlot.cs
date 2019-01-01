using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class H_commandSlot : MonoBehaviour {

	//ใส่ใน commandSlot ที่เป็น Children

	//ใส่ Grid layout Group ที่ commandParent

	public Image icon;

	public Sprite move;
	public Sprite turnLeft;
	public Sprite turnRight;

	public void AddCommand(string Command){

		if (Command == "w") {
			icon.sprite = move;
		} else if (Command == "a") {
			icon.sprite = turnLeft;
		} else if (Command == "d") {
			icon.sprite = turnRight;
		}
		icon.enabled = true;
	}

	public void ClearSlot(){
		icon.sprite = null;
		icon.enabled = false;

	}

}
