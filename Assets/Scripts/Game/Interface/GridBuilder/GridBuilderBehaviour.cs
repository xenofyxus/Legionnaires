using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Interface.GridBuilder
{
	public class GridBuilderBehaviour : MonoBehaviour
	{
		[System.Serializable]
		public class TowerList
		{
			public GameObject[] upgradedTowers;
		}

		[System.Serializable]
		public class TowerInfo
		{
			public GameObject thisTower;
			public Vector2 position;
			public int towerType;
			public int cost;
			public int totalSupply;
			public int upgrade;
			public int waveCreated = -1;
			public int totalCost;
			public int scoreReward;
		}

		float offSetX = 1.48f;
		float offSetY = 1.5f;

		int maxSupply;
		int currentSupply;
		public int towerX;
		public int towerY;
		bool newTower;

		public GameObject buyTowers;
		public GameObject sellOrUpgrade;
		public GameObject onlySell;
		public GameObject wizardSpecial;
		GameObject gridTiles;
		Vector2 placeTower;
		public TowerList[] humanTowers = new TowerList[6];
		public TowerList[] orcTowers = new TowerList[6];
		public TowerList[] towersUsed = new TowerList[6];
		GameObject[,] towerGridPos = new GameObject[5, 6];
		public TowerInfo[,] towerGridPosCopy = new TowerInfo[5, 6];

		private GameObject buildMessage;
		// Use this for initialization
		void Start ()
		{
			buildMessage = transform.Find("ConfirmationText").gameObject;
			buildMessage.SetActive (false);
			if (Settings.Current.LegionnaireBuilder == LegionnaireBuilder.Human) {
				towersUsed = humanTowers;
			}
			if (Settings.Current.LegionnaireBuilder == LegionnaireBuilder.Orc) {
				towersUsed = orcTowers;
			}
			instantiateTower ();
			gridTiles = GameObject.Find ("GridTiles");
		}

		public static GridBuilderBehaviour Current{ get; private set; }

		// Update is called once per frame
		void Update ()
		{ 
			if (Input.GetMouseButtonDown (0) && GameObject.FindGameObjectsWithTag ("TowerMenu").Length == 0) {	
				Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				towerX = Mathf.FloorToInt ((mouseWorldPos.x + 3.8f) / offSetX);
				towerY = Mathf.FloorToInt ((mouseWorldPos.y / offSetY) - 1f);
				placeTower = new Vector2 (-3.0f + (towerX * 1.48f), 2.4f + (towerY * 1.47f));
				if (0 <= towerX && towerX <= 4 && 0 <= towerY && towerY <= 5) {
					GameObject currentTower = towerGridPosCopy [towerX, towerY].thisTower;
					if (currentTower == null) {
						Instantiate (buyTowers, placeTower, transform.rotation);
						newTower = true;
					} else if (currentTower != null) {
						TooltipBar.TowerPanel.TowerPanelBehaviour.Current.SetUnit (currentTower.GetComponent<Units.LegionnaireBehaviour> ());
						if (currentTower == towersUsed [2].upgradedTowers [0]) {
							Instantiate (wizardSpecial, placeTower, transform.rotation);
						} else if (towersUsed [towerGridPosCopy [towerX, towerY].towerType].upgradedTowers.Length - 1 == towerGridPosCopy [towerX, towerY].upgrade) {
							Instantiate (onlySell, placeTower, transform.rotation);
						} else {
							Instantiate (sellOrUpgrade, placeTower, transform.rotation);
						}
						newTower = false;
					}
				}
			}
			if (TowerMenu.TowerMenuBehaviour.placeTower == true && TowerMenu.TowerMenuBehaviour.currentMenuItem != -1) {
				createTower ();
				Interface.TooltipBar.TooltipBarBehaviour.Current.SetPanel("Hide");
				TowerMenu.TowerMenuBehaviour.currentMenuItem = -1;
				TowerMenu.TowerMenuBehaviour.placeTower = false;
			}
			if (TowerMenu.TowerMenuBehaviour.sellTower == true && TowerMenu.TowerMenuBehaviour.currentMenuItem == 0) {
				removeTower (0.5f);
				Interface.TooltipBar.TooltipBarBehaviour.Current.SetPanel("Hide");
				TowerMenu.TowerMenuBehaviour.currentMenuItem = -1;
				TowerMenu.TowerMenuBehaviour.sellTower = false;
			}
			if (TowerMenu.TowerMenuBehaviour.upgradeConfirm == true && TowerMenu.TowerMenuBehaviour.currentMenuItem >= 1) {
				createTower ();
				Interface.TooltipBar.TooltipBarBehaviour.Current.SetPanel("Hide");
				TowerMenu.TowerMenuBehaviour.currentMenuItem = -1;
				TowerMenu.TowerMenuBehaviour.upgradeConfirm = false;
			}
			
			maxSupply = Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.SupplyMax;
			currentSupply = Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Supply;
		}

		//Creates the tower you buy
		void createTower ()
		{
			if (newTower) {
				if (checkResources (towersUsed [TowerMenu.TowerMenuBehaviour.currentMenuItem].upgradedTowers [0])) {
					saveTower (towerX, towerY, towersUsed [TowerMenu.TowerMenuBehaviour.currentMenuItem].upgradedTowers [0], TowerMenu.TowerMenuBehaviour.currentMenuItem, placeTower);
					setTile (towerX, towerY, towerGridPosCopy [towerX, towerY].thisTower.GetComponent<SpriteRenderer> ().sprite, Color.white);
				}
			} else {
				newTower = true;
				towerGridPosCopy [towerX, towerY].upgrade += TowerMenu.TowerMenuBehaviour.currentMenuItem;
				if (towerGridPosCopy [towerX, towerY].upgrade < towersUsed [towerGridPosCopy [towerX, towerY].towerType].upgradedTowers.Length) {
					if (checkResources (towersUsed [towerGridPosCopy [towerX, towerY].towerType].upgradedTowers [towerGridPosCopy [towerX, towerY].upgrade])) {
						saveTower (towerX, towerY, towersUsed [towerGridPosCopy [towerX, towerY].towerType].upgradedTowers [towerGridPosCopy [towerX, towerY].upgrade], towerGridPosCopy [towerX, towerY].towerType, placeTower);
						setTile (towerX, towerY, towerGridPosCopy [towerX, towerY].thisTower.GetComponent<SpriteRenderer> ().sprite, Color.white);
					} else
						towerGridPosCopy [towerX, towerY].upgrade -= TowerMenu.TowerMenuBehaviour.currentMenuItem;
				} else
					towerGridPosCopy [towerX, towerY].upgrade -= TowerMenu.TowerMenuBehaviour.currentMenuItem;
			}
		}
			
		//Instantiates all the sprites into their character
		public void Reset ()
		{
			for (int i = 0; i < 5; i++) {
				for (int j = 0; j < 6; j++) {
					if (towerGridPosCopy [i, j].thisTower != null) {
						setTile (i, j, null, Color.clear);
						GameObject.Destroy (towerGridPos [i, j]);
						towerGridPos [i, j] = Instantiate (towerGridPosCopy [i, j].thisTower, towerGridPosCopy [i, j].position, Quaternion.Euler(0,0,180));
					}
				}
			}
		}

		//After the wave has ended displays sprites of the characters on the grid
		public void ResetSprite ()
		{
			for (int i = 0; i < 5; i++) {
				for (int j = 0; j < 6; j++) {
					if (towerGridPosCopy [i, j].thisTower != null) {
						GameObject.Destroy (towerGridPos [i, j]);
						towerGridPos [i, j] = null;
						setTile (i, j, towerGridPosCopy [i, j].thisTower.GetComponent<SpriteRenderer> ().sprite, Color.white);
					}
				}
			}
		}
			
		//sells and removes tower
		void removeTower (float sellValue)
		{	
			setTile (towerX, towerY, null, Color.clear);
			Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Supply -= towerGridPosCopy [towerX, towerY].totalSupply;
			if (Spawners.MinionSpawnerBehaviour.waveCounter == towerGridPosCopy [towerX, towerY].waveCreated) {
				Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Gold += towerGridPosCopy [towerX, towerY].totalCost;
			} else {
				Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Gold += Mathf.RoundToInt (sellValue * towerGridPosCopy [towerX, towerY].cost);
			}
			Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.GoldSpent -= towerGridPosCopy [towerX, towerY].totalCost;
			Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Score -= towerGridPosCopy [towerX, towerY].totalCost / 10;
			towerGridPosCopy [towerX, towerY].totalSupply = 0;
			towerGridPosCopy [towerX, towerY].upgrade = 0;
			towerGridPosCopy [towerX, towerY].totalCost = 0;
			towerGridPosCopy [towerX, towerY].waveCreated = -1;
			GameObject.Destroy (towerGridPos [towerX, towerY]);
			saveTower (towerX, towerY, null, 0, placeTower);
		}

		//sets a tile sprite and a tile color
		public void setTile (int X, int Y, Sprite tileSprite, Color tileColor)
		{
			gridTiles.transform.Find (X + "/" + Y).GetComponent<Image> ().sprite = tileSprite;
			gridTiles.transform.Find (X + "/" + Y).GetComponent<Image> ().color = tileColor;
		}

		//checks if theres enough supply left and money to build a tower
		bool checkResources (GameObject tower)
		{
			if (currentSupply + tower.GetComponent<Game.Units.LegionnaireBehaviour> ().Supply <= maxSupply) {
				if (Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.TryPayingGold (tower.GetComponent<Game.Units.LegionnaireBehaviour> ().Cost)) {
					StartCoroutine (ShowText ("Build complete"));
					return true;
				} else {
					StartCoroutine (ShowText("Not enough gold"));
					return false;
				}
			} else{
				StartCoroutine (ShowText("Not enough supply"));		
				return false;
			}
		}

		//saves the tower so it can be re-created
		void saveTower (int X, int Y, GameObject tower, int originalTower, Vector2 pos)
		{
			towerGridPosCopy [X, Y].towerType = originalTower;
			towerGridPosCopy [X, Y].thisTower = tower;
			towerGridPosCopy [X, Y].position = pos;
			towerGridPosCopy [X, Y].waveCreated = Spawners.MinionSpawnerBehaviour.waveNumber;
			if (tower != null) {
				towerGridPos [X, Y] = Instantiate (tower);
				towerGridPosCopy [X, Y].cost = towerGridPosCopy [X, Y].thisTower.GetComponent<Game.Units.LegionnaireBehaviour> ().Cost;
				towerGridPosCopy [X, Y].totalCost += towerGridPosCopy [X, Y].cost;
				Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Score += towerGridPosCopy [X, Y].cost / 10;
				Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.GoldSpent += towerGridPosCopy [X, Y].cost;
				Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Supply += towerGridPosCopy [X, Y].thisTower.GetComponent<Game.Units.LegionnaireBehaviour> ().Supply;
				towerGridPosCopy [X, Y].totalSupply += towerGridPosCopy [X, Y].thisTower.GetComponent<Game.Units.LegionnaireBehaviour> ().Supply;
			}
		}

		//has to be done for some reason
		void instantiateTower ()
		{
			for (int i = 0; i < 5; i++) {
				for (int j = 0; j < 6; j++) {
					towerGridPosCopy [i, j] = new TowerInfo ();
				}
			}
		}
		//coroutine that shows message when trying to place tower, message fades until invisible and is deactivated
		IEnumerator ShowText(string message){
			if (message == "Build complete") {
				buildMessage.GetComponent<TextMesh> ().color = Color.green;
			} else {
				buildMessage.GetComponent<TextMesh> ().color = Color.red;
			}
			buildMessage.GetComponent<TextMesh> ().text = message;
			buildMessage.SetActive (true);
			buildMessage.transform.position = new Vector2 (0f, 12f);
			for(float f = 1.0f; f >= 0; f -= 0.1f){
				Color textColor = buildMessage.GetComponent<TextMesh> ().color;
				textColor.a = f;
				buildMessage.GetComponent<TextMesh> ().color = textColor;
				yield return new WaitForSeconds(0.1f);
			}
			buildMessage.SetActive (false);
		}
	}
}