using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emitter : MonoBehaviour {
	public GameObject go;
	float timer;
	public int rate;
	// Use this for initialization
	void Start () {
		timer = rate;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			timer = rate;
			Object.Instantiate (go, transform.position+Random.insideUnitSphere * 1, transform.rotation, null);
		}
	}
}
