using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.Auras
{
	public abstract class Aura : Spell
	{
		[Header("Spell data")]

		[SerializeField]
		private AuraTarget targets = AuraTarget.Friendlies;

		private List<UnitBehaviour> units;

		private void Apply(UnitBehaviour unit)
		{
			AuraMark[] marks = unit.GetComponents<AuraMark>();
			AuraMark activeMark = null;
			foreach (AuraMark mark in marks)
			{
				if (mark.auraName == spellName)
				{
					activeMark = mark;
					break;
				}
			}
			if (activeMark != null)
			{
				activeMark.auraCount++;
			}
			else
			{
				unit.gameObject.AddComponent<AuraMark>().auraName = spellName;
				ApplyEffect(unit);
			}
		}

		private void Remove(UnitBehaviour unit)
		{
			AuraMark[] marks = unit.GetComponents<AuraMark>();
			AuraMark activeMark = null;
			foreach (AuraMark mark in marks)
			{
				if (mark.auraName == spellName)
				{
					activeMark = mark;
					break;
				}
			}
			if (activeMark != null && --activeMark.auraCount == 0)
			{
				Destroy(activeMark);
				RemoveEffect(unit);
			}
		}

		protected abstract void ApplyEffect(UnitBehaviour unit);

		protected abstract void RemoveEffect(UnitBehaviour unit);

		protected override void Start()
		{
			base.Start();

			switch (targets)
			{
			case AuraTarget.Friendlies:
				units = new List<UnitBehaviour>(owner.GetFriendlies());
				Apply(owner);
				break;
			case AuraTarget.Enemies:
				units = new List<UnitBehaviour>(owner.GetEnemies());
				break;
			default:
				break;
			}

			UnitBehaviour.UnitSpawning += delegate(object sender, EventArgs e) {
				if (sender != null && this != null)
				{
					switch (targets)
					{
					case AuraTarget.Friendlies:
						if (sender.GetType() == owner.GetType())
						{
							units.Add(sender as UnitBehaviour);
							Apply(sender as UnitBehaviour);
						}
						break;
					case AuraTarget.Enemies:
						if (sender.GetType() != owner.GetType())
						{
							units.Add(sender as UnitBehaviour);
							Apply(sender as UnitBehaviour);
						}
						break;
					default:
						return;
					}
				}
			};

			foreach (var unit in units)
			{
				if (unit != null)
				{
					ApplyEffect(unit);
					unit.Died += delegate(object sender, EventArgs e) {
						Remove(sender as UnitBehaviour);
					};
				}
			}
		}

		protected override void OwnerDied(object sender, EventArgs e)
		{
			base.OwnerDied(sender, e);
			foreach (var unit in units)
			{
				if (unit != null)
					Remove(unit);
			}
		}

		private class AuraMark : Passives.Passive
		{
			public int auraCount = 1;
			public string auraName = "";
		}
	}

	public enum AuraTarget
	{
		Friendlies,
		Enemies
	}
}

