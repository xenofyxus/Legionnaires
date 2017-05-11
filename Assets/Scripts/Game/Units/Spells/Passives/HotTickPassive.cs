using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.Passives
{
	public class HotTickPassive:TickPassive
	{
		[SerializeField]
		[Tooltip("Leave as zero if all friendlys should be healed")]
		private int maxFriendlies = 2;

		[SerializeField]
		private float heal = 10f;

		public int MaxFriendlies {
			get {
				return this.maxFriendlies;
			}
			set {
				maxFriendlies = value;
			}
		}

		public float Heal {
			get {
				return this.heal;
			}
			set {
				heal = value;
			}
		}

		protected override void ApplyTick(float deltaTime)
		{
			List<UnitBehaviour> friendlies = new List<UnitBehaviour>(owner.GetFriendlies());
			for (int i = 0; i < maxFriendlies; i++)
			{
				UnitBehaviour target = friendlies[UnityEngine.Random.Range(0, friendlies.Count)];
				friendlies.Remove(target);

				target.ApplyHeal(heal, owner);
			}
		}
	}
}

