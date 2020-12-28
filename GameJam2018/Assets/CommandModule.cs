using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandModule : Module {

	public override void processOnGrid(NetworkComponent network){
		foreach (GameObject go in network.fullNetwork) {
			if (go.GetComponent<Module> () != null) {
				//Debug.Log ("Activating "+ go.GetComponent<Module>().ToString());

				if (go.GetComponent<Module> () is CommandModule) {
					//Debug.Log ("Skipping");
					continue;
				
				}
				go.GetComponent<Module> ().processOnGrid (go.GetComponent<NetworkComponent>());
			}
		}
	}

}
