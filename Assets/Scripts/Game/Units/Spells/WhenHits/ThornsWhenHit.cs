using System;

namespace Game.Units.Spells.WhenHits
{
    public class ThornsWhenHit:WhenHit
    {
        public float returnedDamage;

        protected override void Apply(float damage, StatModifier damageModifier, UnitBehaviour attacker)
        {
            if(attacker != null)
                attacker.ApplyDamage(returnedDamage);
        }
    }
}

