using System;
using UnityEngine;

namespace Game.Units.Spells.Buffs
{
	public class PercentageDotTickBuff:TickBuff
	{
		[SerializeField]
		[Range(0f, 1f)]
		private float hpPercentageDamage = 0.01f;

		public float HpPercentageDamage {
			get {
				return this.hpPercentageDamage;
			}
			set {
				hpPercentageDamage = value;
			}
		}

		protected override void ApplyTick(float deltaTime)
		{
			float dummyVar;
			owner.ApplyDamage(owner.Hp * hpPercentageDamage, out dummyVar, null);
		}
	}
}

