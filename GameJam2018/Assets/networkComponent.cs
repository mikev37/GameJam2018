using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class networkComponent : MonoBehaviour {
	public static double money = 0;
	public double copyMoney = 0;
	public List<GameObject> connections;
	public string type;
	public HashSet<GameObject> fullNetwork;
	public Dictionary<GameObject, GameObject> fullConnections;

	public List<GameObject> copyNetwork;

	public float updateMyInterval = .1f;

	public float updateTimer = 0f;


	// Use this for initialization
	void Start () {
		connections = new List<GameObject> ();
		copyNetwork = new List<GameObject> ();
		fullNetwork = new HashSet<GameObject> ();
		fullConnections = new Dictionary<GameObject, GameObject> ();
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
		//Debug.Log ("FULL NETWORK " + fullNetwork.Count);
		copyNetwork.Clear();
		foreach(GameObject go in fullNetwork){
			copyNetwork.Add (go);
		}

	}
	void drawConnections(){
		
		foreach(GameObject node in fullConnections.Keys){
			if (connections.Contains (node)) {
				//Update existing lines
				LineRenderer line = fullConnections[node].GetComponent<LineRenderer>();

				line.SetPosition (0, transform.position);
				line.SetPosition (1, node.transform.position);

			} else {
				//Remove non needed line
				GameObject.Destroy(fullConnections[node]);
				fullConnections.Remove(node);
			}
		}

		foreach(GameObject node in connections){
			if(!fullConnections.ContainsKey(node)){
				//New Line Object
				GameObject line = new GameObject();
				line.transform.parent = transform;
				line.transform.position = transform.position;
				line.AddComponent<LineRenderer>();

				//Render the line with the lazer Material
				LineRenderer lr = line.GetComponent<LineRenderer>();
				//lr.material = new Material(lazer_material);
				
				lr.startWidth = 0.2f;
				lr.endWidth = 0.2f;
				lr.SetPosition(0, transform.position);
				lr.SetPosition(1, node.transform.position);
				fullConnections.Add (node, line);
			}
		}

	}

	HashSet<GameObject> digInto(GameObject source,HashSet<GameObject> explored){
		//Debug.Log (source.ToString() + explored.Count.ToString());
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
				//Debug.Log ("OUTPUT SIZE " + output.Count); 
			}
		}
	
		return output;
	}

	void testHarvester(){
		

		if (type == "harvester") {
			//Debug.Log ("Chicken Testing: "+type.ToString());
			//Search through all connections and see how many are resources
			foreach (GameObject go in connections) {
				networkComponent nC = go.GetComponent<networkComponent> ();
				//Debug.Log ("Network Type: "+nC.type);
				if (nC.type == "resource") {
					money += .1;
					copyMoney = money;
				}
			}
		}
	}


	// Update is called once per frame
	void Update () {

		if (updateTimer <= 0) {
			updateTimer = updateMyInterval;
			CalculateFullConnections ();
			drawConnections ();
			testHarvester ();
		} else {
			updateTimer -= Time.deltaTime;
		}
	}
}
