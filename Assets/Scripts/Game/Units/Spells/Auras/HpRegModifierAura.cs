using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Units.Spells.Auras
{
    public class HpRegModifierAura:Aura
    {
        public float hpRegModifier = 0f;

        protected override void Apply(UnitBehaviour unit)
        {
            unit.hpRegModifier.Adders.Add(hpRegModifier);
        }

        protected override void Remove(UnitBehaviour unit)
        {
            unit.hpRegModifier.Adders.Add(hpRegModifier);
        }
    }
}

