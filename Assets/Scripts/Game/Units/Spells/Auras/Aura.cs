using System;
using UnityEngine;

namespace Game.Units.Spells.Auras
{
    public abstract class Aura : MonoBehaviour
    {
        public AuraTarget target;

        private UnitBehaviour[] units;

        protected abstract void Apply(UnitBehaviour unit);

        protected abstract void Remove(UnitBehaviour unit);

        void Start()
        {
            UnitBehaviour owner = GetComponent<UnitBehaviour>();

            switch(target)
            {
                case AuraTarget.Friendlies:
                    units = owner.GetFriendlies();
                    break;
                case AuraTarget.Enemies:
                    units = owner.GetEnemies();
                    break;
                default:
                    break;
            }

            foreach(var unit in units)
            {
                if(unit != null)
                    Apply(unit);
            }
        }

        void OnDestroy()
        {
            foreach(var unit in units)
            {
                if(unit != null)
                    Remove(unit);
            }
        }
    }

    public enum AuraTarget
    {
        Friendlies,
        Enemies
    }
}

