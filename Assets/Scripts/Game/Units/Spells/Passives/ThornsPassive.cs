using System;

namespace Game.Units.Spells.Passives
{
	public class ThornsPassive:Passive
	{
		[UnityEngine.SerializeField]
		private float returnedDamage = 10f;

		public float ReturnedDamage
		{
			get
			{
				return this.returnedDamage;
			}
			set
			{
				returnedDamage = value;
			}
		}

		protected override void OwnerTookDamage(object sender, TookDamageEventArgs e)
		{
			float dummyVar;
			if(e.Attacker != null)
				e.Attacker.ApplyDamage(returnedDamage, out dummyVar, null);
		}
	}
}

