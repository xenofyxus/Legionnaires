using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void Pause(){
		Time.timeScale = 0;
	}

	public void UnPause(){
		Time.timeScale = 1;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
