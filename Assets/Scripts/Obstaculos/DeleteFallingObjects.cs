using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteFallingObjects : MonoBehaviour {

	// Use this for initialization
	public GameManager gameManager;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "falling_object") {
			Destroy (other.gameObject);
		}else if (other.gameObject.tag == "main_character"){
			gameManager.SetGameOver ();
		}
	}
}
