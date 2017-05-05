using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

namespace Game.Units.Spells.Kingspells
{
	public class StompButton : MonoBehaviour
	{
		[SerializeField]
		public float cooldown;
		private float cooldownTimer;


		GameObject StompCopy;

		Vector2 kingPosition = new Vector2 (0.17f, -14.49f);

		//Initiate the timer to the cooldown value
		void Start ()
		{
			cooldownTimer = cooldown;
		}


		public void onClick(){
			if (cooldownTimer <= 0f) {

				StompCopy = Instantiate (Resources.Load ("StompPrefab"), kingPosition, Quaternion.identity) as GameObject;

				cooldownTimer = cooldown;
			} 
		}


		void FixedUpdate ()
		{
			//Count down the timer
			if (cooldownTimer > 0f) {
				cooldownTimer -= Time.deltaTime;
			}

			//Checks when it's clicked a second time and if so, spawns shockwave copy

		}
	}
}
