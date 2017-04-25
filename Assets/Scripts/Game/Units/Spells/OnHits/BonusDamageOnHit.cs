using System;

namespace Game.Units.Spells.OnHits
{
	[Serializable]
	public class BonusDamageOnHit : OnHit
	{
		public float damage;

        protected override float ApplyEffect (float baseDamage, UnitBehaviour target, UnitBehaviour owner, out PostDamageEffect postDamageEffect)
		{
            postDamageEffect = null;
            return damage;
		}
	}
}