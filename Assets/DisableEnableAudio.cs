using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnableAudio : MonoBehaviour {

	public GameObject audio;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		AudioSource audioSource = audio.GetComponent<AudioSource> ();
		audioSource.enabled = Settings.Current.AudioEnabled;
	}
}
