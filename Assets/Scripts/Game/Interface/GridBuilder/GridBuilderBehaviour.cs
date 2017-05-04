using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface.GridBuilder
{
	public class GridBuilderBehaviour : MonoBehaviour
	{

		float offSetX = 1.48f;
		float offSetY = 1.47f;
		public GameObject[] towersAvailable = new GameObject[6];
		public GameObject menu;
		int whichSpotX;
		int whichSpotY;
		Vector2 placeTower;
		GameObject[,] towerGridPos = new GameObject[6, 7];
		List<Vector2> towerPositions = new List<Vector2>();
		List<GameObject> towersSpawned = new List<GameObject> ();
		List<GameObject> towersSpawnedNotClone = new List<GameObject> ();
		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{
			if (Input.GetMouseButtonDown (0) && GameObject.FindGameObjectsWithTag ("TowerMenu").Length == 0) {
				Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				whichSpotX = Mathf.FloorToInt ((mouseWorldPos.x + 3.8f) / offSetX);
				whichSpotY = Mathf.FloorToInt (mouseWorldPos.y / offSetY) - 1;
				placeTower = new Vector2 (-3.0f + (whichSpotX * 1.48f), 2.18f + (whichSpotY * 1.47f));
				if (0 <= whichSpotX && whichSpotX <= 4 && 0 <= whichSpotY && whichSpotY <= 6 && towerGridPos [whichSpotX, whichSpotY] == null) {
					Instantiate (menu, placeTower, transform.rotation);
				}
			}
            if (TowerMenu.TowerMenuBehaviour.currentMenuItem != -1 && TowerMenu.TowerMenuBehaviour.placeTower == true) {
				spawnTower ();
                TowerMenu.TowerMenuBehaviour.currentMenuItem = -1;
                TowerMenu.TowerMenuBehaviour.placeTower = false;
			}
		}

		void spawnTower ()
		{
            towersSpawned.Add(Instantiate (towersAvailable [TowerMenu.TowerMenuBehaviour.currentMenuItem], placeTower, transform.rotation));
			towerPositions.Add (placeTower);
            towersSpawnedNotClone.Add (towersAvailable [TowerMenu.TowerMenuBehaviour.currentMenuItem]);
            towerGridPos [whichSpotX, whichSpotY] = towersAvailable [TowerMenu.TowerMenuBehaviour.currentMenuItem];
		}

		public void Reset (){
			for (int i = 0; i < towersSpawned.Count; i++) {
				GameObject.Destroy (towersSpawned [i]);
				towersSpawned.RemoveAt (i);
				towersSpawned.Insert (i, Instantiate (towersSpawnedNotClone[i], towerPositions [i], transform.rotation));
			}
		}
	}
}