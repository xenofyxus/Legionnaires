using System;

namespace Game.Units.Spells.OnHits
{
	[Serializable]
	public class BonusDamageOnHit : OnHit
	{
		public float damage;

        protected override float Apply (float baseDamage, UnitBehaviour target, out PostDamageEffect postDamageEffect)
		{
            postDamageEffect = null;
            return damage;
		}
	}
}