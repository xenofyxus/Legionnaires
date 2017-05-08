using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObjectScript : MonoBehaviour {

	public List<GameObject> list = new List<GameObject>();

	public void toggleMe()
	{
		foreach(GameObject index in list)
		{
			index.SetActive(!index.activeSelf);
		}
	}

}
