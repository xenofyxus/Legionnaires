using System;
using System.Collections.Generic;

namespace Game.Units.Spells.Auras
{
    public class MovementModifierAura:Aura
    {
        [UnityEngine.Range(0.5f, 2f)]
        public float movementSpeedMultiplier;

        protected override void Applying(UnitBehaviour unit)
        {
            unit.movementSpeedModifier.Multipliers.Add(movementSpeedMultiplier);
        }

        protected override void Removing(UnitBehaviour unit)
        {
            unit.movementSpeedModifier.Multipliers.Remove(movementSpeedMultiplier);
        }
    }
}

