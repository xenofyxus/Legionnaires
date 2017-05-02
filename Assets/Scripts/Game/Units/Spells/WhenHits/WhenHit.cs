using System;
using UnityEngine;

namespace Game.Units.Spells.WhenHits
{
    public abstract class WhenHit : Spell
    {
        [Header("Spell Data")]

        /// <summary>
        /// The effect chance between 1 and 100 in percentages.
        /// </summary>
        [Range(1, 100)]
        public int effectChance = 100;

        [NonSerialized]
        public UnitBehaviour owner;

        protected virtual void Start()
        {
            owner = GetComponent<UnitBehaviour>();
        }

        public void Hit(UnitStat damage, UnitBehaviour attacker)
        {
            if(UnityEngine.Random.Range(1, 101) <= effectChance)
            {
                Apply(damage, attacker);
            }
        }

        protected abstract void Apply(UnitStat damage, UnitBehaviour attacker);
    }
}

