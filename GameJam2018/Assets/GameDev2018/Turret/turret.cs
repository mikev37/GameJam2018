using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour {

	//CONFIG VALUES
	public int health = 100;

	//eND CONFIG

	static int c = 0;
	string turret_name;

	// Use this for initialization
	void Start () {
		turret_name = "Turret #" + c;
		c++;
	}

	//Turret Died
	void die(){
		Debug.Log (turret_name+" has died!!");
	}

	// Process an attack from another entity
	void attack(int damage){
		health -= damage;
		if(health <= 0){
			die ();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
