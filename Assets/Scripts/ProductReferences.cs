using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductReferences : MonoBehaviour {

	public List<GameObject> productList = new List<GameObject> ();
	public Texture2D texture;
	// Use this for initialization
	void Start () {
		//texture.Resize (20, 40);
		//Cursor.SetCursor (texture, new Vector2 (0, 0), CursorMode.ForceSoftware);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject getPrefab(string name){
		for (int i = 0; i < productList.Count; i++) {
			if (productList [i].name == name) {
				return productList [i];
			}
		}
		return null;

	}
}
