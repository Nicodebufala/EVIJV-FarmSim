using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolChoiceDisplay : MonoBehaviour {

	public GameObject toolBar;
	public GameObject selector;
	public GameObject player;
	public List<GameObject> tools;
	public List<string> nameTools;
	// Use this for initialization
	void Start () {
		nameTools = new List<string> ();
		toolBar = GameObject.FindGameObjectWithTag ("Inventorybar");
		selector = GameObject.FindGameObjectWithTag("Selector");
		player = GameObject.FindGameObjectWithTag("Player");
		for (int i = 0; i < tools.Count; i++){
			Debug.Log ("Création : " + tools [i].name);
			Instantiate (tools [i], selector.transform.position, Quaternion.identity);
		}
		GameObject[] toolsobjects = GameObject.FindGameObjectsWithTag ("Tool");
		for (int i = 0; i < toolsobjects.Length; i++){
			toolsobjects[i].transform.SetParent(toolBar.transform);
			toolsobjects[i].transform.position = selector.transform.position + new Vector3 (55 * i, 0, 0);
			nameTools.Add (tools[i].name);
			Debug.Log (tools[i].name);
		}

	}
	
	// Update is called once per frame
	void Update () {
		int i = -1;
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			selector.transform.position = new Vector3(212,selector.transform.position.y,selector.transform.position.z);
			i = 0;
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			selector.transform.position = new Vector3(265,selector.transform.position.y,selector.transform.position.z);
			i = 1;
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			selector.transform.position = new Vector3(320,selector.transform.position.y,selector.transform.position.z);
			i = 2;
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			selector.transform.position = new Vector3(376,selector.transform.position.y,selector.transform.position.z);
			i = 3;
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			selector.transform.position = new Vector3(434,selector.transform.position.y,selector.transform.position.z);
			i = 4;
		}
		if(Input.GetKeyDown(KeyCode.Alpha6)){
			selector.transform.position = new Vector3(491,selector.transform.position.y,selector.transform.position.z);
			i = 5;		
		}
		if(Input.GetKeyDown(KeyCode.Alpha7)){
			selector.transform.position = new Vector3(547,selector.transform.position.y,selector.transform.position.z);
			i = 6;
		}
		if(Input.GetKeyDown(KeyCode.Alpha8)){
			selector.transform.position = new Vector3(604,selector.transform.position.y,selector.transform.position.z);
			i = 7;
		}
		if(Input.GetKeyDown(KeyCode.Alpha9)){
			selector.transform.position = new Vector3(661,selector.transform.position.y,selector.transform.position.z);
			i = 8;
		}
		if (i != -1) {
			player.GetComponent<ToolEquipped> ().resetBool ();
			if (i < nameTools.Count) {
				player.GetComponent<ToolEquipped> ().checkEquip (nameTools [i]);
			}
		}


	}
}
