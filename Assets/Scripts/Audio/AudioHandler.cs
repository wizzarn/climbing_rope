using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {

	AudioSource audioCanonSource;
	AudioSource coinSource;
	AudioSource backgroundSource;
	public GameObject canonBallAudioObj;
	public GameObject backgroundAudioObj;
	public GameObject coinAudioObj;

	void Start () {
		audioCanonSource = canonBallAudioObj.GetComponent<AudioSource> ();
		coinSource = coinAudioObj.GetComponent<AudioSource> ();

		if (backgroundAudioObj) {
			backgroundSource = backgroundAudioObj.GetComponent<AudioSource> ();
			backgroundSource.Play ();
		}

	}
	public void InstanceCanonBallSound(){
		audioCanonSource.Play ();
	}
	public void InstanceCoinSound(){
		coinSource.Play ();
	}
}
