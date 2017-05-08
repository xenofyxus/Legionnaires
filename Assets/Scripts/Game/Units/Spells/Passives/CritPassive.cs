using System;

namespace Game.Units.Spells.Passives
{
	public class CritPassive:Passive
	{
		[UnityEngine.SerializeField]
		[UnityEngine.Range(0f, 1f)]
		private float critChance = 0.5f;

		public float CritChance
		{
			get{ return critChance; }
			set{ critChance = value; }
		}

		[UnityEngine.SerializeField]
		[UnityEngine.Range(1f, 5f)]
		private float damageMultiplier = 2f;

		public float DamageMultiplier
		{
			get{ return damageMultiplier; }
			set{ damageMultiplier = value; }
		}

		protected override void OwnerAttacking(object sender, AttackingEventArgs eArgs)
		{
			if(UnityEngine.Random.Range(0f, 1f) <= critChance)
				eArgs.Damage.AddMultiplier(damageMultiplier);
		}
	}
}

