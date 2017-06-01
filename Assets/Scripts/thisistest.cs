using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thisistest : MonoBehaviour {

	// Use this for initialization
	public void OnFuckingClick () {
		if (Time.timeScale == 1.0F)
			Time.timeScale = 1.5F;
		else if (Time.timeScale == 1.5F)
			Time.timeScale = 5.0F;
		else
			Time.timeScale = 1.0F;
	}
}
