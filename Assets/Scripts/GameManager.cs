using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static bool isGameOver = false;
	public GameObject gameOverObj;
	public GameObject successObj;
	void Start () {
		isGameOver = false;
		ScordeHandler.canScore = true;
		ScordeHandler.score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void SetGameOver(){
		gameOverObj.SetActive (true);
		isGameOver = true;
		ScordeHandler.canScore = false;
	}

	public void ResetMainLevel(){
		Application.LoadLevel (0);
	}
	public void SetSuccess(){
		successObj.SetActive (true);
		isGameOver = true;
		ScordeHandler.canScore = false;
	}
}
