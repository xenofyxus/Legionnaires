using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.Abilities
{
	public abstract class Ability : Spell
	{
		[Header("Spell data")]

		[SerializeField]
		protected float cooldown;

		/// <summary>
		/// Gets or sets the cooldown.
		/// </summary>
		public float Cooldown {
			get{ return cooldown; }
			set{ cooldown = value; }
		}

		private float cooldownTimer = 0;

		protected abstract void Apply();

		protected override void Update()
		{
			base.Update();
			cooldownTimer += Time.deltaTime;
			if (cooldownTimer >= cooldown)
			{
				Apply();
				cooldownTimer = 0;
			}
		}
	}
}

