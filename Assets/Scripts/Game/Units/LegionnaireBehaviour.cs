//Legionnaires
//Author: Alexander Krantz
//Updates: visionRange added by Daniel Karlsson, Victor Carle
//

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units
{
	public class LegionnaireBehaviour : UnitBehaviour
	{
		[Header("Legionnaire specific attributes")]

		[SerializeField]
		protected int cost;

		public int Cost {
			get{ return cost; }
			set{ cost = value; }
		}

		[SerializeField]
		protected int supply;

		public int Supply {
			get{ return supply; }
			set{ supply = value; }
		}

		private bool combatMode = false;

		public static List<LegionnaireBehaviour> legionnaires = new List<LegionnaireBehaviour>();

		public static List<LegionnaireBehaviour> Legionnaires {
			get{ return legionnaires; }
		}

		void Awake()
		{
			legionnaires.Add(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			legionnaires.Remove(this);
		}

		public override UnitBehaviour GetTarget()
		{
			UnitBehaviour[] enemies = GetEnemies();
			if (enemies.Length > 0)
			{
				UnitBehaviour closestEnemy = enemies[0];
				for (int i = 1; i < enemies.Length; i++)
				{
					if (Vector2.Distance(transform.position, enemies[i].transform.position) < Vector2.Distance(transform.position, closestEnemy.transform.position))
					{
						closestEnemy = enemies[i];
					}
				}
				//Added visionRange returns enemy if in vision.
				if (combatMode || Vector2.Distance(transform.position, closestEnemy.transform.position) < 4 + GetComponent<Game.Units.LegionnaireBehaviour>().Range)
				{
					foreach (LegionnaireBehaviour legionnaire in legionnaires)
					{
						legionnaire.combatMode = true;
						combatMode = true;
					}
					return closestEnemy;
				}
				return null;
			}
			return null;
		}

		public override UnitBehaviour[] GetFriendlies()
		{
			List<LegionnaireBehaviour> friendlies = new List<LegionnaireBehaviour>(legionnaires);
			friendlies.Remove(this);
			return friendlies.ToArray();
		}

		public override UnitBehaviour[] GetEnemies()
		{
			return MinionBehaviour.Minions.ToArray();
		}

		public override Vector2? GetDefaultTargetPosition()
		{
			return null;
		}
	}
}

