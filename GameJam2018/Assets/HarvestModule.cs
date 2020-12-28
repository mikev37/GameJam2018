using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestModule : Module {

	public override void processOnGrid(NetworkComponent network){
		//Debug.Log ("Chicken Testing: "+type.ToString());
		//Search through all connections and see how many are resources
		//Debug.Log ("Looking for Resources");

		foreach (GameObject go in network.connections) {
			NetworkComponent nC = go.GetComponent<NetworkComponent> ();
			//Debug.Log ("Network Type: "+nC.type);
			if (go.GetComponent<ResourceModule>() != null) {
				//Debug.Log ("Found resource!");
				//Debug.Log ("Obtain money " + NetworkComponent.money);
				NetworkComponent.money += go.GetComponent<ResourceModule>().extractMoney();
				network.copyMoney = NetworkComponent.money;
			}
		}
	}
}
