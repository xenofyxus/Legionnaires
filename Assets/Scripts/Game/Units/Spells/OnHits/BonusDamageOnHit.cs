using System;
using UnityEngine;

namespace Game.Units.Spells.OnHits
{
	[Serializable]
	public class BonusDamageOnHit : OnHit
	{
		[SerializeField]
		private float bonusDamage = 10f;

        public float BonusDamage
        {
            get{ return bonusDamage; }
            set{ bonusDamage = value; }
        }

		protected override void Apply(UnitStat damage, UnitBehaviour target, out PostDamageEffect postDamageEffect)
		{
			postDamageEffect = null;
            damage.AddAdder(bonusDamage);
		}
	}
}