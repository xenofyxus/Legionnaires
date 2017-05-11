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

		protected override void Apply(UnitBehaviour unit)
		{
			Buffs.PercentageDotTickBuff heartstopper = unit.GetComponent<Buffs.PercentageDotTickBuff>();
			HeartStopperMetaData metaData;
			if (heartstopper == null)
			{
				heartstopper = unit.gameObject.AddComponent<Buffs.PercentageDotTickBuff>();
				heartstopper.Duration = float.PositiveInfinity;
				heartstopper.TickTime = tickTime;
				heartstopper.HpPercentageDamage = hpPercentageDamage;
				metaData = new HeartStopperMetaData();
				heartstopper.MetaData = metaData;
			}
			else
			{
				metaData = heartstopper.MetaData as HeartStopperMetaData;
			}
			metaData.Count++;
		}

		protected override void Remove(UnitBehaviour unit)
		{
			Buffs.PercentageDotTickBuff heartstopper = unit.GetComponent<Buffs.PercentageDotTickBuff>();
			HeartStopperMetaData metaData = heartstopper.MetaData as HeartStopperMetaData;

			if (heartstopper != null && --metaData.Count == 0)
			{
				heartstopper.Remove();
			}
		}

		private class HeartStopperMetaData
		{
			private int count = 0;

			public int Count {
				get {
					return this.count;
				}
				set {
					count = value;
				}
			}
		}
	}
}

