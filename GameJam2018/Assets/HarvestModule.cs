using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestModule : Module {

	public override void processOnGrid(NetworkComponent network){
		//Debug.Log ("Chicken Testing: "+type.ToString());
		//Search through all connections and see how many are resources
		foreach (GameObject go in network.connections) {
			NetworkComponent nC = go.GetComponent<NetworkComponent> ();
			//Debug.Log ("Network Type: "+nC.type);
			if (nC.gameObject.GetComponent<ResourceModule>() != null) {
				NetworkComponent.money += nC.gameObject.GetComponent<ResourceModule>().extractMoney();
				network.copyMoney = NetworkComponent.money;
			}
		}
	}
}
