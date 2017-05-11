using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.Auras
{
    public class MovementModifierAura:Aura
    {
		[SerializeField]
        [UnityEngine.Range(0.5f, 2f)]
		private float multiplier = 1f;

        public float Multiplier
        {
            get{ return multiplier; }
            set{ multiplier = value; }
        }

        protected override void Apply(UnitBehaviour unit)
        {
			unit.MovementSpeed.AddMultiplier(multiplier);
        }

		protected override void Remove(UnitBehaviour unit)
        {
			unit.MovementSpeed.RemoveMultiplier(multiplier);
        }
    }
}

