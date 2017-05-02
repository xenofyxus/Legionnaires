using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Units.Spells.Auras
{
    public class HpRegModifierAura:Aura
    {
		[SerializeField]
		private float modifier = 0f;

        protected override void Apply(UnitBehaviour unit)
        {
			unit.HpReg.AddAdder(modifier);
        }

        protected override void Remove(UnitBehaviour unit)
        {
			unit.HpReg.RemoveAdder(modifier);
        }
    }
}

