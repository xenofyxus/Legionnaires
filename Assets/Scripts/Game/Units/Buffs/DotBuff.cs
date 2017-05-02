using System;
using UnityEngine;

namespace Game.Units.Buffs
{
	public class DotBuff:Buff
	{

		[SerializeField]
		protected float totalDamage;

		public float TotalDamage
		{
			get{ return totalDamage; }
			set{ totalDamage = value; }
		}

		private float hpRegModifier;

		protected override void Apply()
		{
			hpRegModifier = -(totalDamage / duration);
			owner.HpReg.AddMultiplier(hpRegModifier);
		}

		protected override void Remove()
		{
			owner.HpReg.RemoveMultiplier(hpRegModifier);
		}
			
	}
}

