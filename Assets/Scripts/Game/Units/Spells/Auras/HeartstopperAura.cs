using System;
using UnityEngine;

namespace Game.Units.Spells.Auras
{
	public class HeartstopperAura:Aura
	{
		[SerializeField]
		[Range(0f, 1f)]
		private float hpPercentageDamage = 0.01f;

		[SerializeField]
		private float tickTime = 1f;

		public float HpPercentageDamage {
			get {
				return this.hpPercentageDamage;
			}
			set {
				hpPercentageDamage = value;
			}
		}

		public float TickTime {
			get {
				return this.tickTime;
			}
			set {
				tickTime = value;
			}
		}

		protected override void ApplyEffect(UnitBehaviour unit)
		{
			Buffs.PercentageDotTickBuff heartstopper = unit.GetComponent<Buffs.PercentageDotTickBuff>();
			heartstopper = unit.gameObject.AddComponent<Buffs.PercentageDotTickBuff>();
			heartstopper.Duration = float.PositiveInfinity;
			heartstopper.TickTime = tickTime;
			heartstopper.HpPercentageDamage = hpPercentageDamage;
		}

		protected override void RemoveEffect(UnitBehaviour unit)
		{
			Buffs.PercentageDotTickBuff heartstopper = unit.GetComponent<Buffs.PercentageDotTickBuff>();
			heartstopper.Remove();
		}
	}
}

