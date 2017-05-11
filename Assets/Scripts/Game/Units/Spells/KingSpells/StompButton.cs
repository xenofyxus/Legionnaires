using UnityEngine;
using UnityEngine.UI;
using System;

namespace Game.Units.Spells.Kingspells
{
	public class StompButton : MonoBehaviour
	{
		[SerializeField]
		private float cooldown;
		private float cooldownTimer;
		Image stompBtn;
		GameObject StompCopy;
		Vector2 kingPosition = new Vector2 (0.17f, -14.49f);


		public float Cooldown {
			get {
				return this.cooldown;
			}
			set {
				cooldown = value;
			}
		}

		public float CooldownTimer {
			get {
				return this.cooldownTimer;
			}
			set {
				cooldownTimer = value;
			}
		}
			
		//Initiate the timer to the cooldown value
		void Start ()
		{
			stompBtn = this.gameObject.GetComponent<Image> ();
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
				stompBtn.fillAmount = 1 - (cooldownTimer / cooldown);
			}
		}
	}
}
