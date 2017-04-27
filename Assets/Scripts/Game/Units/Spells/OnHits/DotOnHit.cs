using System;
using System.Collections.Generic;

namespace Game.Units.Spells.OnHits
{
    public class DotOnHit : OnHit
    {
        public string dotName;

        public float totalDamage;

        public int maxStacks;

        [UnityEngine.Range(1f, 2f)]
        public float stackMultiplier = 1f;

        [UnityEngine.Range(0f, 20f)]
        public float duration;

        protected override float ApplyEffect(float baseDamage, UnitBehaviour target, UnitBehaviour owner, out PostDamageEffect postDamageEffect)
        {
            Buffs.DotBuff[] activeDots = target.GetComponents<Buffs.DotBuff>();
            Buffs.DotBuff activeDot = null;

            foreach(Buffs.DotBuff dot in activeDots)
            {
                if(dot.id == dotName)
                {
                    activeDot = dot;
                    break;
                }
            }

            if(activeDot == null)
            {
                Buffs.DotBuff dot = target.gameObject.AddComponent<Buffs.DotBuff>();
                dot.id = dotName;
                dot.duration = duration;
                dot.totalDamage = totalDamage;
                dot.stackMultiplier = stackMultiplier;
                dot.stacks = 1;
            }
            else if(activeDot.stacks < maxStacks)
            {
                int newStacks = activeDot.stacks + 1;
                activeDot.Remove();
                Buffs.DotBuff newDot = target.gameObject.AddComponent<Buffs.DotBuff>();
                newDot.id = dotName;
                newDot.duration = duration;
                newDot.totalDamage = totalDamage;
                newDot.stackMultiplier = stackMultiplier;
                newDot.stacks = newStacks;
            }
            else
            {
                activeDot.duration = duration;
            }

            postDamageEffect = null;
            return 0f;
        }
    }
}

