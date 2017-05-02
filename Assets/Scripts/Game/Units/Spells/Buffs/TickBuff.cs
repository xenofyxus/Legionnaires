using System;
using UnityEngine;

namespace Game.Units.Spells.Buffs
{
    public abstract class TickBuff:Buff
    {
        [SerializeField]
        private float tickTime = 0f;

        public float TickTimer
        {
            get{ return tickTime; }
            set{ tickTime = value; }
        }

        private float tickTimer = 0f;

        protected abstract void ApplyTick(float deltaTime);

        protected abstract override void Apply();

        protected abstract override void Remove();

        protected override void Update()
        {
            base.Update();
            tickTimer += Time.deltaTime;
            if(tickTimer >= tickTime)
            {
                ApplyTick((tickTime + (tickTime == 0f ? tickTimer : 0)));
                tickTimer = 0f;
            }
        }
    }
}

