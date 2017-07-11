using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public Vector3 translatePosition = new Vector3(0,.01f,0);
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (translatePosition);
	}
}
