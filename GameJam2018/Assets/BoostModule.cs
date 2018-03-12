using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostModule : Module {

	public override void processOnGrid(NetworkComponent network){
		//Debug.Log ("Chicken Testing: "+type.ToString());
		//Search through all connections and see how many are resources
		Debug.Log ("Looking for Turrets");

		foreach (GameObject go in network.connections) {
			NetworkComponent nC = go.GetComponent<NetworkComponent> ();
			//Debug.Log ("Network Type: "+nC.type);
			if (go.GetComponent<turret>() != null) {
				go.GetComponent<turret>().boost = 1f;
			}
		}
	}
}
