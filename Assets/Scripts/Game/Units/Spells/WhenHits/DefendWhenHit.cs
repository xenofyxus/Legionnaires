using System;

namespace Game.Units.Spells.WhenHits
{
    public class DefendWhenHit:WhenHit
    {
        [UnityEngine.SerializeField]
        [UnityEngine.Range(0f, 1f)]
        private float damageMultiplier = 1f;

        public float DamageMultiplier
        {
            get{ return damageMultiplier; }
            set{ damageMultiplier = value; }
        }

        protected override void Apply(UnitStat damage, UnitBehaviour attacker)
        {
            damage.AddMultiplier(damageMultiplier);
        }
    }
}

