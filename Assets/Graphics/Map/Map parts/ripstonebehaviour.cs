using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ripstonebehaviour : MonoBehaviour {
	
	public void onClick () 
	{
		SceneManager.LoadScene(0);
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene(0);
		}
	}
}
