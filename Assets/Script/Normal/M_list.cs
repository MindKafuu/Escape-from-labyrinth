using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_list : MonoBehaviour {

	//ใส่ใน Game Mamage

	public static M_list Instance;

	void Awake(){
		if (Instance != null) {
			Debug.LogWarning ("More than one instance of commandmand found!");
			return;
		}
		Instance = this;
	}

	public delegate void OnCommandChange();
	public OnCommandChange OnCommandChangeCallback;

	public GameObject box;
	public Transform commandParent;

	public List<GameObject> listPanelSlot = new List<GameObject> ();

	public List<string> Commands { get; private set; }

	void Start () {
        Commands = new List<string>();
        GameObject Box = (GameObject)Instantiate (box);
		Box.transform.SetParent (commandParent);
		Box.gameObject.transform.localScale = new Vector3 (1, 1, 0);
		listPanelSlot.Add (Box);

		RectTransform parent = commandParent.GetComponent<RectTransform> ();
		GridLayoutGroup grid = commandParent.GetComponent<GridLayoutGroup> ();
		grid.cellSize = new Vector2 (parent.rect.width / 7, parent.rect.height / 9);
	}

	public void Add(string input){
		
		Commands.Add (input);

		GameObject Box = (GameObject)Instantiate (box);
		Box.transform.SetParent (commandParent);
		Box.gameObject.transform.localScale = new Vector3 (1, 1, 0);
		listPanelSlot.Add (Box);


		if (Commands.Count >= 48) {
			RectTransform parent = commandParent.GetComponent<RectTransform> ();
			GridLayoutGroup grid = commandParent.GetComponent<GridLayoutGroup> ();
			grid.cellSize = new Vector2 (parent.rect.width / 10, parent.rect.height / 14);
			for (int i = 0; i <= listPanelSlot.Count - 1; i++) {
				
				GameObject PanelSlot = listPanelSlot [i];
				PanelSlot.transform.localScale = new Vector2 (0.75f, 0.75f);

			}

		}


		if (OnCommandChangeCallback != null) {
			OnCommandChangeCallback.Invoke ();
		}

	}
	public void Remove(){
		Commands.RemoveAt (Commands.Count - 1);

		if (listPanelSlot.Count > 0) {
			GameObject lastSlot = listPanelSlot [listPanelSlot.Count - 1];
			listPanelSlot.RemoveAt (listPanelSlot.Count - 1);
			Destroy (lastSlot);
		}


		if (OnCommandChangeCallback != null) {
			OnCommandChangeCallback.Invoke ();
		}
			
	}
	public void Clear(){
		Commands.Clear ();

		for (int i = listPanelSlot.Count - 1; i >= 0; i--) {
			GameObject deleteSlot = listPanelSlot [i];
			Destroy (deleteSlot);
			listPanelSlot.RemoveAt (i);
		}
		GameObject Box = (GameObject)Instantiate (box);
		Box.transform.SetParent (commandParent);
		Box.gameObject.transform.localScale = new Vector3 (1, 1, 0);
		listPanelSlot.Add (Box);


		RectTransform parent = commandParent.GetComponent<RectTransform> ();
		GridLayoutGroup grid = commandParent.GetComponent<GridLayoutGroup> ();
		grid.cellSize = new Vector2 (parent.rect.width / 7, parent.rect.height / 9);


		if (OnCommandChangeCallback != null) {
			OnCommandChangeCallback.Invoke ();
		}
	}

}
