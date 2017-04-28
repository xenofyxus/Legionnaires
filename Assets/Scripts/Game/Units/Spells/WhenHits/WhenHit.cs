using System;
using UnityEngine;

namespace Game.Units.Spells.WhenHits
{
    public abstract class WhenHit : MonoBehaviour
    {
        [Header("Spell Info")]

        public string spellName;

        [Multiline]
        public string description;

        public Sprite icon;

        [Header("Spell Data")]

        /// <summary>
        /// The effect chance between 1 and 100 in percentages.
        /// </summary>
        [Range(1, 100)]
        public int effectChance = 100;

        [NonSerialized]
        public UnitBehaviour owner;

        void Start()
        {
            owner = GetComponent<UnitBehaviour>();
        }

        public void Hit(float damage, StatModifier damageModifier, UnitBehaviour attacker)
        {
            if(UnityEngine.Random.Range(1, 101) <= effectChance)
            {
                Apply(damage, damageModifier, attacker);
            }
        }

        protected abstract void Apply(float damage, StatModifier damageModifier, UnitBehaviour attacker);
    }
}

