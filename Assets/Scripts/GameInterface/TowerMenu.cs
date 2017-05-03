using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameInterface
{	[System.Serializable]
	public class TowerMenu : MonoBehaviour
	{
		public List<MenuButton> buttons = new List<MenuButton> ();

		private Vector2 mousePos;
		private Vector2 circleCenter;

		public int menuItems;
		public static int currentMenuItem = -1;
		public static bool placeTower;
		private int oldMenuItem;
		private GameObject buyBTN;

		void Start ()
		{
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
				placeTower = true;
			}

			if (currentMenuItem != -1 && placeTower == true || vectorToMouse.magnitude > 3.5f) {
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