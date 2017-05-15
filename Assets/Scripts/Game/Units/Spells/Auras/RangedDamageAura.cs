using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.Auras
{
    public class RangedDamageAura : Aura
    {
        [SerializeField]
        [Range(0f, 5f)]
        private float damageMultiplier = 1f;

        public float DamageMultiplier
        {
            get{ return damageMultiplier; }
            set{ damageMultiplier = value; }
        }

        protected override void ApplyEffect(UnitBehaviour unit)
        {
            if(unit.Projectile != null)
            {
                unit.DamageMax.AddMultiplier(damageMultiplier);
                unit.DamageMin.AddMultiplier(damageMultiplier);
            }
        }

        protected override void RemoveEffect(UnitBehaviour unit)
        {
            if(unit.Projectile != null)
            {
                unit.DamageMax.RemoveMultiplier(damageMultiplier);
                unit.DamageMin.RemoveMultiplier(damageMultiplier);
            }
        }
    }
}