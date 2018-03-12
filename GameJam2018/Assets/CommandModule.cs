using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandModule : Module {

	protected override void processOnGrid(HashSet<GameObject> fullNetwork){
		foreach (GameObject go in fullNetwork) {
			networkComponent nC = go.GetComponent<networkComponent> ();
			Debug.Log (go);
			nC.testHarvester ();
		}
	}

}
