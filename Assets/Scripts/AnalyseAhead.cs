using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class AnalyseAhead : MonoBehaviour {

	public float distanceRayCast = 5f;
	public GameObject ui;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Debug.DrawRay(transform.position,(transform.forward * distanceRayCast));
		RaycastHit hit;

		if(Physics.Raycast (transform.position, transform.forward, out hit, distanceRayCast)) {
			//Debug.Log (hit.collider.gameObject.tag);
			/*if (hit.collider.gameObject.tag == "Animal") {
				if (Input.GetKeyDown(KeyCode.E)) {
					hit.collider.gameObject.GetComponent<SoundManager> ().launchSound ();
					hit.collider.gameObject.GetComponent<RessourceManager>().getRessource (this.gameObject);
				}
			}*/
			if (hit.collider.gameObject.GetComponent<Recoltable>() != null && hit.collider.gameObject.GetComponent<Recoltable>().readyForHarvest) {
				if (Input.GetKeyDown(KeyCode.E)) {
					hit.collider.gameObject.GetComponent<Recoltable>().harvest (this.gameObject);
				}
			}
			if (hit.collider.gameObject.GetComponent<SoundManager>() != null){
				if (Input.GetKeyDown(KeyCode.E)) {
					hit.collider.gameObject.GetComponent<SoundManager> ().launchSound ();
				}
			}
			if (hit.collider.gameObject.tag == "Terrain") {
				if (Input.GetKeyDown (KeyCode.E) && this.GetComponent<ToolEquipped> ().hoe) {
					GameObject.FindObjectOfType<Terrain> ().GetComponentInChildren<SoilManager> ().createSoil (hit.point);
				}
				/*if (Input.GetKeyDown (KeyCode.E) && this.GetComponent<ToolEquipped> ().shovel) { //Add soil Destruction
					GameObject.FindObjectOfType<Terrain> ().GetComponentInChildren<SoilManager> ().createSoil (hit.point);
				}*/

			}
			if (hit.collider.gameObject.tag == "Soil") {
				if (Input.GetKeyDown (KeyCode.E) && this.GetComponent<ToolEquipped> ().shovel) {
					GameObject t = ui.GetComponentInChildren<PlantChoiceMenuUI> (true).gameObject;
					changeLockState ();
					t.GetComponent<PlantChoiceMenuUI>().soilTouched = hit.collider.gameObject;
					t.GetComponent<PlantChoiceMenuUI> ().updateQuantities ();
					t.SetActive(true);

				}
				/*if (Input.GetKeyDown (KeyCode.E) && this.GetComponent<ToolEquipped> ().shovel) { //Add soil Destruction
					GameObject.FindObjectOfType<Terrain> ().GetComponentInChildren<SoilManager> ().createSoil (hit.point);
				}*/

			}

		}
	}

	public void changeLockState(){
		this.GetComponentInParent<RigidbodyFirstPersonController> ().changeLockState ();
	}
}
