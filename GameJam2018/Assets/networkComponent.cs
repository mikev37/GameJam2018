using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class networkComponent : MonoBehaviour {

	public List<GameObject> connections;

	public HashSet<GameObject> fullNetwork;

	public List<GameObject> copyNetwork;

	public float updateMyInterval = .1f;

	public float updateTimer = 0f;


	// Use this for initialization
	void Start () {
		connections = new List<GameObject> ();
		copyNetwork = new List<GameObject> ();
		fullNetwork = new HashSet<GameObject> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer != 10)
			return;

		connections.Add (other.gameObject);

	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.layer != 10)
			return;

		connections.Remove(other.gameObject);

	}

	void CalculateFullConnections(){
		fullNetwork.Clear ();
		fullNetwork = digInto (gameObject,fullNetwork);
		Debug.Log ("FULL NETWORK " + fullNetwork.Count);
		copyNetwork.Clear();
		foreach(GameObject go in fullNetwork){
			copyNetwork.Add (go);
		}

	}

	HashSet<GameObject> digInto(GameObject source,HashSet<GameObject> explored){
		Debug.Log (source.ToString() + explored.Count.ToString());
		HashSet<GameObject> output = new HashSet<GameObject>();
		networkComponent nC = source.GetComponent<networkComponent> ();
		if (nC == null) {
			return output;
		}

		foreach (GameObject go in nC.connections) {
			output.UnionWith (nC.connections);
			if (!explored.Contains (go)) {
				explored.Add (go);
				output.UnionWith(digInto (go, explored));
				Debug.Log ("OUTPUT SIZE " + output.Count); 
			}
		}
	
		return output;
	}



	// Update is called once per frame
	void Update () {

		if (updateTimer <= 0) {
			updateTimer = updateMyInterval;
			CalculateFullConnections ();
		} else {
			updateTimer -= Time.deltaTime;
		}
	}
}
