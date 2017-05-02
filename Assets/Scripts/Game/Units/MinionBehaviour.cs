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

		public int Value
		{
			get{ return value; }
			set{ this.value = value; }
		}

        public static List<MinionBehaviour> minions = new List<MinionBehaviour>();

        void Awake()
        {
            minions.Add(this);
        }

	    protected override void OnDestroy()
		{
			base.OnDestroy();
			minions.Remove(this);
		}

        public override UnitBehaviour GetTarget()
        {
            UnitBehaviour[] enemies = GetEnemies();
            if(enemies.Length > 0)
            {
                UnitBehaviour closestEnemy = enemies[0];
                for(int i = 1; i < enemies.Length; i++)
                {
                    if(Vector2.Distance(transform.position, enemies[i].transform.position) < Vector2.Distance(transform.position, closestEnemy.transform.position))
                    {
                        closestEnemy = enemies[i];
                    }
                }
                if(Vector2.Distance(transform.position, closestEnemy.transform.position) < 6)
                {
                    return closestEnemy;
                }
            }
            return null;
        }

        public override UnitBehaviour[] GetFriendlies()
        {
            return minions.ToArray();
        }

        public override UnitBehaviour[] GetEnemies()
        {
            GameObject kingObject = GameObject.FindGameObjectWithTag("King");
            UnitBehaviour[] enemies = new UnitBehaviour[LegionnaireBehaviour.legionnaires.Count + (kingObject != null ? 1 : 0)];

            for(int i = 0; i < LegionnaireBehaviour.legionnaires.Count; i++)
            {
                enemies[i] = LegionnaireBehaviour.legionnaires[i];
            }

            if(kingObject != null)
                enemies[enemies.Length - 1] = kingObject.GetComponent<KingBehaviour>();
            
            return enemies;
        }

        public override Vector2? GetDefaultTargetPosition()
        {
            return new Vector2(transform.position.x, -15);
        }
    }
}

