using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	private GameObject[] remainingMinions;

	void Start () {

	}
	void LateUpdate()
	{
		remainingMinions = GameObject.FindGameObjectsWithTag ("Minion");
		if (remainingMinions [0].transform.position.y < Camera.main.transform.position.y && Camera.main.transform.position.y > -7.3) {

			Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3 (0, remainingMinions [0].transform.position.y, -10), 3 * Time.deltaTime);
		}
	}
}
