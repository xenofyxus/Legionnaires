using System;

namespace Game.Units.Spells.Passives
{
	public class Passive : Spell
	{
		protected override void Start()
		{
			base.Start();
			owner.Attacking += OwnerAttacking;
			owner.Attacked += OwnerAttacked;
			owner.TakingDamage += OwnerTakingDamage;
			owner.TookDamage += OwnerTookDamage;
			owner.TakingHeal += OwnerTakingHeal;
			owner.TookHeal += OwnerTookHeal;
			owner.Dying += OwnerDying;
		}

		protected virtual void OwnerAttacking(object sender, AttackingEventArgs e)
		{
		}

		protected virtual void OwnerAttacked(object sender, AttackedEventArgs e)
		{
		}

		protected virtual void OwnerTakingDamage(object sender, TakingDamageEventArgs e)
		{
		}

		protected virtual void OwnerTookDamage(object sender, TookDamageEventArgs e)
		{
		}

		protected virtual void OwnerTakingHeal(object sender, TakingHealEventArgs e)
		{
		}

		protected virtual void OwnerTookHeal(object sender, TookHealEventArgs e)
		{
		}

		protected virtual void OwnerDying(object sender, EventArgs e)
		{
		}
	}
}

