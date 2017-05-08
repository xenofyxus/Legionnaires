using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.Auras
{
    public abstract class Aura : Spell
    {
        [Header("Spell data")]

        [SerializeField]
        private AuraTarget targets = AuraTarget.Friendlies;

        private List<UnitBehaviour> units;

        protected abstract void Apply(UnitBehaviour unit);

        protected abstract void Remove(UnitBehaviour unit);

        protected override void Start()
        {
            base.Start();

            switch(targets)
            {
                case AuraTarget.Friendlies:
                    units = new List<UnitBehaviour>(owner.GetFriendlies());
                    Apply(owner);
                    break;
                case AuraTarget.Enemies:
                    units = new List<UnitBehaviour>(owner.GetEnemies());
                    break;
                default:
                    break;
            }

            UnitBehaviour.UnitSpawning += delegate(object sender, EventArgs e)
            {
                if(sender != null)
                {
                    Apply(sender as UnitBehaviour);
                    units.Add(sender as UnitBehaviour);
                }
            };

            foreach(var unit in units)
            {
                if(unit != null)
                {
                    Apply(unit);
                    unit.Died += delegate(object sender, EventArgs e)
                    {
                        Remove(sender as UnitBehaviour);
                    };
                }
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
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

