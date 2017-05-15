using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.Passives
{
	public class ExplodePassive:Passive
	{
		GameObject deathAnimation;

		[Header("Spell Data")]

		[SerializeField]
		private float damage = 10f;

		[SerializeField]
		private float radius = 1f;

		public float Damage {
			get {
				return this.damage;
			}
			set {
				damage = value;
			}
		}

		public float Radius {
			get {
				return this.radius;
			}
			set {
				radius = value;
			}
		}

		protected override void OwnerDied(object sender, EventArgs e)
		{
			base.OwnerDied(sender, e);
			foreach (UnitBehaviour enemy in owner.GetEnemies())
			{
				if (Vector2.Distance(owner.transform.position, enemy.transform.position) <= radius)
				{
					float dummyVar;
					enemy.ApplyDamage(damage, out dummyVar, null);
				}
			}
		}
	}
}

