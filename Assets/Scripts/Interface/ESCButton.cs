﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCButton : MonoBehaviour {

	public GameObject legionnaireSpawner;

	void Start () {
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			SceneManager.LoadScene (0);
		}
	}
}
