using System;

namespace Game.Units.Spells.Abilities
{
    public class DefendAbility:Ability
    {
        [UnityEngine.SerializeField]
        [UnityEngine.Range(0f, 1f)]
        private float damageMultiplier = 1f;

        public float DamageMultiplier
        {
            get{ return damageMultiplier; }
            set{ damageMultiplier = value; }
        }

        [UnityEngine.SerializeField]
        [UnityEngine.Range(0f, 10f)]
        private float duration = 0f;

        public float Duration
        {
            get{ return duration; }
            set{ duration = value; }
        }

        protected override void Apply(UnitBehaviour unit)
        {
            Buffs.DefendBuff[] defendBuffs = unit.GetComponents<Buffs.DefendBuff>();
            Buffs.DefendBuff activeBuff = null;

            foreach(Buffs.DefendBuff defendBuff in defendBuffs)
            {
                DefendMetaData defendBuffMetaData = (DefendMetaData)defendBuff.MetaData;
                if(defendBuffMetaData.spellName == spellName)
                {
                    activeBuff = defendBuff;
                    break;
                }
            }

            if(activeBuff == null)
            {
                Buffs.DefendBuff newBuff = unit.gameObject.AddComponent<Buffs.DefendBuff>();
                newBuff.MetaData = new DefendMetaData(spellName);
                newBuff.Duration = duration;
                newBuff.DamageMultiplier = damageMultiplier;
            }
            else
            {
                activeBuff.Duration = duration;
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

