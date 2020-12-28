using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

	public static PlayerData playerData;

	void Start(){
		PlayerData.playerData = this;
	}

	public int circles;
	public int tris;
	public int squares;

	public GameObject activeConstruction;


}
