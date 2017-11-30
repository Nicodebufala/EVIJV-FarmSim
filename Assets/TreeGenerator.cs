using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour {

	public Terrain terrain;
	// Use this for initialization
	void Start () {
		test ();

		List<TreeInstance> newTrees = new List<TreeInstance> (terrain.terrainData.treeInstances);
		List<Vector3> posTrees = new List<Vector3> ();
		List<GameObject> modelTree = new List<GameObject> ();

		for (int i = 0; i < newTrees.Count; i++) {
			TreeInstance t = newTrees [i];
			Debug.Log (t.position +" "+ i + terrain.terrainData.size);
			posTrees.Add (t.position);
			modelTree.Add (terrain.terrainData.treePrototypes [t.prototypeIndex].prefab);
			posTrees [i] = new Vector3 (posTrees [i].x * terrain.terrainData.size.x, posTrees [i].y * terrain.terrainData.size.y, terrain.terrainData.size.z * posTrees [i].z); 
		}
		terrain.terrainData.treeInstances = new List<TreeInstance>().ToArray ();
		for (int i = 0; i < posTrees.Count; i++) {
			Instantiate (modelTree[i], posTrees[i],Quaternion.identity);
		}

		GameObject forest = GameObject.FindGameObjectWithTag ("Forest");
		GameObject[] trees = GameObject.FindGameObjectsWithTag ("Tree");
		for (int i = 0; i < trees.Length; i++) {
			trees [i].transform.SetParent (forest.transform);
		}


	}

	TerrainData td1;
    TerrainData td2;
	List<TreeInstance> backup;


    void test () {

		Terrain terrain = this.GetComponentInParent<Terrain>();
        if (terrain == null) {
            Debug.LogError("No terrain on GameObject: " + gameObject);
            return;
        }

        td1 = terrain.terrainData;
		backup = new List<TreeInstance> (terrain.terrainData.treeInstances);

        // This is the backup name/path of the cloned TerrainData.
        string tdBackupName = "TerrainData/" + td1.name;
        td2 = Resources.Load<TerrainData>(tdBackupName);
        if (td2 == null) {
            Debug.LogError("No TerrainData backup in a Resources folder, missing name is: " + tdBackupName);
            return;
        }

        // If blue screen. We still have to copy, for sure. It is a fast operation.
        resetTerrainDataChanges();
    }

    void OnApplicationQuit() {
        // To reset the terrain after quite the application. Just needed for the Unity editor.
		Debug.Log("test");
        resetTerrainDataChanges();
    }

    void resetTerrainDataChanges() {
        // Terrain collider
        td1.SetHeights(0, 0, td2.GetHeights(0, 0, td1.heightmapWidth, td1.heightmapHeight));
        // Textures
        td1.SetAlphamaps(0, 0, td2.GetAlphamaps(0, 0, td1.alphamapWidth, td1.alphamapHeight));
        // Trees
		terrain.terrainData.treeInstances = backup.ToArray();
        // Grasses
        //td1.SetDetailLayer(0, 0, 0, td2.GetDetailLayer(0, 0, td1.detailWidth, td1.detailHeight, 0));
    }
// Update is called once per frame
	void Update () {
		
	}
}
