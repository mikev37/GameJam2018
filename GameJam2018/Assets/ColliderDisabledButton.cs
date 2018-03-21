using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderDisabledButton : MonoBehaviour {
	public LayerMask mask;
	private Button btn;
	public string debug;
	public int size = 10;
	// Use this for initialization
	void Start () {
		btn = GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
		btn.interactable = Physics2D.OverlapBoxAll (this.transform.position, Vector2.one * size, 0, mask.value).Length == 0;

	}
}
