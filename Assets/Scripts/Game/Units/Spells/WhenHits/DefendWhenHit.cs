using System;

namespace Game.Units.Spells.WhenHits
{
    public class DefendWhenHit:WhenHit
    {
        [UnityEngine.Range(0f, 1f)]
        public float damageMultiplier = 1f;

        protected override void Apply(float damage, StatModifier damageModifier, UnitBehaviour attacker)
        {
            damageModifier.Multipliers.Add(damageMultiplier);
        }
    }
}

