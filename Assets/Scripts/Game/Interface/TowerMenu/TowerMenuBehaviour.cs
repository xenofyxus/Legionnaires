﻿using System.Collections;
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
		private GameObject gridBuilder;
		void Start ()
		{
			gridBuilder = GameObject.Find ("GridFather");
			gridBuilder.SetActive (false);
			buyBTN = GameObject.Find ("BUY");
			buyBTN.SetActive (false);
			menuItems = buttons.Count;
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
				buyBTN.SetActive (true);
			}

			if (vectorToMouse.magnitude < 1.5f && currentMenuItem != -1) {

				if (this.gameObject.name == "SellMenu(Clone)") {
					if (currentMenuItem == 0) {
						sellTower = true;
					} else {
						upgradeConfirm = true;
					}
				}	

				if (this.gameObject.name == "WizardUpgrade(Clone)") {
					if (currentMenuItem == 0) {
						sellTower = true;
					} else {
						upgradeConfirm = true;
					}
				}	

				if (this.gameObject.name == "SellOnly(Clone)") {
					sellTower = true;
				}	

				if (this.gameObject.name == "TowerMenju(Clone)") {
					placeTower = true;
				}	
			}

			if (currentMenuItem != -1 && (placeTower == true || sellTower == true || upgradeConfirm == true) || vectorToMouse.magnitude > 3.5f) {
				gridBuilder.SetActive (true);
				GameObject.Destroy (this.gameObject);
			}
		}
	}

	[System.Serializable]

	public class MenuButton
	{
		public string name;
		public Image menuImage;
		public Color normalColor = Color.white;
		public Color pressedColor = Color.gray;
	}

}