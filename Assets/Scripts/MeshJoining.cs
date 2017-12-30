using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshJoining : MonoBehaviour {

	// Use this for initialization
	void Start() {
		launchMJ ();
    }	

	public void launchMJ(){
		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
		List<CombineInstance> tmp = new List<CombineInstance> ();
		int i = 1;
		while (i < meshFilters.Length) {
			if (meshFilters [i] != null) {
				CombineInstance t = new CombineInstance ();
				t.subMeshIndex = 0;
				t.mesh = meshFilters[i].sharedMesh;
				t.transform = Matrix4x4.TRS (meshFilters [i].transform.localPosition, meshFilters [i].transform.localRotation, meshFilters [i].transform.localScale);
				meshFilters[i].gameObject.SetActive(false);
				tmp.Add (t);
			}
			i++;
		}
		Mesh m = new Mesh ();
		m.name = "test";
		CombineInstance[] combine = tmp.ToArray();
		m.CombineMeshes(combine,true);
		Debug.Log(this.gameObject.name);
		this.gameObject.GetComponent<MeshFilter>().mesh = m;
		this.gameObject.GetComponent<MeshCollider> ().sharedMesh = m;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
