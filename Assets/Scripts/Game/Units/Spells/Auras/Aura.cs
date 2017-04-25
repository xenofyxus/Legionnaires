using System;
using UnityEngine;

namespace Game.Units.Spells.Auras
{
    public abstract class Aura : MonoBehaviour
    {
        public AuraTarget target;

        private UnitBehaviour[] units;

        public void Apply(UnitBehaviour[] units)
        {
            foreach(var unit in units)
            {
                Applying(unit);
            }
            this.units = units;
        }

        protected abstract void Applying(UnitBehaviour unit);

        public void Remove()
        {
            foreach(var unit in units)
            {
                if(unit != null)
                    Removing(unit);
            }
        }

        protected abstract void Removing(UnitBehaviour unit);
    }

    public enum AuraTarget
    {
        Friendlies,
        Enemies
    }
}

