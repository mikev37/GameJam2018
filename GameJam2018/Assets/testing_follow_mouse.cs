using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class testing_follow_mouse : MonoBehaviour {

	bool following = false;
	string t_name = "Generic";
	public int health = 100;
	static int c = 0;

	// Use this for initialization
	void Start () {
		t_name = "Mouse Follower #" + c;
		c++;
	}

	void OnMouseDown(){
		following = !following;
	}

	void die(){
		Debug.Log (t_name+" has died!!");
	}

	void attack(int damage){
		health -= damage;
		if(health <= 0){
			die();
		}
	}

	// Update is called once per frame
	void Update () {
		if(following){
			Vector3 mouse = Input.mousePosition;
			mouse = Camera.main.ScreenToWorldPoint (mouse);
			mouse.z = transform.position.z;

			transform.position = mouse;
		}
	}
}
