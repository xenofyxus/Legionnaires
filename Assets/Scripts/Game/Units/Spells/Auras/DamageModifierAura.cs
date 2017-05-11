using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Units.Spells.Auras
{
    public class DamageModifierAura:Aura
    {
		[SerializeField]
        [Range(0f, 2f)]
		private float multiplier = 0f;

        public float Multiplier
        {
            get{ return multiplier; }
            set{ multiplier = value; }
        }
        protected override void Apply(UnitBehaviour unit)
        {
			unit.DamageMax.AddMultiplier(multiplier);
			unit.DamageMin.AddMultiplier(multiplier);
        }

        protected override void Remove(UnitBehaviour unit)
        {
			unit.DamageMax.RemoveMultiplier(multiplier);
			unit.DamageMin.RemoveMultiplier(multiplier);
        }
    }
}

