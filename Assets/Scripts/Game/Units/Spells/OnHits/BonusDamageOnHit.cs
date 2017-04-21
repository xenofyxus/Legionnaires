using System;

namespace Game.Units.Spells.OnHits
{
	[Serializable]
	public class BonusDamageOnHit : OnHit
	{
		public float damage;

		protected override float? ApplyEffect (float baseDamage, Game.Units.UnitBehaviour target)
		{
            return baseDamage + damage;
		}
	}
}