using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructable : MonoBehaviour {
	public bool shrikable = false;
	// CONFIG VALUES
	float damage_interval = 1f;
	public int health = 100;
	public int healthMax = 100;
	public int creep_damage = 5;
	public float hit_range = 0.8f;	

	// END CONFIG

	float next_damage = 0f;

	public List<GameObject> entities = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}

	//Died
	void die(){
		Destroy (this.gameObject);
	}

	// Process an attack from another entity
	public bool attack(int damage){
		health -= damage;
		if (shrikable) {
			transform.localScale *= 1f * health / healthMax;
		}
		if(health <= 0){
			die ();
			return true;
		}
		return false;
	}

	// Trigger when creep hits
	public bool creepHit(){
		return attack (creep_damage);
	}


	//When another object enters the range of the turret
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer == 8){
			entities.Add(other.gameObject);
		}
	}

	//When another object exits the range of the turret
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.layer == 8) {
			entities.Remove (other.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		next_damage -= Time.deltaTime;

		if(next_damage <= 0){
			next_damage = damage_interval;
			foreach(GameObject target in entities){

				//Sync the Z Indicies before distance computation
				Vector3 tmp_target = target.transform.position;
				tmp_target.z = transform.position.z;

				if(Vector3.Distance(tmp_target, transform.position) <= hit_range){
					if (creepHit ()) {
						break;
					}
				}
			}
		}
		
	}
}
