using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing_follow_mouse : MonoBehaviour {

	bool following = false;

	// Use this for initialization
	void Start () {
		
	}

	void OnMouseDown(){
		following = !following;
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
