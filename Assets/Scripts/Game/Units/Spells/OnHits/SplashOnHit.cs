using System;
using UnityEngine;

namespace Game.Units.Spells.OnHits
{
	public class SplashOnHit:OnHit
	{
		[SerializeField]
		[UnityEngine.Range(0f, 2f)]
		protected float damageMultiplier;

		public float DamageMultiplier
		{
			get{ return damageMultiplier; }
			set{ damageMultiplier = value; }
		}

		[SerializeField]
		protected float range;

		public float Range
		{
			get{ return range; }
			set{ range = value; }
		}

		protected override void Apply(UnitStat damage, UnitBehaviour target, out PostDamageEffect postDamageEffect)
		{
			postDamageEffect = HandlePostDamageEffect;
		}

		void HandlePostDamageEffect(float damage, UnitStat healing, UnitBehaviour target)
		{
			foreach(var enemy in target.GetFriendlies())
			{
				if(UnityEngine.Vector2.Distance(target.transform.position, enemy.transform.position) <= range)
				{
					enemy.ApplyDamage(damage * damageMultiplier);
				}
			}
		}
	}
}

