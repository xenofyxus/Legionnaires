using System;
using UnityEngine;

namespace Game.Units.Spells.Auras
{
	public abstract class Aura : Spell
	{
		[Header("Spell data")]

		[SerializeField]
		private AuraTarget targets = AuraTarget.Friendlies;

		private UnitBehaviour[] units;

		protected abstract void Apply(UnitBehaviour unit);

		protected abstract void Remove(UnitBehaviour unit);

		protected virtual void Start()
		{
			UnitBehaviour owner = GetComponent<UnitBehaviour>();

			switch(targets)
			{
			case AuraTarget.Friendlies:
				units = owner.GetFriendlies();
				break;
			case AuraTarget.Enemies:
				units = owner.GetEnemies();
				break;
			default:
				break;
			}

			foreach(var unit in units)
			{
				if(unit != null)
					Apply(unit);
			}
		}

		protected virtual void OnDestroy()
		{
			foreach(var unit in units)
			{
				if(unit != null)
					Remove(unit);
			}
		}
	}

	public enum AuraTarget
	{
		Friendlies,
		Enemies
	}
}

