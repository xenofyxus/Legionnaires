using System;

namespace Game.Units.Buffs
{
    public class DefendBuff:Buff
    {
        public float damageMultiplier = 1f;

        private Spells.WhenHits.DefendWhenHit defend;

        protected override void Apply()
        {
            defend = owner.gameObject.AddComponent<Spells.WhenHits.DefendWhenHit>();
            defend.damageMultiplier = damageMultiplier;
            defend.effectChance = 100;
        }

        protected override void Remove()
        {
            if(defend != null)
                Destroy(defend);
        }
    }
}

