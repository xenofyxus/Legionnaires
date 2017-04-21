using System;

namespace AssemblyCSharp.Spells
{
	[Serializable]
	public class BonusDamageOnHit : OnHit
	{
		public int damage;

		protected override void ApplyEffect (AssemblyCSharp.UnitBehaviour target)
		{
			target.maxHP -= damage;
		}
	}
}