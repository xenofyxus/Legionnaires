using System;

namespace Game.Units.Spells.OnHits
{
    public class SplashOnHit:OnHit
    {
        [UnityEngine.Range(0f,2f)]
        public float damageMultiplier;

        public float range;

        protected override float Apply(float baseDamage, UnitBehaviour target, out PostDamageEffect postDamageEffect)
        {
            postDamageEffect = HandlePostDamageEffect;
            return 0f;
        }

        float HandlePostDamageEffect (float damage, UnitBehaviour target, UnitBehaviour owner)
        {
            foreach(var enemy in target.GetFriendlies())
            {
                if(UnityEngine.Vector2.Distance(target.transform.position, enemy.transform.position) <= range)
                {
                    enemy.ApplyDamage(damage * damageMultiplier);
                }
            }
            return 0f;
        }
    }
}

