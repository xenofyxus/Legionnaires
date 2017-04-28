using System;

namespace Game.Units.Spells.OnHits
{
    public class LifeStealOnHit:OnHit
    {
        public float healingMultiplier;

        protected override void Apply(float baseDamage, StatModifier modifier, UnitBehaviour target, out PostDamageEffect postDamageEffect)
        {
            postDamageEffect = HandlePostDamageEffect;
        }

        void HandlePostDamageEffect(float damage, StatModifier healModifier, UnitBehaviour target)
        {
            healModifier.Adders.Add(damage * healingMultiplier);
        }
    }
}

