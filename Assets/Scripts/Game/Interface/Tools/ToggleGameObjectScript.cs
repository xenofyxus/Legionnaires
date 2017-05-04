using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObjectScript : MonoBehaviour {

	public GameObject current;

	public void toggleMe()
	{
		current.SetActive (!current.activeSelf);
	}

}
