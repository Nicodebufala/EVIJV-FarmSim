using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class AnalyseAhead : MonoBehaviour {

	public float distanceRayCast = 5f;
	public GameObject ui;
	public bool inUi;
	public bool inPlacementMode;
	public GameObject itemToPlace;
	// Use this for initialization
	void Start () {
		inUi = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		Debug.DrawRay(transform.position,(transform.forward * distanceRayCast));
		RaycastHit hit;

		if (!inUi) {
			if (Input.GetMouseButton (1)) {
				GameObject t = ui.GetComponentInChildren<PlaceItemChoiceMenuUI> (true).gameObject;
				if (itemToPlace != null && inPlacementMode) {
					DestroyImmediate(itemToPlace);
					inPlacementMode = false;
				}
				changeLockState ();
				t.SetActive (true);
				t.GetComponent<PlaceItemChoiceMenuUI> ().checkAvailabilities ();
				Debug.Log ("clicdroit");
			}
			if (inPlacementMode && itemToPlace != null && Input.GetMouseButton (0)) {
				if (itemToPlace.GetComponent<PlacingItemCollisionCheck> ().inZonelst.Count == 0) {
					GameObject item = itemToPlace;
					item.GetComponent<PlacingItemCollisionCheck> ().enabled = false;
					item.GetComponent<Rigidbody> ().useGravity = true;
					item.GetComponent<Rigidbody> ().isKinematic = false;
					item.GetComponent<MeshCollider> ().isTrigger = false;
					item.GetComponent<MeshCollider> ().enabled = true;
					item.transform.parent = null;
					this.gameObject.GetComponent<InventoryManager> ().removeElement (itemToPlace.GetComponent<ProductRef>().nameProduct, 1f);
					inPlacementMode = false;
					itemToPlace = null;


				} else {
					Debug.Log (itemToPlace.GetComponent<PlacingItemCollisionCheck> ().inZonelst.Count);
				}

			}
			if (Physics.Raycast (transform.position, transform.forward, out hit, distanceRayCast)) {
				//Debug.Log (hit.collider.gameObject.tag);
				/*if (hit.collider.gameObject.tag == "Animal") {
				if (Input.GetKeyDown(KeyCode.E)) {
					hit.collider.gameObject.GetComponent<SoundManager> ().launchSound ();
					hit.collider.gameObject.GetComponent<RessourceManager>().getRessource (this.gameObject);
				}
			}*/
				if (hit.collider.gameObject.GetComponent<Recoltable> () != null && hit.collider.gameObject.GetComponent<Recoltable> ().readyForHarvest) {
					if (Input.GetMouseButton (0)) {
						hit.collider.gameObject.GetComponent<Recoltable> ().harvest (this.gameObject);
					}
				}
				if (hit.collider.gameObject.GetComponent<SoundManager> () != null) {
					if (Input.GetMouseButton (0)) {
						hit.collider.gameObject.GetComponent<SoundManager> ().launchSound ();
					}
				}
				if (hit.collider.gameObject.tag == "Terrain") {
					if (Input.GetMouseButton (0) && this.GetComponent<ToolEquipped> ().hoe) {
						GameObject.FindObjectOfType<Terrain> ().GetComponentInChildren<SoilManager> ().createSoil (hit.point);
					}
					/*if (Input.GetKeyDown (KeyCode.E) && this.GetComponent<ToolEquipped> ().shovel) { //Add soil Destruction
					GameObject.FindObjectOfType<Terrain> ().GetComponentInChildren<SoilManager> ().createSoil (hit.point);
				}*/

				}
				if (hit.collider.gameObject.tag == "Soil") {
					if (Input.GetMouseButton (0) && this.GetComponent<ToolEquipped> ().shovel) {
						GameObject t = ui.GetComponentInChildren<PlantChoiceMenuUI> (true).gameObject;
						changeLockState ();
						t.GetComponent<PlantChoiceMenuUI> ().soilTouched = hit.collider.gameObject;
						t.GetComponent<PlantChoiceMenuUI> ().updateQuantities ();
						t.SetActive (true);

					}
					/*if (Input.GetKeyDown (KeyCode.E) && this.GetComponent<ToolEquipped> ().shovel) { //Add soil Destruction
					GameObject.FindObjectOfType<Terrain> ().GetComponentInChildren<SoilManager> ().createSoil (hit.point);
				}*/
				}
				if (hit.collider.gameObject.tag == "WorkBench") {
					if (Input.GetMouseButton (0)) {
						GameObject t = ui.GetComponentInChildren<CraftChoiceMenuUI> (true).gameObject;
						changeLockState ();
						t.SetActive (true);


					}
					/*if (Input.GetKeyDown (KeyCode.E) && this.GetComponent<ToolEquipped> ().shovel) { //Add soil Destruction
					GameObject.FindObjectOfType<Terrain> ().GetComponentInChildren<SoilManager> ().createSoil (hit.point);
				}*/
				}
			}

		}
	}

	public void changeLockState(){
		inPlacementMode = false;
		this.GetComponentInParent<RigidbodyFirstPersonController> ().changeLockState ();
		inUi = !inUi;
		if (itemToPlace != null) {
			prepareAndPlacePrefab ();
		}
	}

	public void prepareAndPlacePrefab(){
		GameObject player = GameObject.FindGameObjectWithTag ("Player").transform.parent.gameObject;
		//Debug.Log (this.name + " " +this.GetComponent<MeshCollider> ().bounds.extents);
		RaycastHit hit;
		inPlacementMode = true;
		itemToPlace = Instantiate (itemToPlace, player.transform);
		itemToPlace.GetComponent<MeshJoining> ().launchMJ ();
		float heightItem = itemToPlace.GetComponent<MeshCollider>().bounds.extents.y;
		float radiusItem = itemToPlace.GetComponent<MeshRenderer> ().bounds.extents.z * 2;
		if (Physics.Raycast (player.transform.position + player.transform.forward * radiusItem, player.transform.up * -1 , out hit)) {
			Vector3 posItem = hit.point + new Vector3 (0, heightItem-player.GetComponent<CapsuleCollider>().bounds.extents.y + 0.1f, 0);
			itemToPlace.transform.position = posItem;
			itemToPlace.GetComponent<MeshCollider>().enabled = true;
			itemToPlace.GetComponent<MeshCollider>().isTrigger = true;
			itemToPlace.GetComponent<Rigidbody> ().useGravity = false;
			itemToPlace.GetComponent<Rigidbody> ().isKinematic = true;
			itemToPlace.AddComponent<PlacingItemCollisionCheck> ();
		}

	}
}
