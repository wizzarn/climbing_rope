using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceLeftCalculator : MonoBehaviour {

	public GameObject topObj;
	public GameObject bottomObj;
	float tmr_ = 0;
	float tmr = .5f; // update every .5 sec
	HealthBarController healtbarController;
	float maxDistance = 0;
	void Start () {
		maxDistance = (topObj.transform.position.y - bottomObj.transform.position.y);
	}

	void Update () {
		tmr_ += Time.deltaTime;
		if (tmr_ > tmr){
			tmr = 0;
			float actualDistance = (topObj.transform.position.y - bottomObj.transform.position.y);
			float result = 100 - ((actualDistance * 100) / maxDistance);
			HealthBarController.Instance.setHealth (result);
		}
	}
}
