using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderButton : MonoBehaviour {
	public int id;
	private Sprite previewPic;
	private GameObject buildingObj;
	private string previewName;
	public Image img;

	public void load(string res){
		//load picture, name, and prefab

		buildingObj = Resources.Load("GameJam2018/prefabs/"+res) as GameObject;
		previewPic = buildingObj.GetComponentInChildren<SpriteRenderer> ().sprite;
		//set buttom text and image
		GetComponentInChildren<Text>().text = res;
		img.sprite = previewPic;
	}
}
