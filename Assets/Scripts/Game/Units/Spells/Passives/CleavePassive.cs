using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.Passives
{
    public class CleavePassive:Passive
    {
        [SerializeField]
        [Range(0f, 2f)]
        private float damageMultiplier = 1f;

        public float DamageMultiplier
        {
            get
            {
                return this.damageMultiplier;
            }
            set
            {
                damageMultiplier = value;
            }
        }

        protected override void OwnerAttacked(object sender, AttackedEventArgs e)
        {
            foreach(UnitBehaviour enemy in owner.GetEnemies())
            {
                if(Vector2.Distance(owner.transform.position, enemy.transform.position) <= owner.Range)
                {
                    float dummyVar;
                    enemy.ApplyDamage(e.Damage * damageMultiplier, out dummyVar, owner);
                }
            }
        }
    }
}

