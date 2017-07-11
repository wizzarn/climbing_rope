using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectsGenerator : MonoBehaviour {

	public GameObject fallObj;
	public float minTimeThrow = 2;
	public float maxTimeThrow = 5;
	float timeThrow=0;
	float _tmr;
	public GameObject limitLeft;
	public GameObject limitRight;
	float limitMinX;
	float limitMaxX;
	void Start () {
		limitMinX = limitLeft.transform.position.x;
		limitMaxX = limitRight.transform.position.x;
		timeThrow = Random.Range(minTimeThrow, maxTimeThrow);
	}
	

	void Update () {
		_tmr += Time.deltaTime;
		if (_tmr > timeThrow) {
			_tmr = 0;
			InstantiateNewObject ();
		}
	}
	void InstantiateNewObject(){
		Vector3 respawnPosition = this.gameObject.transform.position;
		respawnPosition.x = Random.Range (limitMinX,limitMaxX);
		GameObject newObject = (GameObject)Instantiate (fallObj,respawnPosition,Quaternion.identity);
	}
}
