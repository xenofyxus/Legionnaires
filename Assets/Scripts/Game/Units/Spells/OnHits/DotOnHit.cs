using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.OnHits
{
	public class DotOnHit : OnHit
	{
		[SerializeField]
		protected float totalDamage;

		public float TotalDamage
		{
			get{ return totalDamage; }
			set{ totalDamage = value; }
		}

		[SerializeField]
		protected int maxStacks;

		public int MaxStacks
		{
			get{ return maxStacks; }
			set{ maxStacks = value; }
		}

		[SerializeField]
		[UnityEngine.Range(1f, 2f)]
		protected float stackMultiplier = 1f;

		public float StackMultiplier
		{
			get{ return stackMultiplier; }
			set{ stackMultiplier = value; }
		}

		[SerializeField]
		[UnityEngine.Range(0f, 20f)]
		protected float duration;

		public float Duration
		{
			get{ return duration; }
			set{ duration = value; }
		}

		protected override void Apply(UnitStat damage, UnitBehaviour target, out PostDamageEffect postDamageEffect)
		{
			Buffs.DotBuff[] activeDots = target.GetComponents<Buffs.DotBuff>();
			Buffs.DotBuff activeDot = null;


			foreach(Buffs.DotBuff dot in activeDots)
			{
				DotMetaData dotMetaData = dot.MetaData as DotMetaData;
				if(dotMetaData.dotName == spellName)
				{
					activeDot = dot;
					break;
				}
			}


			if(activeDot == null)
			{
				Buffs.DotBuff dot = target.gameObject.AddComponent<Buffs.DotBuff>();

				dot.MetaData = new DotMetaData(spellName);
				DotMetaData dotMetaData = dot.MetaData as DotMetaData;

				dotMetaData.stacks = 1;

				dot.Duration = duration;
				dot.TotalDamage = (float)Math.Pow(stackMultiplier, 0) * totalDamage;
			}
			else
			{
				DotMetaData activeDotMetaData = activeDot.MetaData as DotMetaData;
				if(activeDotMetaData.stacks < maxStacks)
				{
					int newStacks = activeDotMetaData.stacks + 1;
					Destroy(activeDot);

					Buffs.DotBuff newDot = target.gameObject.AddComponent<Buffs.DotBuff>();

					newDot.MetaData = new DotMetaData(spellName);
					DotMetaData newDotMetaData = newDot.MetaData as DotMetaData;

					newDotMetaData.stacks = newStacks;

					newDot.Duration = duration;
					newDot.TotalDamage = (float)Math.Pow(stackMultiplier, newStacks - 1) * totalDamage;
				}
				else
				{
					activeDot.Duration = duration;
				}
			}

			postDamageEffect = null;
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

