using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffects : MonoBehaviour {

	public bool isEnabled = true;
	public float tmr = .001f;
	public float tmr_;
	public bool flag = false;
	Light light;
	void Start () {
		light = this.gameObject.GetComponent<Light> ();
		if (Random.Range (1, 10) > 5) {
			flag = true;
			light.intensity = 0;
		} else {
			flag = false;
			light.intensity = 2;
		}
	}
	

	void Update () {
		tmr_ += Time.deltaTime;
		if (tmr_ > tmr) {
			tmr_ = 0;
			if (!flag){
				if (light.intensity > 0)
					light.intensity -= .2f;
				else
					flag = true;
			}else{
				if (light.intensity < 2) 
					light.intensity += .2f;
				else
					flag = false;
			}
		}

	}
}
