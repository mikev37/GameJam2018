using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

	bool aquireTarget = false;
	public float speed;
	Vector3 target;

	// Use this for initialization
	void Start () {
		target = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (aquireTarget) {
			//Gets the world position of the mouse on the screen        
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			target.x = mousePosition.x;
			target.y = mousePosition.y;
			target.z = transform.position.z;

			if (Input.GetButtonUp ("Fire1")) {
				aquireTarget = false;
			}
		} else {
			//Gets the world position of the mouse on the screen        
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );

			//Checks whether the mouse is over the sprite
			bool overSprite = this.GetComponentInChildren<SpriteRenderer>().bounds.Contains( mousePosition );

			//If it's over the sprite
			if (overSprite) {
				//If we've pressed down on the mouse (or touched on the iphone)
				if (Input.GetButtonDown ("Fire1")) {
					aquireTarget = true;
				}
			}
		}

		float step = Time.deltaTime * speed;

		this.transform.position = Vector3.MoveTowards (this.transform.position, target, step);

	}
}
