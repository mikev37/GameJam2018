﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class turret_barrel : MonoBehaviour {
	public Material lazer_material;

	// CONFIG VALUES
	float fire_rate = 0.5f;
	bool turret_enabled = true;

	float turret_range = 1;

	int power = 5;

	//END CONFIG

	public List<GameObject> targets = new List<GameObject>();
	float next_fire = 0f;

	// Use this for initialization
	void Start () {
		
	}

	// Set a new enabled state for the turret
	void setEnabled(bool state){
		turret_enabled = state;
	}

	// Get Current state ENABLED|DISABLED
	bool isEnabled(){
		return turret_enabled;
	}

	//Fire at a target
	void shootAt(GameObject target){
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

	//Set a new range for the turret
	void setRange(float range){
		turret_range = range;
		CircleCollider2D col = transform.GetComponent<CircleCollider2D>();
		col.radius = turret_range;
	}

	//Set a new fire rate for the turret
	void setFireRate(float rate){
		fire_rate = rate;
	}

	//Set a new power level for the turret
	void setPower(int p){
		power = p;
	}

	//When another object enters the range of the turret
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer == 8){
			targets.Add(other.gameObject);
		}
	}

	//When another object exits the range of the turret
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.layer == 8) {
			targets.Remove (other.gameObject);
		}
	}

	// Auto Fires are any known targets which are nearby
	void autoShootAtNearby(){
		if(next_fire <= 0 && turret_enabled && targets.Count > 0){
			GameObject closest = null;
			foreach(GameObject target in targets){
				//Set the first target as the closest by default
				if(closest == null){
					closest = target;
					continue;
				}
				//Sync Z Indisies 
				Vector3 tmp_target = target.transform.position;
				tmp_target.z = transform.position.z;

				Vector3 tmp_closest = closest.transform.position;
				tmp_closest.z = transform.position.z;

				//Check if target is closer than the default
				if(Vector3.SqrMagnitude(transform.position)-Vector3.SqrMagnitude(tmp_target) < 
					Vector3.SqrMagnitude(transform.position)-Vector3.SqrMagnitude(tmp_closest)){
					closest = target;
				}
			}
			shootAt (closest);
		}
	}


	// Update is called once per frame
	void Update () {
		next_fire -= Time.deltaTime;
		autoShootAtNearby ();
	}
}
