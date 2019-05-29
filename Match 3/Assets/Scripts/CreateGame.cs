using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile{

	public GameObject tileObj;
	public string type;
	public Tile(GameObject obj, string t){
		tileObj = obj;
		type = t;
	}
}

public class CreateGame : MonoBehaviour {

	GameObject tile1 = null;
	GameObject tile2 = null;

	public GameObject[] tile; 
	List<GameObject> tileBank = new List<GameObject> ();

	static int rows = 8;
	static int cols = 6;
	Tile[,] tiles = new Tile[cols,rows];

	void ShuffleList(){
		System.Random rand = new System.Random ();
		int r = tileBank.Count;

		while (r > 1) {
			r--;
			int n = rand.Next (r + 1);
			GameObject val = tileBank [n];;
			tileBank [n] = tileBank [r];
			tileBank [r] = val;
		}
	}


	// Use this for initialization
	void Start () {

		int numcopies = (rows * cols) / 3;

		for (int i = 0; i < numcopies; i++) {
			for (int j = 0; j < tile.Length; j++) {
				GameObject o = GameObject.Instantiate (tile [j], new Vector3 (-10, -10, 0), tile [j].transform.rotation);
				o.SetActive (false);
				tileBank.Add (o);
			}
		}

		ShuffleList ();

		for (int r = 0; r < rows; r++) {
			for (int c = 0; c < cols; c++) {
				Vector3 tilePos = new Vector3 (c, r, 0);
				for (int n = 0; n < tileBank.Count; n++) {

					GameObject o = tileBank [n];
					if (!o.activeSelf) {
						o.transform.position = new Vector3 (tilePos.x, tilePos.y, tilePos.z);
						o.SetActive (true);
						tiles [c, r] = new Tile (o, o.name);
						n = tileBank.Count + 1;
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("Pressed");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				tile1 = hit.collider.gameObject;
				Debug.Log ("Found on press");
			}

		} else if (Input.GetMouseButtonUp (0) && tile1) {
			Debug.Log ("Released");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				Debug.Log ("Found on release");
				tile2 = hit.collider.gameObject;
			}


			if (tile1 && tile2) {
				Vector3 tempPos = tile1.transform.position;
				tile1.transform.position = tile2.transform.position;
				tile2.transform.position = tempPos;

				tile1 = null;
				tile2 = null;
			}

		}

	}
}
