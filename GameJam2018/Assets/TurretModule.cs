using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretModule : Module{

	float timer;

	public override void processOnGrid(NetworkComponent network){
		GetComponent<turret> ().enabled = true;
		timer = 5f;
	}

	void Update(){
		if (timer <= 0) {
			GetComponent<turret> ().enabled = false;
			timer = 5f;
		}
		timer -= Time.deltaTime;
	}
}
