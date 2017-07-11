using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveChildren : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ActivateChildren(bool value){
		this.gameObject.SetActive(value);
	}
}
