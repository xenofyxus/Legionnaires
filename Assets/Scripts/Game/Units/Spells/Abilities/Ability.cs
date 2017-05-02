using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.Abilities
{
	public abstract class Ability : Spell
	{
		[Header("Spell data")]

		[SerializeField]
		protected AbilityTarget targets;

		/// <summary>
		/// Gets or sets the type of targets.
		/// </summary>
		public AbilityTarget Targets
		{
			get{ return targets; }
			set{ targets = value; }
		}

		[SerializeField]
		protected float cooldown;

		/// <summary>
		/// Gets or sets the cooldown.
		/// </summary>
		public float Cooldown
		{
			get{ return cooldown; }
			set{ cooldown = value; }
		}

		private float cooldownTimer = 0;

		protected UnitBehaviour owner;

		protected abstract void Apply(UnitBehaviour unit);

		void Start()
		{
			owner = GetComponent<UnitBehaviour>();
		}

		void Update()
		{
			if(cooldownTimer == 0)
			{
				UnitBehaviour target = null;
				switch(targets)
				{
				case AbilityTarget.Friendlies:
					List<UnitBehaviour> friendlies = new List<UnitBehaviour>(owner.GetFriendlies());
					while(target == null)
					{
						target = friendlies[UnityEngine.Random.Range(0, friendlies.Count)];
						if(Vector2.Distance(owner.transform.position, target.transform.position) > owner.Range)
						{
							target = null;
							friendlies.Remove(target);
							if(friendlies.Count == 0)
								return;
						}
					}
					break;
				case AbilityTarget.Enemies:
					target = owner.GetTarget();
					if(target == null)
						return;
					break;
				default:
					break;
				}
				Apply(target);
			}
			else if(cooldownTimer < cooldown)
			{
				cooldownTimer += Time.deltaTime;
			}
			else
			{
				cooldownTimer = 0;
			}
		}
	}

	public enum AbilityTarget
	{
		Friendlies,
		Enemies
	}
}

