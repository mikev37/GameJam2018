using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkComponent : MonoBehaviour {
	public static double money = 0;
	public double copyMoney = 0;
	//sds
	//neighbors
	public List<GameObject> connections;
	//map of neighbors to lines rendered
	public Dictionary<GameObject, GameObject> fullConnections;

	public Material lineMaterial;
	public Color lineColor = Color.white;

	//whether this node connects neighbors to itself
	public bool connect = true;
	public bool overrideConect = false;

	//the network as accessed from this node.
	public HashSet<GameObject> fullNetwork;

	//output read only network
	public List<GameObject> copyNetwork;

	//update variables
	public float updateMyInterval = .1f;
	float updateTimer = 0f;


	// Use this for initialization
	void Start () {
		connections = new List<GameObject> ();
		copyNetwork = new List<GameObject> ();
		fullNetwork = new HashSet<GameObject> ();
		fullConnections = new Dictionary<GameObject, GameObject> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (!connect)
			return;
		if (other.gameObject.layer != 10)
			return;
		if(other.gameObject.GetComponent<NetworkComponent>().connect || overrideConect)
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
				lr.material = lineMaterial;
				lineColor.a = 1f;
				lr.material.color = node.GetComponent<NetworkComponent>().lineColor;
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
		NetworkComponent nC = source.GetComponent<NetworkComponent> ();
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
		
	public void testCommand(){
		if (GetComponent<CommandModule>() != null) {
			GetComponent<CommandModule> ().processOnGrid (this);
			//Debug.Log ("Command center active");
		}
	}


	// Update is called once per frame
	void Update () {

		if (updateTimer <= 0) {
			updateTimer = updateMyInterval;
			CalculateFullConnections ();
			drawConnections ();
			testCommand ();
		} else {
			updateTimer -= Time.deltaTime;
		}
	}
}
