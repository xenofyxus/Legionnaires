using System;
using UnityEngine;

namespace Game.Units.Buffs
{
    public class DotBuff:Buff
    {
        public string id;

        public int stacks;

        public float stackMultiplier;

        public float totalDamage;

        private float hpRegModifier;

        protected override void Apply()
        {
            hpRegModifier = -(Mathf.Pow(stackMultiplier, stacks - 1) * totalDamage / duration);
            owner.hpRegModifier.Adders.Add(hpRegModifier);
        }

        protected override void Removing()
        {
            owner.hpRegModifier.Adders.Remove(hpRegModifier);
        }
    }
}

