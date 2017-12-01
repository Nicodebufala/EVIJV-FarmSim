﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolEquipped : MonoBehaviour {

	public bool pickAxe = false;
	public bool axe = false;
	public bool shovel = false;
	public bool hoe = false;
	public bool milkingBucket = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void resetBool(){
		pickAxe = false;
		axe = false;
		shovel = false;
		hoe = false;
		milkingBucket = false;
	}

	public bool checkIfUnequipped(){
		return	!( pickAxe || milkingBucket || shovel || axe || hoe);
	}

	public void checkEquip(string name){
		Debug.Log (name);
		if (name == "Pickaxe") {
			pickAxe = true;
		}
		if (name == "MilkingBucket") {
			milkingBucket = true;
		}
		if (name == "Shovel") {
			shovel = true;
		}
		if (name == "Axe") {
			axe = true;
		}
		if (name == "Hoe") {
			hoe = true;
		}
	}
}
