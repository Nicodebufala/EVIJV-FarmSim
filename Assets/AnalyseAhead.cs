using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyseAhead : MonoBehaviour {

	public float distanceRayCast = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position,(transform.forward * distanceRayCast));
		RaycastHit hit;

		if(Physics.Raycast (transform.position, transform.forward, out hit, distanceRayCast)) {
			//Debug.Log (hit.collider.gameObject.tag);
			if (hit.collider.gameObject.tag == "Animal") {
				if (Input.GetKeyDown(KeyCode.E)) {
					hit.collider.gameObject.GetComponent<SoundManager> ().launchSound ();
					hit.collider.gameObject.GetComponent<RessourceManager>().getRessource (this.gameObject);
				}
			}
		}
	}
}
