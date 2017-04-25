/// Legionnaires
/// <summary>
/// Minion spawner.
/// summary>

//Author: Daniel Karlsson, Victor Carle
//Updates:
//Date: 24/4-17


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour {

	public GameObject minionToSpawn;
	public GameObject legionnaireSpawner;
	private GameObject[] waveObjects;

	private bool newWave;

	private int numberOfUnits = 25;
	private float instantiatetimer = 10f; //time until next wave starts.

	private float MinX = -3;
	private float MaxX = 3;
	private float MinY = 16;
	private float MaxY = 17;

	void Start () {
	}

	void Update () {
		waveObjects = GameObject.FindGameObjectsWithTag ("Minion");
		if (waveObjects.Length == 0) {
			legionnaireSpawner.GetComponent<LegionnaireSpawner> ().Reset ();

			newWave = true;
		}
		if (newWave) {
			NextWave ();
		}
	}
	void NextWave(){
		instantiatetimer -= Time.deltaTime;
		if (instantiatetimer <= 0) {
			instantiatetimer = 1f;
			for (int i = 0; i < 4; i++) {
				float x = Random.Range (MinX, MaxX);
				float y = Random.Range (MinY, MaxY);
				Instantiate (minionToSpawn, new Vector2 (x, y), transform.rotation);
			}
		}
		if(waveObjects.Length > numberOfUnits){
			instantiatetimer = 10f;
			newWave = false;
		}
	}
}
