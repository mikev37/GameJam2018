using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandModule : Module {

	public override void processOnGrid(NetworkComponent network){
		foreach (GameObject go in network.fullNetwork) {
			if (go.GetComponent<Module> () != null && go != gameObject) {
				if (go.GetComponent<Module> () is CommandModule)
					continue;
				go.GetComponent<Module> ().processOnGrid (network);
			}
		}
	}

}
