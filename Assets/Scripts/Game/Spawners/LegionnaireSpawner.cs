//Legionnaires
//Author: Daniel Karlsson, Victor Carle
//Updates:
//Date: 24/4-17

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionnaireSpawner : MonoBehaviour {

	public GameObject towerToSpawn;

	public static List<GameObject> towerSpawned = new List<GameObject>();

	void Start () {

	}

	void Update () {

		//TODO Drag and drop Legionnaires

		if (Input.GetMouseButtonDown (0)) {

			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			mouseWorldPos.z = gameObject.transform.position.z;

			towerSpawned.Add(Instantiate(towerToSpawn, mouseWorldPos, transform.rotation));
		}
		if (Input.GetKeyDown ("y")) {
			GameObject.Destroy (towerSpawned[0]);
			towerSpawned.RemoveAt(0);
		}

	}
}
