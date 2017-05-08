using System;

namespace Game.Units.Spells.Buffs
{
	public class DefendBuff:Buff
	{
		[UnityEngine.SerializeField]
		private float damageMultiplier = 1f;

		public float DamageMultiplier
		{
			get{ return damageMultiplier; }
			set{ damageMultiplier = value; }
		}

		protected override void Apply()
		{
			owner.TakingDamage += OwnerTakingDamage;
		}

		void OwnerTakingDamage(object sender, TakingDamageEventArgs eArgs)
		{
			eArgs.Damage.AddMultiplier(damageMultiplier);
		}

		protected override void Remove()
		{
			owner.TakingDamage -= OwnerTakingDamage;
		}
			
	}
}

