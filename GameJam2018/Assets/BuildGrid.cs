using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGrid : MonoBehaviour {
	public GameObject gridButton;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 20; i++) {
			for (int j = 0; j < 20; j++) {
				float size = 3;// gridButton.GetComponent<RectTransform> ().rect.width;
				Vector3 position =  Vector3.right * i * size + Vector3.down * j * size;
				GameObject go = Object.Instantiate (gridButton, position, Quaternion.identity, transform);//transform,
				go.GetComponent<RectTransform>().position = position;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
