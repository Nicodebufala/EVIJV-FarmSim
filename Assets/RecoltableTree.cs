using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoltableTree : Recoltable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void harvest(GameObject player){
		float tmp = quantity;
		quantity = 0f;
		player.GetComponent<InventoryManager>().addElement(nameRessource,Mathf.CeilToInt(tmp));
		GameObject.FindObjectOfType<Terrain> ().GetComponentInChildren<TreeGenerator> ().addArbreToRespawn (this.transform.position, Time.time, this.gameObject);
	}
}
