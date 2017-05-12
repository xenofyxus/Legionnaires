using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Interface.TowerMenu
{

	[System.Serializable]
	public class TowerMenuBehaviour : MonoBehaviour
	{
		public List<MenuButton> buttons = new List<MenuButton> ();

		private Vector2 mousePos;
		private Vector2 circleCenter;
		public int menuItems;
		public static int currentMenuItem = -1;
		public static bool placeTower;
		public static bool sellTower;
		public static bool sellConfirm;
		public static bool upgradeConfirm;
		public static bool nextWaveStarted = false;
		private int oldMenuItem;
		private GameObject buyBTN;
		private GameObject gridTiles;
		private GameObject gridBuilder; 
		int ex;
		int ey;
		Color colorTile = new Color (1f, 0.92f, 0.016f, 0.5f);
		void Start ()
		{
			gridTiles = GameObject.Find ("GridTiles");
			gridBuilder = GameObject.Find ("GridBuilder");
			buyBTN = GameObject.Find ("BUY");
			buyBTN.SetActive (false);
			menuItems = buttons.Count;
			ex = gridBuilder.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerX;
			ey = gridBuilder.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerY;
			EnableDisableTiles (colorTile);
			if (menuItems > 3) {
				for (int i = 0; i < menuItems; i++) {
					buttons [i].btnImage.sprite = gridBuilder.GetComponent<GridBuilder.GridBuilderBehaviour> ().towersAvailable [i].upgradedTowers [0].GetComponent<SpriteRenderer> ().sprite;
				}
			} else {
				for(int i = 1; i < menuItems; i++){
					buttons [i].btnImage.sprite = gridBuilder.GetComponent<GridBuilder.GridBuilderBehaviour> ().towersAvailable [gridBuilder.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerGridPosCopy[ex, ey].towerType].upgradedTowers [i + gridBuilder.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerGridPosCopy[ex, ey].upgrade].GetComponent<SpriteRenderer> ().sprite;

				}
			}
			foreach (MenuButton button in buttons) {
				button.menuImage.color = button.normalColor;
			}
			currentMenuItem = -1;
		}
			
		void EnableDisableTiles(Color markedTile){
			for (int i = 0; i < 5; i++) {
				for (int j = 0; j < 7; j++) {
					if (i == ex && j == ey) {
						gridBuilder.GetComponent<GridBuilder.GridBuilderBehaviour> ().setTile (ex, ey, null, markedTile);
					} else {
						gridTiles.transform.Find (i + "/" + j).GetComponent<Image> ().enabled = !gridTiles.transform.Find (i + "/" + j).GetComponent<Image> ().enabled;
					}
				}
			}
		}

		void Update ()
		{
			if (nextWaveStarted) {
				EnableDisableTiles (Color.clear);
				GameObject.Destroy (this.gameObject);
			}
			if(Input.GetMouseButtonDown (0)){
				GetCurrentMenuItem();
			}
		}

		public void GetCurrentMenuItem ()
		{
			sellTower = false;
			sellConfirm = false;
			upgradeConfirm = false;
			placeTower = false;

			Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition);
			circleCenter = (Vector2)transform.position;
			Vector2 vectorToMouse = mousePos - circleCenter;

			float angle = (Mathf.Atan2 (vectorToMouse.x, vectorToMouse.y) * Mathf.Rad2Deg);

			if (angle < 0) {
				angle = 360 + angle;
			}

			if (vectorToMouse.magnitude < 3.5f && vectorToMouse.magnitude > 1.5f) {
				currentMenuItem = (int)(angle / (360 / menuItems));

				buttons [oldMenuItem].menuImage.color = buttons [oldMenuItem].normalColor;
				oldMenuItem = currentMenuItem;
				buttons [currentMenuItem].menuImage.color = buttons[currentMenuItem].pressedColor;

				int xSpot = gridBuilder.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerX;
				int ySpot = gridBuilder.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerY;
				int towerType = gridBuilder.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerGridPosCopy [xSpot, ySpot].towerType;
				int upgrade = gridBuilder.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerGridPosCopy [xSpot, ySpot].upgrade;

				if(this.gameObject.name == "TowerMenju(Clone)"){
					TooltipBar.TowerPanel.TowerPanelBehaviour.Current.SetUnit (gridBuilder.GetComponent<GridBuilder.GridBuilderBehaviour> ().towersAvailable [currentMenuItem].upgradedTowers [0].GetComponent<Units.LegionnaireBehaviour>());
				}
				if (this.gameObject.name == "SellMenu(Clone)") {
					TooltipBar.TowerPanel.TowerPanelBehaviour.Current.SetUnit (gridBuilder.GetComponent<GridBuilder.GridBuilderBehaviour> ().towersAvailable [towerType].upgradedTowers [upgrade + currentMenuItem].GetComponent<Units.LegionnaireBehaviour>());
				}
				if (this.gameObject.name == "WizardUpgrade(Clone)") {
					TooltipBar.TowerPanel.TowerPanelBehaviour.Current.SetUnit (gridBuilder.GetComponent<GridBuilder.GridBuilderBehaviour> ().towersAvailable [towerType].upgradedTowers [upgrade + currentMenuItem].GetComponent<Units.LegionnaireBehaviour>());
				}
				buyBTN.SetActive (true);
			}

			if (vectorToMouse.magnitude < 1.5f && currentMenuItem != -1) {

				if (this.gameObject.name != "TowerMenju(Clone)") {
					if (currentMenuItem == 0) {
						sellTower = true;
					} else {
						upgradeConfirm = true;

					}
				} else {
					placeTower = true;
				}
			}

			if (currentMenuItem != -1 && (placeTower == true || sellTower == true || upgradeConfirm == true) || vectorToMouse.magnitude > 3.5f) {
				EnableDisableTiles (Color.clear);
				GameObject.Destroy (this.gameObject);
			}
		}
	}

	[System.Serializable]

	public class MenuButton
	{
		public string name;
		public Image menuImage;
		public Image btnImage;
		public Color normalColor = Color.white;
		public Color pressedColor = Color.gray;
	}

}