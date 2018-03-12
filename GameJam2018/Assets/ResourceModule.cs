using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceModule : Module {
	public override void processOnGrid(NetworkComponent network){
		//Do nothing?

		//maybe throw an error
	}
	public float money;

	public float moneyRemaining;

	public float extractMoney(){
		if (moneyRemaining > money) {
			moneyRemaining -= money;
			return money;
		}else {
			float temp = moneyRemaining;
			moneyRemaining = 0;
			return temp;
		}
	}
}
