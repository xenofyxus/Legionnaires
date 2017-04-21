/*
 * Author: Alexander Krantz
 * Author: Anton Anderzén
 */
using UnityEngine;

namespace Game.Units.Spells.OnHits
{
	[System.Serializable]
	public abstract class OnHit : MonoBehaviour
	{
		public int hitChance;

		private const int minHitChance = 1;
		private const int maxHitChance = 100;

		public void Hit(UnitBehaviour target)
		{
			if (hitChance <= 0) {
				throw new System.ArgumentOutOfRangeException ("Hit chance must be more than 0 and equal to or less than 100");
			} else if (hitChance > 100) {
				throw new System.ArgumentOutOfRangeException ("Hit chance must be more than 0 and equal to or less than 100");
			} else {
				if (Random.Range(minHitChance, maxHitChance + 1) <= hitChance) {
					ApplyEffect (target);
				}
			}

		}
		/// <summary>
		/// Base method for applying on hit effects defined by derived classes.
		/// </summary>
		/// <param name="target">Target unit to apply the effect on.</param>
		protected abstract void ApplyEffect (UnitBehaviour target);
	}
}

