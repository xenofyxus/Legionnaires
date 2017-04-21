using System;

namespace Game.Units.Spells.OnHits
{
	public class DotOnHit : OnHit
	{
		protected override void ApplyEffect (UnitBehaviour target)
		{
			target.hp -= 15;
		}
	}
}

