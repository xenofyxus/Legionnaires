/*
 * Author: Alexander Krantz
 * Author: Anton Anderzén
 */
using UnityEngine;

namespace Game.Units.Spells.OnHits
{
    [System.Serializable]
    public abstract class OnHit : MonoBehaviour
    {
        [Range(1, 100)]
        public int hitChance = 100;

        private const int minHitChance = 1;
        private const int maxHitChance = 100;

        /// <summary>
        /// Do not set this owner if you are not adding this OnHit to anything other than a UnitBehaviour
        /// </summary>
        [System.NonSerialized]
        public UnitBehaviour owner;

        public float Hit(float baseDamage, UnitBehaviour target, out PostDamageEffect postDamageEffect)
        {
            postDamageEffect = null;
            if(Random.Range(minHitChance, maxHitChance + 1) <= hitChance)
            {
                return Apply(baseDamage, target, out postDamageEffect);
            }
            return 0;
        }

        /// <summary>
        /// Base method for applying on hit effects defined by derived classes.
        /// </summary>
        /// <param name="target">Target unit to apply the effect on.</param>
        /// <returns>>The added damage when applying it do the enemy</returns>
        protected abstract float Apply(float baseDamage, UnitBehaviour target, out PostDamageEffect postDamageEffect);

        void Start()
        {
            if(owner == null)
                owner = GetComponent<UnitBehaviour>();
        }
    }
}

