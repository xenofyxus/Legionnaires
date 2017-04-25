using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Units.Spells.Auras
{
    public class HpRegModifierAura:Aura
    {
        public float hpRegModifier = 0f;

        Dictionary<UnitBehaviour, float> hpRegDeltas = new Dictionary<UnitBehaviour, float>();

        protected override void Applying(UnitBehaviour unit)
        {
            unit.hpReg += hpRegModifier;
        }

        protected override void Removing(UnitBehaviour unit)
        {
            float hpRegDelta;
            if(hpRegDeltas.TryGetValue(unit, out hpRegDelta))
            {
                unit.hpReg -= hpRegModifier;
            }
        }
    }
}

