using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyReadout : MonoBehaviour {

	Text readout;

	// Use this for initialization
	void Start () {
		readout = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		readout.text = "" + (int)NetworkComponent.money;
	}
}
