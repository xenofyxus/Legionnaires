using System;
using System.Collections.Generic;

namespace Game.Units.Spells.OnHits
{
    public class DotOnHit : OnHit
    {
        public float totalDamage;

        public int maxStacks;

        [UnityEngine.Range(1f, 2f)]
        public float stackMultiplier = 1f;

        [UnityEngine.Range(0f, 20f)]
        public float duration;

        protected override void Apply(float baseDamage, StatModifier modifier, UnitBehaviour target, out PostDamageEffect postDamageEffect)
        {
            Buffs.DotBuff[] activeDots = target.GetComponents<Buffs.DotBuff>();
            Buffs.DotBuff activeDot = null;


            foreach(Buffs.DotBuff dot in activeDots)
            {
                DotMetaData dotMetaData = dot.metaData as DotMetaData;
                if(dotMetaData.dotName == spellName)
                {
                    activeDot = dot;
                    break;
                }
            }


            if(activeDot == null)
            {
                Buffs.DotBuff dot = target.gameObject.AddComponent<Buffs.DotBuff>();

                dot.metaData = new DotMetaData(spellName);
                DotMetaData dotMetaData = dot.metaData as DotMetaData;

                dotMetaData.stacks = 1;

                dot.duration = duration;
                dot.totalDamage = (float)Math.Pow(stackMultiplier, 0) * totalDamage;
            }
            else
            {
                DotMetaData activeDotMetaData = activeDot.metaData as DotMetaData;
                if(activeDotMetaData.stacks < maxStacks)
                {
                    int newStacks = activeDotMetaData.stacks + 1;
                    Destroy(activeDot);

                    Buffs.DotBuff newDot = target.gameObject.AddComponent<Buffs.DotBuff>();

                    newDot.metaData = new DotMetaData(spellName);
                    DotMetaData newDotMetaData = newDot.metaData as DotMetaData;

                    newDotMetaData.stacks = newStacks;

                    newDot.duration = duration;
                    newDot.totalDamage = (float)Math.Pow(stackMultiplier, newStacks - 1) * totalDamage;
                }
                else
                {
                    activeDot.duration = duration;
                }
            }

            postDamageEffect = null;
        }

        private class DotMetaData
        {
            public string dotName;

            public int stacks;

            public DotMetaData(string dotName)
            {
                this.dotName = dotName;
                this.stacks = 1;
            }
        }
    }
}

