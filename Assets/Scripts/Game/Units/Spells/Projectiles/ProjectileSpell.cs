using System;
using UnityEngine;

namespace Game.Units.Spells.ProjectileSpells
{
	public abstract class ProjectileSpell : Spell
	{
		protected ProjectileBehaviour projectile;

		public ProjectileBehaviour Projectile
		{
			get
			{
				return this.projectile;
			}
			set
			{
				projectile = value;
			}
		}

		protected virtual void Start()
		{
			projectile = GetComponent<ProjectileBehaviour>();
			projectile.Attacked += ProjectileAttacked;
		}

		protected abstract void ProjectileAttacked(object sender, AttackedEventArgs eArgs);

		protected virtual void Update()
		{
		}

		protected virtual void OnDestroy()
		{
		}
		
	}
}

