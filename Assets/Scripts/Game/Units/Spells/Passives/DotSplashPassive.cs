using System;
using UnityEngine;

namespace Game.Units.Spells.Passives
{
	public class DotSplashPassive:SplashPassive
	{
		[Header("Dot attributes")]

		[SerializeField]
		protected float totalDamage;

		public float TotalDamage {
			get{ return totalDamage; }
			set{ totalDamage = value; }
		}

		[SerializeField]
		protected int maxStacks;

		public int MaxStacks {
			get{ return maxStacks; }
			set{ maxStacks = value; }
		}

		[SerializeField]
		[UnityEngine.Range(1f, 2f)]
		protected float stackMultiplier = 1f;

		public float StackMultiplier {
			get{ return stackMultiplier; }
			set{ stackMultiplier = value; }
		}

		[SerializeField]
		[UnityEngine.Range(0f, 20f)]
		protected float duration;

		public float Duration {
			get{ return duration; }
			set{ duration = value; }
		}

		protected override void ApplyAdditionalEffect(UnitBehaviour unit)
		{
			base.ApplyAdditionalEffect(unit);
			Buffs.DotTickBuff[] activeDots = unit.GetComponents<Buffs.DotTickBuff>();
			Buffs.DotTickBuff activeDot = null;

			foreach (Buffs.DotTickBuff dot in activeDots)
			{
				DotMetaData dotMetaData = dot.MetaData as DotMetaData;
				if (dotMetaData.dotName == spellName)
				{
					activeDot = dot;
					break;
				}
			}


			if (activeDot == null)
			{
				Buffs.DotTickBuff dot = unit.gameObject.AddComponent<Buffs.DotTickBuff>();

				dot.MetaData = new DotMetaData(spellName);
				DotMetaData dotMetaData = dot.MetaData as DotMetaData;

				dotMetaData.stacks = 1;

				dot.TickTime = 1f;
				dot.Duration = duration;
				dot.TotalDamage = totalDamage;
			}
			else
			{
				DotMetaData activeDotMetaData = activeDot.MetaData as DotMetaData;
				if (activeDotMetaData.stacks < maxStacks)
				{
					activeDotMetaData.stacks += 1;

					activeDot.Duration = duration;
					activeDot.TotalDamage = (float)Math.Pow(stackMultiplier, activeDotMetaData.stacks - 1) * totalDamage;
				}
				else
				{
					activeDot.Duration = duration;
				}
			}
		}

		private class DotMetaData
		{
			public string dotName;

			public int stacks;

			public DotMetaData(string dotName)
			{
				this.dotName = dotName;
				this.stacks = 1;
			}
		}
	}
}

