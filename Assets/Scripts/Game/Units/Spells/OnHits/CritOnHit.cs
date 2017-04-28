using System;

namespace Game.Units.Spells.OnHits
{
    public class CritOnHit : OnHit
    {
        public float multiplier;

        protected override void Apply(float baseDamage, StatModifier modifier, UnitBehaviour target, out PostDamageEffect postDamageEffect)
        {
            postDamageEffect = null;
            modifier.Multipliers.Add(multiplier);
        }
    }
}

