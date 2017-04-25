using System;
using UnityEngine;

namespace Game.Units.Buffs
{
    public abstract class Buff : MonoBehaviour
    {
        public float duration = 0;

        public UnitBehaviour owner;

        public abstract void Apply();

        public abstract void Remove();

        void Update()
        {
            if(duration > -1)
                duration -= Time.deltaTime;
            if(duration < 0)
            {
                if(owner.buffs.Remove(this))
                {
                    Remove();
                    GameObject.Destroy(this);
                }
            }
        }
    }
}

