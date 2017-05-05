using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

namespace Game.Units.Spells.Kingspells
{
	public class ShockwaveButton : MonoBehaviour
	{
		[SerializeField]
		public float cooldown;
		private float cooldownTimer;


		GameObject ShockwaveCopy;
		Vector2 mousePosition;
		Vector2 mousePositionNew;
		Vector2 kingPosition = new Vector2 (0.17f, -14.49f);
		bool isClicked = false;

		//Initiate the timer to the cooldown value
		void Start ()
		{
			cooldownTimer = cooldown;
		}


		//Assign mouseposition if cooldowntimer allows us
		public void onClick ()
		{
			if (cooldownTimer <= 0f) {
				mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				isClicked = true;
			}
		}


		void FixedUpdate ()
		{
			//Count down the timer
			if (cooldownTimer > 0f) {
				cooldownTimer -= Time.deltaTime;
			}

			mousePositionNew = Camera.main.ScreenToWorldPoint (Input.mousePosition);


			//Checks when it's clicked a second time and if so, spawns shockwave copy
			if (isClicked && Input.GetMouseButtonDown (0) && (mousePosition != mousePositionNew) && cooldownTimer <= 0f) {
				mousePosition = mousePositionNew;

				ShockwaveCopy = Instantiate (Resources.Load ("ShockwavePrefab"), kingPosition, Quaternion.identity) as GameObject;
				ShockwaveCopy.transform.rotation = Quaternion.FromToRotation (Vector2.up, mousePositionNew - (Vector2)ShockwaveCopy.transform.position);

				isClicked = false;
				cooldownTimer = cooldown;
			} 
		}
	}
}
