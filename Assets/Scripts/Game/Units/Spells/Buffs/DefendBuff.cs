using System;

namespace Game.Units.Spells.Buffs
{
    public class DefendBuff:Buff
    {
        [UnityEngine.SerializeField]
        private float damageMultiplier = 1f;

        public float DamageMultiplier
        {
            get{ return damageMultiplier; }
            set{ damageMultiplier = value; }
        }

        private Spells.WhenHits.DefendWhenHit defend;

        protected override void Apply()
        {
            defend = owner.gameObject.AddComponent<Spells.WhenHits.DefendWhenHit>();
            defend.DamageMultiplier = damageMultiplier;
            defend.effectChance = 100;
        }

        protected override void Remove()
        {
            if(defend != null)
                Destroy(defend);
        }
			
    }
}

