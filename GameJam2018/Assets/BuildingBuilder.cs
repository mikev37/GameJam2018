using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBuilder : MonoBehaviour {

	public Text detailText;

	// Use this for initialization
	void Start () {
		detailText = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerData.playerData.activeConstruction != null) {
			detailText.text = PlayerData.playerData.activeConstruction.name;
		} else {
			detailText.text = "";
		}
	}
}
