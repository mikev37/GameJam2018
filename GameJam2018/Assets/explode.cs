using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour {

	public int damage;
	private Vector3 shrinkVector;
	// Use this for initialization
	void Start () {
		shrinkVector = -2 * Vector3.one;
	}
	void OnCollisionStay2D(Collision2D info){
		if (info.gameObject.GetComponent<destructable> () != null) {
			info.gameObject.GetComponent<destructable> ().attack (damage);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x <= 0) {
			Destroy (gameObject);
		}
		shrinkVector += Vector3.one * 5 * Time.deltaTime;
		transform.localScale -= shrinkVector * Time.deltaTime * 10;
	}
}
