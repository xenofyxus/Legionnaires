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

	public GameObject minionType1;
	public GameObject minionType2;
	public GameObject minionType3;

	private int waveNumber = 0;
	private GameObject[] waveObjList = new GameObject[3]; 


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
		waveObjList [0] = minionType1;
		waveObjList [1] = minionType2;
		waveObjList [2] = minionType3;
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
				Instantiate (waveObjList[waveNumber], new Vector2 (x, y), transform.rotation);
			}
		}
		if(waveObjects.Length > numberOfUnits){
			instantiatetimer = 10f;
			waveNumber++;
			if (waveNumber == waveObjList.Length) {
				waveNumber = 0;
			}
			newWave = false;
		}
	}
}
