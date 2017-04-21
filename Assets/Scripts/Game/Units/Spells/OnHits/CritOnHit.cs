using System;

namespace Game.Units.Spells.OnHits
{
    public class CritOnHit : OnHit
    {
        public float multiplier;

        protected override float? ApplyEffect(float baseDamage, UnitBehaviour target)
        {
            return (int?)(baseDamage * multiplier);
        }
    }
}

