using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void updateDisplay(){
		Debug.Log ("display");
		string tmp = "";
		ArrayList items = player.GetComponent<InventoryManager>().getItems ();
		foreach(string s in items){
			Debug.Log (s);
			tmp =tmp + s + "\n" ;
		}
		Debug.Log (tmp);
		this.GetComponent<Text>().text = tmp;
	}
}
