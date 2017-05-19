using System;
using UnityEngine;

namespace Game.Units.Spells.Passives
{
	public class ThornsPassive:Passive
	{
		[UnityEngine.SerializeField]
		[Tooltip("0.2 = 20% of the damage in return")]
		private float returnedDamageFactor = 0.2f;
		private float returnedDamage;

		public float ReturnedDamage
		{
			get
			{
				return this.returnedDamage;
			}
			set
			{
				returnedDamage = value;
			}
		}

		protected override void OwnerTookDamage(object sender, TookDamageEventArgs e)
		{
			float dummyVar;
			if (e.Attacker != null) {
				returnedDamage = UnityEngine.Random.Range (e.Attacker.DamageMin, e.Attacker.DamageMax) * returnedDamageFactor;
				e.Attacker.ApplyDamage (returnedDamage, out dummyVar, null);
			}
		}
	}
}

