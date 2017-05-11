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
		private int oldMenuItem;
		private GameObject buyBTN;
		private GameObject gridFather;
		private GameObject gridBuild;
		void Start ()
		{
			gridFather = GameObject.Find ("GridFather");
			gridBuild = GameObject.Find ("GridBuilder");
			gridFather.SetActive (false);
			buyBTN = GameObject.Find ("BUY");
			buyBTN.SetActive (false);
			menuItems = buttons.Count;
			int ex = gridBuild.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerX;
			int ey = gridBuild.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerY;
			if (menuItems > 3) {
				for (int i = 0; i < menuItems; i++) {
					buttons [i].btnImage.sprite = gridBuild.GetComponent<GridBuilder.GridBuilderBehaviour> ().towersAvailable [i].upgradedTowers [0].GetComponent<SpriteRenderer> ().sprite;
				}
			} else {
				for(int i = 1; i < menuItems; i++){
					buttons [i].btnImage.sprite = gridBuild.GetComponent<GridBuilder.GridBuilderBehaviour> ().towersAvailable [gridBuild.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerGridPosCopy[ex, ey].towerType].upgradedTowers [i + gridBuild.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerGridPosCopy[ex, ey].upgrade].GetComponent<SpriteRenderer> ().sprite;

				}
			}
			foreach (MenuButton button in buttons) {
				button.menuImage.color = button.normalColor;
			}
			currentMenuItem = -1;
		}

		void Update ()
		{
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

				int xSpot = gridBuild.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerX;
				int ySpot = gridBuild.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerY;
				int towerType = gridBuild.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerGridPosCopy [xSpot, ySpot].towerType;
				int upgrade = gridBuild.GetComponent<GridBuilder.GridBuilderBehaviour> ().towerGridPosCopy [xSpot, ySpot].upgrade;

				if(this.gameObject.name == "TowerMenju(Clone)"){
					TooltipBar.TowerPanel.TowerPanelBehaviour.Current.SetUnit (gridBuild.GetComponent<GridBuilder.GridBuilderBehaviour> ().towersAvailable [currentMenuItem].upgradedTowers [0].GetComponent<Units.LegionnaireBehaviour>());
				}
				if (this.gameObject.name == "SellMenu(Clone)") {
					TooltipBar.TowerPanel.TowerPanelBehaviour.Current.SetUnit (gridBuild.GetComponent<GridBuilder.GridBuilderBehaviour> ().towersAvailable [towerType].upgradedTowers [upgrade + currentMenuItem].GetComponent<Units.LegionnaireBehaviour>());
				}
				if (this.gameObject.name == "WizardUpgrade(Clone)") {
					TooltipBar.TowerPanel.TowerPanelBehaviour.Current.SetUnit (gridBuild.GetComponent<GridBuilder.GridBuilderBehaviour> ().towersAvailable [towerType].upgradedTowers [upgrade + currentMenuItem].GetComponent<Units.LegionnaireBehaviour>());
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
				gridFather.SetActive (true);
				DestroyMe ();
			}
		}

		public void DestroyMe(){
			GameObject.Destroy (this.gameObject);
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