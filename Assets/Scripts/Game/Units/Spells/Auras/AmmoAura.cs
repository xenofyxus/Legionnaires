using System;
using UnityEngine;

namespace Game.Units.Spells.Auras
{
	public class AmmoAura:Aura
	{
		[SerializeField]
		[Range(1f, 5f)]
		private float attackSpeedMultiplier = 1f;

		public float AttackSpeedMultiplier {
			get {
				return this.attackSpeedMultiplier;
			}
			set {
				attackSpeedMultiplier = value;
			}
		}

		protected override void ApplyEffect(UnitBehaviour unit)
		{
			if (unit.name.Contains("Marksman") || unit.name.Contains("Ranger"))
			{
				unit.AttackSpeed.AddMultiplier(attackSpeedMultiplier);
			}
		}

		protected override void RemoveEffect(UnitBehaviour unit)
		{
			if (unit.name.Contains("Marksman") || unit.name.Contains("Ranger"))
			{
				unit.AttackSpeed.RemoveMultiplier(attackSpeedMultiplier);
			}
		}
	}
}

