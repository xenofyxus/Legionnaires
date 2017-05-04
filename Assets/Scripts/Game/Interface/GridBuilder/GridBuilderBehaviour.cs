using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface.GridBuilder
{
	public class GridBuilderBehaviour : MonoBehaviour
	{
		[System.Serializable]
		public class towerList{
			public GameObject[] upgradedTowers;
		}

		float offSetX = 1.48f;
		float offSetY = 1.47f;

		public towerList[] towersAvailable = new towerList[6];
		public GameObject menu;
		public GameObject sell;
		int whichSpotX;
		int whichSpotY;
		Vector2 placeTower;
		GameObject[,] towerGridPos = new GameObject[5, 7];
		int[,] originalTower = new int[5, 7];
		Vector2[,] originalVector = new Vector2[5, 7];
		int[,] whatUpgrade = new int[5, 7];
		bool upgradeable;
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
				else if(0 <= whichSpotX && whichSpotX <= 4 && 0 <= whichSpotY && whichSpotY <= 6 && towerGridPos [whichSpotX, whichSpotY] != null){
					Instantiate (sell, placeTower, transform.rotation);
					TowerMenu.TowerMenuBehaviour.placeTower = false;
					TowerMenu.TowerMenuBehaviour.sellTower = true;
					upgradeable = true;
				}
			}
			if (TowerMenu.TowerMenuBehaviour.currentMenuItem != -1 && TowerMenu.TowerMenuBehaviour.placeTower == true) {
				spawnTower ();
				TowerMenu.TowerMenuBehaviour.currentMenuItem = -1;
				TowerMenu.TowerMenuBehaviour.placeTower = false;
			}
			if (TowerMenu.TowerMenuBehaviour.currentMenuItem == 0 && TowerMenu.TowerMenuBehaviour.sellTower == true) {
				sellTower ();
				TowerMenu.TowerMenuBehaviour.currentMenuItem = -1;
				TowerMenu.TowerMenuBehaviour.sellTower = false;
			}
			if (TowerMenu.TowerMenuBehaviour.currentMenuItem == 1 && upgradeable == true) {
				upgradeable = false;
				upgradeTower ();
				TowerMenu.TowerMenuBehaviour.currentMenuItem = -1;
				TowerMenu.TowerMenuBehaviour.sellTower = false;
			}
		}

		void spawnTower ()
		{
			towerGridPos [whichSpotX, whichSpotY] = Instantiate (towersAvailable [TowerMenu.TowerMenuBehaviour.currentMenuItem].upgradedTowers[0], placeTower, transform.rotation);
			originalTower [whichSpotX, whichSpotY] = TowerMenu.TowerMenuBehaviour.currentMenuItem;
			originalVector [whichSpotX, whichSpotY] = placeTower;
			whatUpgrade [whichSpotX, whichSpotY] = 0;
		}

		void sellTower() 
		{
			GameObject.Destroy(towerGridPos [whichSpotX, whichSpotY]);
			towerGridPos[whichSpotX, whichSpotY] = null;
		}

		public void Reset (){
			for (int i = 0; i < towerGridPos.GetLength (0); i++) {
				for (int j = 0; j < towerGridPos.GetLength (1); j++) {
					if (towerGridPos [i, j] != null) {
						GameObject.Destroy (towerGridPos [i, j]);
						towerGridPos [i, j] = Instantiate(towersAvailable[originalTower[i,j]].upgradedTowers[whatUpgrade[i,j]], originalVector[i,j], transform.rotation);
					}
				}
			}
		}
		void upgradeTower(){
			sellTower ();
			if (2 > whatUpgrade [whichSpotX, whichSpotY]) {
				whatUpgrade [whichSpotX, whichSpotY] += 1;
			}
			towerGridPos [whichSpotX, whichSpotY] = Instantiate (towersAvailable [originalTower[whichSpotX, whichSpotY]].upgradedTowers[whatUpgrade[whichSpotX, whichSpotY]], originalVector [whichSpotX, whichSpotY], transform.rotation);
		}
	}
}