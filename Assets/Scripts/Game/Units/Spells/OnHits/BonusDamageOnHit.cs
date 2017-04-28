using System;

namespace Game.Units.Spells.OnHits
{
	[Serializable]
	public class BonusDamageOnHit : OnHit
	{
		public float damage;

        protected override void Apply (float baseDamage, StatModifier modifier, UnitBehaviour target, out PostDamageEffect postDamageEffect)
		{
            postDamageEffect = null;
            modifier.Adders.Add(damage);
		}
	}
}