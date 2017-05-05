using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Interface.GridBuilder
{
	public class GridBuilderBehaviour : MonoBehaviour
	{
		[System.Serializable]
		public class towerList{
			public GameObject[] upgradedTowers;
		}

			//Transform.find("x,y).GetComponent<Image>().sprite;
		float offSetX = 1.48f;
		float offSetY = 1.47f;

		int maxSupply;
		int currentSupply;

		int currentGold;

		public towerList[] towersAvailable = new towerList[6];
		public GameObject menu;
		public GameObject sell;
		int whichSpotX;
		int whichSpotY;
		Vector2 placeTower;
		GameObject[,] towerGridPos = new GameObject[5, 7]; //Instantiated tower
		GameObject[,] towerGridPosCopy = new GameObject[5, 7]; //Which exact tower
		int[,] originalTower = new int[5, 7]; //Which type of tower
		int[,] whatUpgrade = new int[5, 7]; //Which level the tower has
		Vector2[,] originalVector = new Vector2[5, 7]; //Tile placed on
		bool upgradeable;
		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{ 
			//The code in update is all about mouse positions and choosing towers
			if (Input.GetMouseButtonDown (0) && GameObject.FindGameObjectsWithTag ("TowerMenu").Length == 0) {				
				Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				whichSpotX = Mathf.FloorToInt ((mouseWorldPos.x + 3.8f) / offSetX);
				whichSpotY = Mathf.FloorToInt (mouseWorldPos.y / offSetY) - 1;
				placeTower = new Vector2 (-3.0f + (whichSpotX * 1.48f), 2.18f + (whichSpotY * 1.47f));
				if (0 <= whichSpotX && whichSpotX <= 4 && 0 <= whichSpotY && whichSpotY <= 6 && towerGridPosCopy [whichSpotX, whichSpotY] == null) {
					Instantiate (menu, placeTower, transform.rotation);
				}
				else if(0 <= whichSpotX && whichSpotX <= 4 && 0 <= whichSpotY && whichSpotY <= 6 && towerGridPosCopy [whichSpotX, whichSpotY] != null){
					Instantiate (sell, placeTower, transform.rotation);
					TowerMenu.TowerMenuBehaviour.placeTower = false;
					TowerMenu.TowerMenuBehaviour.sellTower = true;
					upgradeable = true;
				}
			}
			if (TowerMenu.TowerMenuBehaviour.currentMenuItem != -1 && TowerMenu.TowerMenuBehaviour.placeTower == true) {
				createTower ();
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
			maxSupply = Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.SupplyMax;
			currentSupply = Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Supply;
			currentGold = Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Gold;
		}

		//Creates the first version of the tower as you buy it
		void createTower ()
		{
			if (currentSupply + towersAvailable [TowerMenu.TowerMenuBehaviour.currentMenuItem].upgradedTowers [0].GetComponent<Game.Units.LegionnaireBehaviour> ().Supply <= maxSupply) {
				if(Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.TryPayingGold(towersAvailable [TowerMenu.TowerMenuBehaviour.currentMenuItem].upgradedTowers [0].GetComponent<Game.Units.LegionnaireBehaviour> ().Cost)){
					transform.Find (whichSpotX + "/" + whichSpotY).GetComponent<Image> ().sprite = towersAvailable [TowerMenu.TowerMenuBehaviour.currentMenuItem].upgradedTowers [0].GetComponent<SpriteRenderer> ().sprite;
					transform.Find (whichSpotX + "/" + whichSpotY).GetComponent<Image> ().color = Color.white;
					towerGridPos [whichSpotX, whichSpotY] = towersAvailable [TowerMenu.TowerMenuBehaviour.currentMenuItem].upgradedTowers [0];
					towerGridPosCopy [whichSpotX, whichSpotY] = towersAvailable [TowerMenu.TowerMenuBehaviour.currentMenuItem].upgradedTowers [0];
					originalTower [whichSpotX, whichSpotY] = TowerMenu.TowerMenuBehaviour.currentMenuItem;
					originalVector [whichSpotX, whichSpotY] = placeTower;
					whatUpgrade [whichSpotX, whichSpotY] = 0;
					Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Supply += towerGridPosCopy [whichSpotX, whichSpotY].GetComponent<Game.Units.LegionnaireBehaviour> ().Supply;
				}
			}
		}

		//sells your tower
		void sellTower() 
		{
			transform.Find (whichSpotX +"/"+ whichSpotY).GetComponent<Image> ().sprite = null;
			transform.Find (whichSpotX +"/"+ whichSpotY).GetComponent<Image> ().color = Color.clear;

			Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Supply -= towerGridPosCopy [whichSpotX, whichSpotY].GetComponent<Game.Units.LegionnaireBehaviour> ().Supply;
			Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Gold += towerGridPosCopy [whichSpotX, whichSpotY].GetComponent<Game.Units.LegionnaireBehaviour> ().Cost;
			GameObject.Destroy(towerGridPos [whichSpotX, whichSpotY]);
			towerGridPos[whichSpotX, whichSpotY] = null;
			towerGridPosCopy[whichSpotX, whichSpotY] = null;
		}

		//Instantiates all the sprites into their character
		public void Reset (){
			for (int i = 0; i < 5; i++) {
				for (int j = 0; j < 7; j++) {
					if (towerGridPosCopy [i, j] != null) {
						transform.Find (i +"/"+ j).GetComponent<Image> ().sprite = null;
						transform.Find (i +"/"+ j).GetComponent<Image> ().color = Color.clear;
						GameObject.Destroy (towerGridPos [i, j]);
						towerGridPos [i, j] = Instantiate(towersAvailable[originalTower[i, j]].upgradedTowers[whatUpgrade[i, j]], originalVector[i, j], transform.rotation);
					}
				}
			}
		}

		//After the wave has ended displays sprites of the characters on the grid
		public void ResetSprite (){
			for (int i = 0; i < 5; i++) {
				for (int j = 0; j < 7; j++) {
					if (towerGridPosCopy [i, j] != null) {
						GameObject.Destroy(towerGridPos[i,j]);
						towerGridPos [i, j] = null;
						transform.Find (i + "/" + j).GetComponent<Image> ().sprite = towerGridPosCopy [i, j].GetComponent<SpriteRenderer> ().sprite;
						transform.Find (i + "/" + j).GetComponent<Image> ().color = Color.white;
					}
				}
			}
		}

		//Upgrades tower into next versionf
		void upgradeTower(){
			if (2 > whatUpgrade [whichSpotX, whichSpotY]) {
				whatUpgrade [whichSpotX, whichSpotY] += 1;
			}
			sellTower ();
			transform.Find (whichSpotX +"/"+ whichSpotY).GetComponent<Image> ().sprite = null;
			transform.Find (whichSpotX +"/"+ whichSpotY).GetComponent<Image> ().color = Color.clear;

			towerGridPos [whichSpotX, whichSpotY] = towersAvailable [originalTower[whichSpotX, whichSpotY]].upgradedTowers[whatUpgrade[whichSpotX, whichSpotY]];
			towerGridPosCopy [whichSpotX, whichSpotY] = towersAvailable [originalTower [whichSpotX, whichSpotY]].upgradedTowers [whatUpgrade [whichSpotX, whichSpotY]];
			Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Supply += towerGridPosCopy [whichSpotX, whichSpotY].GetComponent<Game.Units.LegionnaireBehaviour> ().Supply;
			Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Gold -= towerGridPosCopy [whichSpotX, whichSpotY].GetComponent<Game.Units.LegionnaireBehaviour> ().Cost;
		
			transform.Find (whichSpotX + "/" + whichSpotY).GetComponent<Image> ().sprite = towerGridPosCopy [whichSpotX, whichSpotY].GetComponent<SpriteRenderer> ().sprite;
			transform.Find (whichSpotX + "/" + whichSpotY).GetComponent<Image> ().color = Color.white;

		}
	}
}