using System;

namespace Game.Units.Spells.Passives
{
	public class SplashPassive:Passive
	{
		[UnityEngine.SerializeField]
		[UnityEngine.Range(0f, 2f)]
		private float damageMultiplier;

		[UnityEngine.SerializeField]
		[UnityEngine.Range(0f, 5f)]
		private float radius;

		public float DamageMultiplier {
			get {
				return this.damageMultiplier;
			}
			set {
				damageMultiplier = value;
			}
		}

		public float Radius {
			get {
				return this.radius;
			}
			set {
				radius = value;
			}
		}

		protected override void OwnerAttacked(object sender, AttackedEventArgs e)
		{
			foreach (UnitBehaviour unit in e.Target.GetFriendlies())
			{
				float dummyVar;
				if (UnityEngine.Vector2.Distance(unit.transform.position, e.Target.transform.position) <= radius)
					unit.ApplyDamage(e.Damage * damageMultiplier, out dummyVar, null);
			}
		}
	}
}

