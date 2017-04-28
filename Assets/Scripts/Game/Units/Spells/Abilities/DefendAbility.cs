using System;

namespace Game.Units.Spells.Abilities
{
    public class DefendAbility:Ability
    {
        [UnityEngine.Range(0f, 1f)]
        public float damageMultiplier = 1f;

        [UnityEngine.Range(0f, 10f)]
        public float duration = 0f;

        protected override void Apply(UnitBehaviour unit)
        {
            Buffs.DefendBuff[] defendBuffs = unit.GetComponents<Buffs.DefendBuff>();
            Buffs.DefendBuff activeBuff = null;

            foreach(Buffs.DefendBuff defendBuff in defendBuffs)
            {
                DefendMetaData defendBuffMetaData = (DefendMetaData)defendBuff.metaData;
                if(defendBuffMetaData.spellName == spellName)
                {
                    activeBuff = defendBuff;
                    break;
                }
            }

            if(activeBuff == null)
            {
                Buffs.DefendBuff newBuff = unit.gameObject.AddComponent<Buffs.DefendBuff>();
                newBuff.metaData = new DefendMetaData(spellName);
                newBuff.duration = duration;
                newBuff.damageMultiplier = damageMultiplier;
            }
            else
            {
                activeBuff.duration = duration;
            }
        }

        private class DefendMetaData
        {
            public string spellName;

            public DefendMetaData(string spellName)
            {
                this.spellName = spellName;
            }
        }
    }
}

