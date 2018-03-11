using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret_barrel : MonoBehaviour {
	public Material lazer_material;

	float fire_rate = 0.3f;
	float next_fire = 0f;

	// Use this for initialization
	void Start () {

	}

	void shootAt(Vector3 target){
		if(next_fire <= 0){
			next_fire = fire_rate;
			Vector3 start = transform.position;

			GameObject myLine = new GameObject();
			myLine.transform.position = start;
			myLine.AddComponent<LineRenderer>();

			LineRenderer lr = myLine.GetComponent<LineRenderer>();
			lr.material = new Material(lazer_material);
			//lr.SetColors(color, color);
			start.z += 0.02f;
			target.z += 0.02f;
			lr.startWidth = 0.06f;
			lr.endWidth = 0.06f;
			lr.SetPosition(0, start);
			lr.SetPosition(1, target);

			GameObject.Destroy(myLine, 0.05f);
		}
	}

	// Update is called once per frame
	void Update () {
		Vector3 mouse = Input.mousePosition;
		mouse = Camera.main.ScreenToWorldPoint (mouse);
		mouse.z = transform.position.z;

		transform.LookAt(mouse);

		next_fire -= Time.deltaTime;
		if(Input.GetMouseButton(0)){
			shootAt (mouse);
		}
	}
}
