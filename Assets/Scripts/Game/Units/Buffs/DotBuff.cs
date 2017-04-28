using System;
using UnityEngine;

namespace Game.Units.Buffs
{
    public class DotBuff:Buff
    {
        public float totalDamage;

        private float hpRegModifier;

        protected override void Apply()
        {
            hpRegModifier = -(totalDamage / duration);
            owner.hpRegModifier.Adders.Add(hpRegModifier);
        }

        protected override void Remove()
        {
            owner.hpRegModifier.Adders.Remove(hpRegModifier);
        }
    }
}

