using System;
using UnityEngine;

namespace Game.Units.Spells.Passives
{
	public class StompPassive:Passive
	{
		[SerializeField]
		[Range(0f, 1f)]
		private float stompChance = 0.5f;

		[SerializeField]
		[Range(0f, 10f)]
		private float stunDuration = 1f;

		[SerializeField]
		[Range(0f, 5f)]
		private float radius = 1f;

		public float StompChance {
			get {
				return this.stompChance;
			}
			set {
				stompChance = value;
			}
		}

		public float StunDuration {
			get {
				return this.stunDuration;
			}
			set {
				stunDuration = value;
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
			base.OwnerAttacked(sender, e);
			if (UnityEngine.Random.Range(0f, 1f) < stompChance)
			{
				foreach (UnitBehaviour enemy in owner.GetEnemies())
				{
					if (Vector2.Distance(owner.transform.position, enemy.transform.position) <= radius)
					{
						Buffs.StunBuff activeBuff = enemy.GetComponent<Buffs.StunBuff>();

						if (activeBuff != null && activeBuff.Duration < stunDuration)
						{
							activeBuff.Duration = stunDuration;
						}
						else if (activeBuff == null)
						{
							Destroy(activeBuff);
							Buffs.StunBuff newStunBuff = enemy.gameObject.AddComponent<Buffs.StunBuff>();
							newStunBuff.Duration = stunDuration;
						}
					}
				}
			}
		}
	}
}

