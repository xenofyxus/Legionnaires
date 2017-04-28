using System;
using UnityEngine;

namespace Game.Units.Buffs
{
    public abstract class Buff : MonoBehaviour
    {
        public float duration = 0;

        [NonSerialized]
        public object metaData;

        protected UnitBehaviour owner;

        protected abstract void Apply();

        protected abstract void Remove();

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
                Destroy(this);
            }
        }

        void OnDestroy()
        {
            Remove();
        }
    }
}

