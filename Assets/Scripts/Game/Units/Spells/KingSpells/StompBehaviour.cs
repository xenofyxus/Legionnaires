using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Game.Units.Spells.Kingspells
{
	public class StompBehaviour : MonoBehaviour
	{
		GameObject StompCopy;

		void Awake()
		{
			StompCopy = this.gameObject;
		}

		//Kill the prefabcopy after the animation ended
		void FixedUpdate()
		{
			if (StompCopy != null)
			{
				StartCoroutine(KillAnimationOnEnd());
			}
		}


		private IEnumerator KillAnimationOnEnd()
		{
			yield return new WaitForSeconds(1.0f);
			Destroy(StompCopy);
		}


		//Apply damage to minions when they trigger the rigidbody
		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.GetComponent<Game.Units.MinionBehaviour>() != null)
			{
				float dummyVar;
				if (!other.GetComponent<UnitBehaviour>().ApplyDamage(KingBehaviour.Current.StompDamage, out dummyVar, null))
				{
					Buffs.StunBuff stunBuff = other.GetComponent<UnitBehaviour>().gameObject.AddComponent<Buffs.StunBuff>();
					stunBuff.Duration = KingBehaviour.Current.StompDuration;
				}
			}
		}
	}
}
