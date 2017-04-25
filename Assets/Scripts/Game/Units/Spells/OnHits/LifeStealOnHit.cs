using System;

namespace Game.Units.Spells.OnHits
{
    public class LifeStealOnHit:OnHit
    {
        public float healingMultiplier;

        protected override float ApplyEffect(float baseDamage, UnitBehaviour target, UnitBehaviour owner, out PostDamageEffect postDamageEffect)
        {
            postDamageEffect = HandlePostDamageEffect;
            return 0;
        }

        float HandlePostDamageEffect(float damage, UnitBehaviour target, UnitBehaviour owner)
        {
            return damage * healingMultiplier;
        }
    }
}

