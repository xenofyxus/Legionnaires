using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.ProjectileSpells
{
	public class SplashProjectileSpell:ProjectileSpell
	{
		[Header("Spell Data")]

		[UnityEngine.SerializeField]
		[UnityEngine.Range(0f, 2f)]
		private float damageMultiplier;

		[UnityEngine.SerializeField]
		[UnityEngine.Range(0f, 5f)]
		private float radius;

		public float DamageMultiplier
		{
			get
			{
				return this.damageMultiplier;
			}
			set
			{
				damageMultiplier = value;
			}
		}

		public float Radius
		{
			get
			{
				return this.radius;
			}
			set
			{
				radius = value;
			}
		}

		protected override void ProjectileAttacked(object sender, AttackedEventArgs eArgs)
		{
			UnitBehaviour[] enemies;
			if(projectile.OwnerTeam == typeof(LegionnaireBehaviour))
			{
				enemies = MinionBehaviour.Minions.ToArray();
			}
			else
			{
				enemies = LegionnaireBehaviour.Legionnaires.ToArray();
			}
			foreach(UnitBehaviour unit in enemies)
			{
				float dummyVar;
				if(UnityEngine.Vector2.Distance(unit.transform.position, projectile.transform.position) <= radius)
				{
					if(unit != eArgs.Target)
						unit.ApplyDamage(eArgs.Damage * damageMultiplier, out dummyVar, null);
					ApplyAdditionalEffect(unit);
				}
			}
		}

		protected virtual void ApplyAdditionalEffect(UnitBehaviour unit)
		{
		}
	}
}

