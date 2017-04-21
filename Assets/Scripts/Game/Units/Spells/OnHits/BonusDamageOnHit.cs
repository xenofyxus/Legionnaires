using System;

namespace Game.Units.Spells.OnHits
{
	[Serializable]
	public class BonusDamageOnHit : OnHit
	{
		public int damage;

		protected override void ApplyEffect (Game.Units.UnitBehaviour target)
		{
			target.hp -= damage;
		}
	}
}