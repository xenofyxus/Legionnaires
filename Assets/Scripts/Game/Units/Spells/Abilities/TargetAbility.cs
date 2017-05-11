using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.Abilities
{
	public abstract class TargetAbility:Ability
	{
		[SerializeField]
		protected AbilityTarget targets;

		/// <summary>
		/// Gets or sets the type of targets.
		/// </summary>
		public AbilityTarget Targets {
			get{ return targets; }
			set{ targets = value; }
		}

		protected abstract void Apply(UnitBehaviour unit);

		protected override void Apply()
		{
			UnitBehaviour target = null;
			switch (targets)
			{
			case AbilityTarget.Friendlies:
				List<UnitBehaviour> friendlies = new List<UnitBehaviour>(owner.GetFriendlies());
				while (target == null)
				{
					target = friendlies[UnityEngine.Random.Range(0, friendlies.Count)];
					if (Vector2.Distance(owner.transform.position, target.transform.position) > owner.Range)
					{
						target = null;
						friendlies.Remove(target);
						if (friendlies.Count == 0)
							return;
					}
				}
				break;
			case AbilityTarget.Enemies:
				target = owner.GetTarget();
				if (target == null)
					return;
				break;
			default:
				break;
			}
			Apply(target);
		}
	}

	public enum AbilityTarget
	{
		Friendlies,
		Enemies
	}
}

