using System;

namespace Game.Units.Spells.OnHits
{
    public class SplashOnHit:OnHit
    {
        [UnityEngine.Range(0f,2f)]
        public float damageMultiplier;

        public float range;

        protected override void Apply(float baseDamage, StatModifier damageModifier, UnitBehaviour target, out PostDamageEffect postDamageEffect)
        {
            postDamageEffect = HandlePostDamageEffect;
        }

        void HandlePostDamageEffect (float damage, StatModifier healModifier, UnitBehaviour target)
        {
            foreach(var enemy in target.GetFriendlies())
            {
                if(UnityEngine.Vector2.Distance(target.transform.position, enemy.transform.position) <= range)
                {
                    enemy.ApplyDamage(damage * damageMultiplier);
                }
            }
        }
    }
}

