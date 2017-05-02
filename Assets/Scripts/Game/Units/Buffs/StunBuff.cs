using System;
using UnityEngine;

namespace Game.Units.Buffs
{
	public class StunBuff : Buff
	{
		protected override void Apply()
		{	
			owner.AttackSpeed.AddMultiplier(0f);
			owner.MovementSpeed.AddMultiplier(0f);
		}

		protected override void Remove()
		{
			owner.AttackSpeed.RemoveMultiplier(0f);
			owner.MovementSpeed.RemoveMultiplier(0f);
		}
	}
}

