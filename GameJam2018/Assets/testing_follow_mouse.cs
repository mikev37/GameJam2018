using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class testing_follow_mouse : MonoBehaviour {

	bool following = true;
	string t_name = "Generic";
	public int health = 100;
	static int c = 0;
	public string debug;
	public Camera controlCam;

	// Use this for initialization
	void Start () {
		t_name = "Mouse Follower #" + c;
		c++;
		if(controlCam == null)
			controlCam = Camera.main;
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
			debug = mouse.ToString ();
			if (controlCam.targetTexture != null) {
				mouse.x = mouse.x * controlCam.targetTexture.width / Screen.width;
				mouse.y = mouse.y * controlCam.targetTexture.height / Screen.height;
			}
			mouse = controlCam.ScreenToWorldPoint (mouse);

			mouse.z = transform.position.z;

			transform.position = mouse;
		}
	}
}
