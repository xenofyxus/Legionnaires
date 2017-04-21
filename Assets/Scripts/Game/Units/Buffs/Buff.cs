using System;
using UnityEngine;

namespace Game.Units.Buffs
{
    public abstract class Buff : MonoBehaviour
    {
        public int duration;

        [NonSerialized]
        public GameObject auraOwner;

        [NonSerialized]
        public bool isAura;

        public abstract void Apply(UnitBehaviour unit);

        public abstract void Remove(UnitBehaviour unit);
    }
}

