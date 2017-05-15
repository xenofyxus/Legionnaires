using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Units.Spells.Auras
{
    public class HpRegModifierAura:Aura
    {
		[SerializeField]
		private float modifier = 0f;

        public float Modifier
        {
            get{ return modifier; }
            set{ modifier = value; }
        }

        protected override void ApplyEffect(UnitBehaviour unit)
        {
			unit.HpReg.AddAdder(modifier);
        }

		protected override void RemoveEffect(UnitBehaviour unit)
        {
			unit.HpReg.RemoveAdder(modifier);
        }
    }
}

