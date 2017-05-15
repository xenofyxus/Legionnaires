using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Units
{
	public class EffectBehaviour:MonoBehaviour
	{
		[SerializeField]
		private float duration = 1f;

		public float Duration {
			get {
				return this.duration;
			}
			set {
				duration = value;
			}
		}

		void Update()
		{
			StartCoroutine(KillAnimationOnEnd());
		}

		private IEnumerator KillAnimationOnEnd()
		{
			yield return new WaitForSeconds(duration);
			Destroy(gameObject);
		}
	}
}

