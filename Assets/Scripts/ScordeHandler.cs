using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScordeHandler : MonoBehaviour {

	public GameObject scoreTxtObj;
	float tmr_ = 0;
	float tmrScoreDelay = .2f;
	float scoreMultiplier = 3;
	public static int score = 0;
	public static bool canScore = true;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		tmr_ += Time.deltaTime;
		if (tmr_ > tmrScoreDelay && canScore) {
			score += (int)(scoreMultiplier * (1+tmrScoreDelay));
			scoreTxtObj.GetComponent<Text>().text = score.ToString ();
			tmr_ = 0;
		}
	}
	public static void AddCoin(int value){
		score += value;
	}
}
