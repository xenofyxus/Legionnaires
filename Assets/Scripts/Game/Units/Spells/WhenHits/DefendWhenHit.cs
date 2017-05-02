using System;

namespace Game.Units.Spells.WhenHits
{
    public class DefendWhenHit:WhenHit
    {
        [UnityEngine.Range(0f, 1f)]
        public float damageMultiplier = 1f;

        protected override void Apply(UnitStat damage, UnitBehaviour attacker)
        {
            damage.AddMultiplier(damageMultiplier);
        }
    }
}

