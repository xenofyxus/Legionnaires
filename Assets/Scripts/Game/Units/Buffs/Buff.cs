using System;
using UnityEngine;

namespace Game.Units.Buffs
{
    public abstract class Buff : MonoBehaviour
    {
        public float duration = 0;

        public UnitBehaviour owner;

        protected abstract void Apply();

        public void Remove()
        {
            Removing();
            GameObject.Destroy(this);
        }

        protected abstract void Removing();

        void Start()
        {
            owner = GetComponent<UnitBehaviour>();
            Apply();
        }

        void Update()
        {
            if(duration > 0)
                duration -= Time.deltaTime;
            else
            {
                Remove();
            }
        }
    }
}

