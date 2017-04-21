using System;

namespace Game.Units.Spells.OnHits
{
	[Serializable]
	public class BonusDamageOnHit : OnHit
	{
		public int damage;

		protected override int? ApplyEffect (int baseDamage, Game.Units.UnitBehaviour target)
		{
            return baseDamage + damage;
		}
	}
}