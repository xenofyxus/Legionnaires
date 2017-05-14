using System;
using UnityEngine;

namespace Game.Units.Spells.Buffs
{
	public abstract class TickBuff:Buff
	{
		[SerializeField]
		private float tickTime = 0f;

		public float TickTime {
			get{ return tickTime; }
			set{ tickTime = value; }
		}

		private float tickTimer = 0f;

		protected abstract void ApplyTick(float deltaTime);

		protected override void Apply()
		{
		}

		protected override void Update()
		{
			base.Update();
			tickTimer += Time.deltaTime;
			if (tickTimer >= tickTime)
			{
				ApplyTick((tickTime + (tickTime == 0f ? tickTimer : 0)));
				tickTimer = 0f;
			}
		}
	}
}

