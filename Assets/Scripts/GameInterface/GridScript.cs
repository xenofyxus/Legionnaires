using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameInterface
{
	public class GridScript : MonoBehaviour
	{

		float offSetX = 1.48f;
		float offSetY = 1.47f;
		public GameObject[] towers = new GameObject[6];
		public GameObject menu;
		int whichSpotX;
		int whichSpotY;
		Vector2 placeTower;
		GameObject[,] minionPos = new GameObject[5, 7];
		// Use this for initialization
		void Start ()
		{

		}
	
		// Update is called once per frame
		void Update ()
		{
			if (Input.GetMouseButtonDown (0) && GameObject.FindGameObjectsWithTag("TowerMenu").Length == 0) {
				Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				whichSpotX = Mathf.FloorToInt ((mouseWorldPos.x + 3.8f) / offSetX);
				whichSpotY = Mathf.FloorToInt (mouseWorldPos.y / offSetY) - 1;
				placeTower = new Vector2 (-3.0f + (whichSpotX * 1.48f), 2.18f + (whichSpotY * 1.47f));
				if (0 <= whichSpotX && whichSpotX <= 4 && 0 <= whichSpotY && whichSpotY <= 6 && minionPos [whichSpotX, whichSpotY] == null) {
					Instantiate (menu, placeTower, transform.rotation);
				}
			}
			if (TowerMenu.currentMenuItem != -1) {
				spawnTower ();
				TowerMenu.currentMenuItem = -1;
			}
		}

		void spawnTower () {
			if (0 <= whichSpotX && whichSpotX <= 4 && 0 <= whichSpotY && whichSpotY <= 6 && minionPos [whichSpotX, whichSpotY] == null) {
				Instantiate (towers [TowerMenu.currentMenuItem], placeTower, transform.rotation);
				minionPos [whichSpotX, whichSpotY] = towers [TowerMenu.currentMenuItem];
			}
		}
	}
}