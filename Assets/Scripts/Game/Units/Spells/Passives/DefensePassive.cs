using System;

namespace Game.Units.Spells.Passives
{
	public class DefensePassive:Passive
	{
		[UnityEngine.SerializeField]
		[UnityEngine.Range(0f, 1f)]
		private float damageMultiplier = 0.8f;

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

		protected override void OwnerTakingDamage(object sender, TakingDamageEventArgs e)
		{
			e.Damage.AddMultiplier(damageMultiplier);
		}
	}
}

