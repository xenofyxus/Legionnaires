using System;
using UnityEngine;

namespace Game.Units.Spells.Passives
{
	public abstract class TickPassive:Passive
	{
		[Header("Spell data")]

		[SerializeField]
		private float tickTime = 1f;

		public float TickTime {
			get {
				return this.tickTime;
			}
			set {
				tickTime = value;
			}
		}

		private float tickTimer = 0f;

		protected override void Update()
		{
			base.Update();
			tickTimer += Time.deltaTime;
			if (tickTimer >= tickTime)
			{
				ApplyTick(tickTime == 0f ? tickTimer : tickTime);
				tickTimer = 0f;
			}
		}

		protected abstract void ApplyTick(float deltaTime);
	}
}

