using System;

namespace Game.Units.Spells.Passives
{
	public class LifeStealPassive:Passive
	{
		[UnityEngine.SerializeField]
		[UnityEngine.Range(0f, 2f)]
		private float lifeStealMultiplier = 0.1f;

		public float LifeStealMultiplier
		{
			get
			{
				return this.lifeStealMultiplier;
			}
			set
			{
				lifeStealMultiplier = value;
			}
		}

		protected override void OwnerAttacked(object sender, AttackedEventArgs e)
		{
			owner.ApplyHeal(e.Damage * lifeStealMultiplier, owner);
		}
	}
}

