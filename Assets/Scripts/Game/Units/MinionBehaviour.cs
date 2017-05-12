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

		protected static List<MinionBehaviour> minions = new List<MinionBehaviour>();

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
				UnitBehaviour closestEnemy = enemies[0];
				Collider2D closestEnemyCollider = closestEnemy.GetComponent<Collider2D>();
				float closestEnemyDistance = thisCollider.Distance(closestEnemyCollider).distance;
				for (int i = 1; i < enemies.Length; i++)
				{
					closestEnemyCollider = closestEnemy.GetComponent<Collider2D>();
					closestEnemyDistance = thisCollider.Distance(closestEnemyCollider).distance;
					Collider2D enemyCollider = enemies[i].GetComponent<Collider2D>();
					float enemyDistance = thisCollider.Distance(enemyCollider).distance;

					if ((enemyDistance < closestEnemyDistance && enemyDistance >= MinimumRange) || closestEnemyDistance < MinimumRange)
					{
						closestEnemy = enemies[i];
					}
				}
				if (closestEnemyDistance <= Range + 4)
					return closestEnemy;
			}
			return null;
		}

		public override UnitBehaviour[] GetFriendlies()
		{
			List<MinionBehaviour> friendlies = new List<MinionBehaviour>(minions);
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
			return new Vector2(transform.position.x, -15);
		}
	}
}

