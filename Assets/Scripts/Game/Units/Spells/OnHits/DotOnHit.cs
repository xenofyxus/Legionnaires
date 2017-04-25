using System;

namespace Game.Units.Spells.OnHits
{
	public class DotOnHit : OnHit
	{
        protected override float ApplyEffect (float baseDamage, UnitBehaviour target, UnitBehaviour owner, out PostDamageEffect postDamageEffect)
		{
            postDamageEffect = null;
            throw new NotImplementedException();
		}
	}
}

