using System;
using UnityEngine;

namespace Game.Units.Spells.Buffs
{
    public class DotTickBuff:TickBuff
	{
		[SerializeField]
		protected float totalDamage;

		public float TotalDamage
		{
			get{ return totalDamage; }
			set
            {
                totalDamage = value;
                dps = totalDamage / duration;
            }
		}

        private float dps;

        protected override void Start()
        {
            base.Start();
            dps = totalDamage / duration;
        }

        protected override void ApplyTick(float deltaTime)
        {
            float dummyVar;
            owner.ApplyDamage(dps * deltaTime, out dummyVar, null);
        }
			
	}
}

