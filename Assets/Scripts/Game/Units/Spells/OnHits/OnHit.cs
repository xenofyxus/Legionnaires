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
        [Header("Spell info")]

        public string spellName;

        [Multiline()]
        public string description;

        public Sprite icon;

        [Header("Spell data")]

        [Range(1, 100)]
        public int hitChance = 100;

        /// <summary>
        /// Do not set this owner if you are not adding this OnHit to anything other than a UnitBehaviour
        /// </summary>
        [System.NonSerialized]
        public UnitBehaviour owner;

        public void Hit(float baseDamage, StatModifier damageModifier, UnitBehaviour target, out PostDamageEffect postDamageEffect)
        {
            postDamageEffect = null;
            if(Random.Range(1, 101) <= hitChance)
            {
                Apply(baseDamage, damageModifier, target, out postDamageEffect);
            }
        }

        /// <summary>
        /// Base method for applying on hit effects defined by derived classes.
        /// </summary>
        /// <param name="target">Target unit to apply the effect on.</param>
        /// <returns>>The added damage when applying it do the enemy</returns>
        protected abstract void Apply(float baseDamage, StatModifier damageModifier, UnitBehaviour target, out PostDamageEffect postDamageEffect);

        void Start()
        {
            if(owner == null)
                owner = GetComponent<UnitBehaviour>();
        }
    }
}

