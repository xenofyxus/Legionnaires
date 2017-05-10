using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units
{
	public class MinionBehaviour : UnitBehaviour
	{
		[Header("Minion specific attributes")]

		[SerializeField]
		protected int value;

		public int Value {
			get{ return value; }
			set{ this.value = value; }
		}

		protected static List<MinionBehaviour> minions = new List<MinionBehaviour> ();

		public static List<MinionBehaviour> Minions {
			get{ return minions; }
		}

		void Awake()
		{
			minions.Add(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			minions.Remove(this);
		}

		protected override void OnDied()
		{
			if (LegionnaireBehaviour.legionnaires.Count == 0)
			{
				Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Gold += value / 2;
			}
			else
			{
				Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Gold += value;
			}
			base.OnDied();
		}

		public override UnitBehaviour GetTarget()
		{
			UnitBehaviour[] enemies = GetEnemies();
			if (enemies.Length > 0)
			{
				UnitBehaviour closestEnemy = enemies [0];
				for (int i = 1;i < enemies.Length;i++)
				{
					if (Vector2.Distance(transform.position, enemies [i].transform.position) < Vector2.Distance(transform.position, closestEnemy.transform.position))
					{
						closestEnemy = enemies [i];
					}
				}
				if (Vector2.Distance(transform.position, closestEnemy.transform.position) < 6)
				{
					return closestEnemy;
				}
			}
			return null;
		}

		public override UnitBehaviour[] GetFriendlies()
		{
			List<MinionBehaviour> friendlies = new List<MinionBehaviour> (minions);
			friendlies.Remove(this);
			return friendlies.ToArray();
		}

		public override UnitBehaviour[] GetEnemies()
		{
			if (LegionnaireBehaviour.Legionnaires.Count > 0)
			{
				return LegionnaireBehaviour.Legionnaires.ToArray();
			}
			else if (KingBehaviour.Current != null)
				{
					return new UnitBehaviour[]{ KingBehaviour.Current };
				}
				else
				{
					return new UnitBehaviour[0];
				}
		}

		public override Vector2? GetDefaultTargetPosition()
		{
			return new Vector2 (transform.position.x, -15);
		}
	}
}

