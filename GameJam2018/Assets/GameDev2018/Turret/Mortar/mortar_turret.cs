using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morter_turret : turret {
	public Material morter_shell;

	// Use this for initialization
	void Start () {
		setPower (10);
		setRange (1);
		setFireRate (1.5f);
	}

	//Fire at a target
	protected override void shootAt(GameObject target){
		if(next_fire <= 0){



		}
	}
}