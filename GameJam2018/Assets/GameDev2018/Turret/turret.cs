using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class turret : MonoBehaviour {

	public float boost; //firerate boost from booster

	// CONFIG VALUES
	bool turret_enabled = true;
	protected float fire_rate = 0.5f;
	float turret_range = 1;
	protected int power = 5;

	//END CONFIG

	public List<GameObject> targets = new List<GameObject>();
	protected float next_fire = 0f;

	// Set a new enabled state for the turret
	public void setEnabled(bool state){
		turret_enabled = state;
	}

	// Get Current state ENABLED|DISABLED
	public bool isEnabled(){
		return turret_enabled;
	}

	//Fire at a target
	protected abstract void shootAt(GameObject target);

	//Set a new range for the turret
	public void setRange(float range){
		turret_range = range;
		CircleCollider2D col = transform.GetComponent<CircleCollider2D>();
		col.radius = turret_range;
	}

	//Set a new fire rate for the turret
	public void setFireRate(float rate){
		fire_rate = rate;
	}

	//Set a new power level for the turret
	public void setPower(int p){
		power = p;
	}

	//When another object enters the range of the turret
	protected void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer == 8){
			targets.Add(other.gameObject);
		}
	}

	//When another object exits the range of the turret
	protected void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.layer == 8) {
			targets.Remove (other.gameObject);
		}
	}

	// Auto Fires are any known targets which are nearby
	protected void autoShootAtNearby(){
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
	protected void Update () {
		next_fire -= Time.deltaTime * (1 + boost);
		autoShootAtNearby ();
		boost -= Time.deltaTime;
		if (boost <= 0)
			boost = 0;
	}
}
