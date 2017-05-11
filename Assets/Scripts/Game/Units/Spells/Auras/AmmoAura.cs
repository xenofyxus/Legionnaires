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

		protected override void Apply(UnitBehaviour unit)
		{
			if (unit.GetComponent<AmmoAuraMarkPassive>() == null && (unit.name.Contains("Marksman") || unit.name.Contains("Ranger")))
			{
				unit.gameObject.AddComponent<AmmoAuraMarkPassive>();
				unit.AttackSpeed.AddMultiplier(attackSpeedMultiplier);
			}
		}

		protected override void Remove(UnitBehaviour unit)
		{
			AmmoAuraMarkPassive mark = unit.GetComponent<AmmoAuraMarkPassive>();
			if (mark != null && (unit.name.Contains("Marksman") || unit.name.Contains("Ranger")))
			{
				Destroy(mark);
				unit.AttackSpeed.RemoveMultiplier(attackSpeedMultiplier);
			}
		}

		private class AmmoAuraMarkPassive : Passives.Passive
		{
		}
	}
}

