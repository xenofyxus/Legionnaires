using System;

namespace AssemblyCSharp.Spells
{
	public class DotOnHit : OnHit
	{
		protected override void ApplyEffect (UnitBehaviour target)
		{
			target.HP -= 15;
		}
	}
}

