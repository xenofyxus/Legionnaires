/// Legionnaires
/// <summary>
/// Minion spawner.
/// summary>

//Author: Daniel Karlsson, Victor Carle
//Date: 24/4-17
//Updates: Anton Anderzen, Victor Carle
//Added dynamic wave count and minion amount/wave.
//Date: 26/4-17

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
	
	private int waveNumber = 0;
	[Tooltip ("What minion and how many of that minion to Spawn")]
	public List<Game.Spawners.WaveObject> waveObjList = new List<Game.Spawners.WaveObject> ();

	public GameObject legionnaireSpawner;
	private GameObject[] waveObjects;

	private bool newWave;
	private int numberOfUnitsSpawned = 0;
	private float instantiateTimer = 5f;
	//time until next wave starts.

	private float MinX = -3;
	private float MaxX = 3;
	private float MinY = 16;
	private float MaxY = 17;

	void Start ()
	{
	}

	void Update ()
	{
		waveObjects = GameObject.FindGameObjectsWithTag ("Minion");
		if (waveObjects.Length == 0) {
			legionnaireSpawner.GetComponent<LegionnaireSpawner> ().Reset ();
			newWave = true;
		}
		if (newWave) {
			NextWave ();
		}
	}


	void NextWave ()
	{

		for (int i = 0; i < Mathf.Ceil (((float)waveObjList [waveNumber].amountOfMinions) / 4); i++) {
			instantiateTimer -= Time.deltaTime;
			if (instantiateTimer <= 0) {
				instantiateTimer = 2.5f;
				for (int j = 0; j < 4; j++) {		// Spawning 4 units per "instantiateTimer"-delay
					if (numberOfUnitsSpawned >= waveObjList [waveNumber].amountOfMinions) {
						break;
					}
					float x = Random.Range (MinX, MaxX);
					float y = Random.Range (MinY, MaxY);
					Instantiate (waveObjList [waveNumber].minion, new Vector2 (x, y), transform.rotation);
					numberOfUnitsSpawned++;

				}

			}
		}
		if (numberOfUnitsSpawned >= waveObjList [waveNumber].amountOfMinions) { // When done spawning goes on to next wave.
			instantiateTimer = 5f;
			waveNumber++;
			numberOfUnitsSpawned = 0;
			if (waveNumber == waveObjList.Count) {
				waveNumber = 0;
			}
			newWave = false;
		}
	}
}

