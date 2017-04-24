using System;

namespace Game.Units.Spells.OnHits
{
    public class CritOnHit : OnHit
    {
        public float multiplier;

        protected override float ApplyEffect(float baseDamage, UnitBehaviour target, UnitBehaviour owner, out PostDamageEffect postDamageEffect)
        {
            postDamageEffect = null;
            return baseDamage * multiplier - baseDamage;
        }
    }
}

