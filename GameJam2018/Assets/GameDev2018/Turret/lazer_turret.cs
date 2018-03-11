using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class lazer_turret : turret {
	public Material lazer_material;

	// Use this for initialization
	void Start () {
		setPower (5);
		setRange (1);
		setFireRate (0.5f);
	}

	//Fire at a target
	protected override void shootAt(GameObject target){
		if(next_fire <= 0){
			//Get target position and current position
			Vector3 target_pos = target.transform.position;
			target_pos.z = transform.position.z;
			Vector3 start = transform.position;
			//Reset the next fire counter
			next_fire = fire_rate;

			transform.GetChild(0).transform.LookAt (target_pos);

			//New Line Object
			GameObject myLine = new GameObject();
			myLine.transform.position = start;
			myLine.AddComponent<LineRenderer>();

			//Render the line with the lazer Material
			LineRenderer lr = myLine.GetComponent<LineRenderer>();
			lr.material = new Material(lazer_material);
			start.z += 0.02f;
			target_pos.z += 0.02f;
			lr.startWidth = 0.045f;
			lr.endWidth = 0.045f;
			lr.SetPosition(0, start);
			lr.SetPosition(1, target_pos);

			//Remove the drawn line
			GameObject.Destroy(myLine, 0.05f);
			target.SendMessage("attack", power);
		}
	}
}