/// Legionnaires
/// <summary>
/// Minion spawner.
/// </summary>

//Author: Daniel Karlsson, Victor Carle
//Updates:
//Date: 24/4-17


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour {

	public GameObject minionToSpawn;

	private double timer = 0.0;
	private int i = 0;

	private int MinX = -3;
	private int MaxX = 3;
	private int MinY = 16;
	private int MaxY = 17;

	void Start () {

	}

	void Update () {

		timer += Time.deltaTime;

		if (timer > 0.7 && i < 10) {

			int spawnsPerTic = 0;

			while (spawnsPerTic < 4) {

				int x = Random.Range (MinX, MaxX);
				int y = Random.Range (MinY, MaxY);

				Instantiate (minionToSpawn, new Vector2 (x, y), transform.rotation);
				i++;
				spawnsPerTic++;
			}
			timer = 0.0;
		}

	}
}
