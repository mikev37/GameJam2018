using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAtTouch : MonoBehaviour {
	public LayerMask mask;
	public int price = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerData.playerData.activeConstruction != null){
			Vector3 target = Vector3.zero;
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			target.x = mousePosition.x;
			target.y = mousePosition.y;
			target.z = transform.position.z;
			this.transform.position = target;

			if (Input.GetMouseButtonDown(0)) {

				bool overlap = Physics2D.OverlapBoxAll (target,Vector2.one * 2, 0, mask.value).Length == 0;

				bool money = NetworkComponent.money > price;

				if (overlap && money) {

					NetworkComponent.money -= price;

					Object.Instantiate (PlayerData.playerData.activeConstruction, target, Quaternion.identity, null);
					//PlayerData.playerData.activeConstruction = null;

			
				}

			}
		}
	}
}
