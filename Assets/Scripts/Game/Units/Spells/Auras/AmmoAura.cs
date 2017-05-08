using System;
using UnityEngine;

namespace Game.Units.Spells.Auras
{
    public class AmmoAura:Aura
    {
        [SerializeField]
        [Range(1f,5f)]
        private float attackSpeedMultiplier=1f;

        protected override void Apply(UnitBehaviour unit)
        {
            if(unit.name.Contains("Marksman") || unit.name.Contains("Ranger"))
            {
                unit.AttackSpeed.AddMultiplier(attackSpeedMultiplier);
            }
        }

        protected override void Remove(UnitBehaviour unit)
        {
            if(unit.name.Contains("Marksman") || unit.name.Contains("Ranger"))
            {
                unit.AttackSpeed.RemoveMultiplier(attackSpeedMultiplier);
            }
        }
    }
}

