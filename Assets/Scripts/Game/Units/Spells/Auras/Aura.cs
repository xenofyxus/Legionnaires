using System;
using UnityEngine;

namespace Game.Units.Spells.Auras
{
    public abstract class Aura : MonoBehaviour
	{
        public AuraTarget target;

        public void Apply(UnitBehaviour[] units)
        {
            foreach(var unit in units)
            {
                Applying(unit);
            }
        }

        protected abstract void Applying(UnitBehaviour unit);

        public void Remove(UnitBehaviour[] units)
        {
            foreach(var unit in units)
            {
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

