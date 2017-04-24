using System;
using System.Collections.Generic;

namespace Game.Units.Spells.Auras
{
    public class MovementModifierAura:Aura
    {
        [UnityEngine.Range(0.5f, 2f)]
        public float movementSpeedMultiplier;

        Dictionary<UnitBehaviour, float> movementSpeedDeltas = new Dictionary<UnitBehaviour, float>();

        protected override void Applying(UnitBehaviour unit)
        {
            float newMovementSpeed = unit.movementSpeed * movementSpeedMultiplier;
            movementSpeedDeltas.Add(unit, newMovementSpeed - unit.movementSpeed);
            unit.movementSpeed = newMovementSpeed;
        }

        protected override void Removing(UnitBehaviour unit)
        {
            float movementSpeedDelta;
            if(movementSpeedDeltas.TryGetValue(unit, out movementSpeedDelta))
            {
                unit.movementSpeed -= movementSpeedDelta;
            }
        }
    }
}

