using System;
using UnityEngine;

namespace Game.Units.Spells.Buffs
{
	public class ImmolationTickBuff:TickBuff
	{
		[SerializeField]
		private float damagePerSecond = 1f;

		public float DamagePerSecond
		{
			get{ return damagePerSecond; }
			set { damagePerSecond = value; }
		}

		[SerializeField]
		private float radius = 0f;

		public float Radius
		{
			get{ return radius; }
			set{ radius = value; }
		}

		protected override void ApplyTick(float deltaTime)
		{
			UnitBehaviour[] targets = owner.GetEnemies();
			foreach(UnitBehaviour target in targets)
			{
				float dummyVar = 0f;
				if(Vector2.Distance(target.transform.position, transform.position) <= radius)
					target.ApplyDamage(damagePerSecond * deltaTime, out dummyVar, owner);
			}
		}
	}
}

