using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

	public Dictionary<string,float> inventory;
	public GameObject gui;

	// Use this for initialization
	void Start () {
		inventory = new Dictionary<string,float> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addElement(string name,float quantity){
		Debug.Log ("test" + name + quantity);
		if (inventory.ContainsKey (name)) {
			float val = 0f;
			inventory.TryGetValue (name, out val);
			inventory.Remove (name);
			inventory.Add (name, quantity + val);
		} else {
			inventory.Add (name, quantity);
		}
		gui.GetComponent<InventoryDisplay>().updateDisplay ();
	}

	bool removeElement(string name,float quantity){
		float stock = 0f;
		if (inventory.ContainsKey (name)) {
			inventory.TryGetValue (name, out stock);
			if (stock >= quantity) {
				stock -= quantity;
				inventory.Remove (name);
				inventory.Add (name, stock);
				gui.GetComponent<InventoryDisplay>().updateDisplay ();
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}

	}

	public ArrayList getItems(){
		ArrayList arr = new ArrayList ();
		foreach (KeyValuePair<string,float> kvp in inventory)
		{
			//textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
			Debug.Log(kvp);
			arr.Add( string.Format("{0} : {1}", kvp.Key, kvp.Value.ToString()));
		}
		return arr;
	}
}
