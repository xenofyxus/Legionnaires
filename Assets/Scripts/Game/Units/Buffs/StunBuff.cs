using System;
using UnityEngine;

namespace Game.Units.Buffs
{
    public class StunBuff : Buff
    {
        public override void Apply()
        {	
            owner.attackSpeedModifier.Multipliers.Add(0);
            owner.movementSpeedModifier.Multipliers.Add(0);
        }

        public override void Remove()
        {
            owner.attackSpeedModifier.Multipliers.Remove(0);
            owner.movementSpeedModifier.Multipliers.Remove(0);
        }
    }
}

