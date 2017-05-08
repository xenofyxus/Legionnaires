using System;

namespace Game.Units.Spells.Passives
{
	public class KillPassive : Passive
	{
		[UnityEngine.SerializeField]
		[UnityEngine.Range(0f, 1f)]
		private float killChance = 0.1f;

		public float KillChance
		{
			get{ return killChance; }
			set{ killChance = value; }
		}

		protected override void OwnerAttacked(object sender, AttackedEventArgs eArgs)
		{
			float dummyVar = 0f;
			if(UnityEngine.Random.Range(0f, 1f) < killChance)
				eArgs.Target.ApplyDamage(1e20f, out dummyVar, owner);
		}
	}
}

