using System;

namespace Game.Units.Spells.OnHits
{
    public class CritOnHit : OnHit
    {
        public float multiplier;

        protected override float Apply(float baseDamage, UnitBehaviour target, out PostDamageEffect postDamageEffect)
        {
            postDamageEffect = null;
            return baseDamage * multiplier - baseDamage;
        }
    }
}

