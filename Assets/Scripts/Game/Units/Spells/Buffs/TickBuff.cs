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
            if(tickTimer > 0f)
            {
                tickTimer += Time.deltaTime;
                if(tickTimer >= tickTime)
                {
                    tickTimer = 0f;
                }
            }
            else if(tickTimer == 0f)
            {
                ApplyTick((tickTime + (tickTime == 0f ? tickTimer : 0)));
            }
        }
    }
}

