using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVisibleHandler : MonoBehaviour {

	public GameManager gameManager;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "rope_parent") {
			other.gameObject.GetComponentInChildren<ActiveChildren> (true).ActivateChildren (true);
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "main_character" || other.gameObject.tag == "rope_parent") {
			if (other.gameObject.GetComponentInChildren<ActiveChildren>())
				other.gameObject.GetComponentInChildren<ActiveChildren> (true).ActivateChildren (false);
		}
		if (other.gameObject.tag == "main_character") {
			gameManager.SetGameOver ();
		}
	}
}
