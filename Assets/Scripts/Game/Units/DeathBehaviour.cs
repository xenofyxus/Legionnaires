using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Units
{
	public class DeathBehaviour:MonoBehaviour
	{
		GameObject deathAnimation;

		void Awake(){
			deathAnimation = this.gameObject;
		}

		void Update()
		{
			StartCoroutine (KillAnimationOnEnd ());
		}

		private IEnumerator KillAnimationOnEnd()
		{
			yield return new WaitForSeconds(2f);
			Destroy(deathAnimation);
		}
	}
}

