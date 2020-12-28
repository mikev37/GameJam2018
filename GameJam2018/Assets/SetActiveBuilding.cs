using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveBuilding : MonoBehaviour {

	public GameObject prefab;

	public void setActiveBuilding(){
		PlayerData.playerData.activeConstruction = prefab;
	}
}
