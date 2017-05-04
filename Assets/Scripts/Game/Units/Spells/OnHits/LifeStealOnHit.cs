using System;
using UnityEngine;

namespace Game.Units.Spells.OnHits
{
	public class LifeStealOnHit:OnHit
	{
		[SerializeField]
		protected float healingMultiplier;

        public float HealingMultiplier
        {
            get{ return healingMultiplier; }
            set{ healingMultiplier = value; }
        }

		protected override void Apply(UnitStat damage, UnitBehaviour target, out PostDamageEffect postDamageEffect)
		{
			postDamageEffect = HandlePostDamageEffect;
		}

		void HandlePostDamageEffect(float damage, UnitStat healing, UnitBehaviour target)
		{
            healing.AddAdder((float)damage * healingMultiplier);
		}
	}
}

