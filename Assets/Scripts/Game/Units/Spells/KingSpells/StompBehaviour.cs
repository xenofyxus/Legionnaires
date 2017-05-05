using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System;

namespace Game.Units.Spells.Kingspells
{

	public class StompBehaviour : MonoBehaviour
	{

		GameObject StompCopy;
	
		[SerializeField]
		public float damage;
		[UnityEngine.Range(0f, 5f)]
		public float duration;

	
		//When a copy spawns get the mouseposition of the last click to get direction and assign ShockwaveCopy to that object.
		void Awake(){
			StompCopy = this.gameObject;
		}



		//Destroy the shockwave travelled outside of range from the king else move towards the mouseclick
		void FixedUpdate ()
		{
			if (StompCopy != null) {
				StartCoroutine (KillAnimationOnEnd ());
			}
					//Destroy (StompCopy);
					
			}
			

		private IEnumerator KillAnimationOnEnd(){
			yield return new WaitForSeconds(1.0f);
			Destroy(StompCopy);



		}


		//Apply damage to minions when they trigger the rigidbody
		void OnTriggerEnter2D (Collider2D other)
		{
			if (other.gameObject.GetComponent<Game.Units.MinionBehaviour> () != null) {
				//other.GetComponent<UnitBehaviour> ().ApplyDamage (damage);
				Buffs.StunBuff stunBuff = other.GetComponent<UnitBehaviour> ().gameObject.AddComponent<Buffs.StunBuff>();
				stunBuff.Duration = duration;

	
			}
		}

	}
}
