using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour {

	float speed = 2.0f;

	void Update () {
		Quaternion localRotation = transform.localRotation;
		localRotation.z = Mathf.Sin(Time.time * speed) * 0.2f;
		transform.localRotation = localRotation;
	}

}
