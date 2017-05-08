using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.Abilities
{
	public class StunAbility : Ability
	{
		[SerializeField]
		[Range(0f, 20f)]
		private float duration = 5f;

		public float Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				duration = value;
			}
		}

		protected override void Apply(UnitBehaviour unit)
		{
			Buffs.StunBuff activeBuff = unit.GetComponent<Buffs.StunBuff>();

			if(activeBuff != null && activeBuff.Duration < duration)
			{
				activeBuff.Duration = duration;
			}
			else if(activeBuff == null)
			{
				Destroy(activeBuff);
				Buffs.StunBuff newStunBuff = unit.gameObject.AddComponent<Buffs.StunBuff>();
				newStunBuff.Duration = duration;
			}
		}
	}
}