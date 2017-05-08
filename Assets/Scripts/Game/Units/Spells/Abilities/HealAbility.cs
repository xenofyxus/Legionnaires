using System;

namespace Game.Units.Spells.Abilities
{
    public class HealAbility:Ability
    {
        public float healingAmount;

        protected override void Apply(UnitBehaviour unit)
        {
            unit.ApplyHeal(healingAmount, owner);
        }
    }
}

