using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization

	GameObject objExplosion;
	void Start () {
		Destroy (this.gameObject, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {	
	}

}
