using System;

namespace Game.Units.Spells.OnHits
{
    public class CritOnHit : OnHit
    {
        public float multiplier;

        protected override int? ApplyEffect(int baseDamage, UnitBehaviour target)
        {
            return (int)((float)baseDamage * multiplier);
        }
    }
}

