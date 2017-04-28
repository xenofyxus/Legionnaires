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
        public int cost;
	
        public int supply;

        public static List<LegionnaireBehaviour> legionnaires = new List<LegionnaireBehaviour>();

        void Awake()
        {
            legionnaires.Add(this);
        }

        void OnDestroy()
        {
            legionnaires.Remove(this);
        }

        protected override UnitBehaviour GetTarget()
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
                //Added visionRange returns enemy if in vision.
                if(Vector2.Distance(transform.position, closestEnemy.transform.position) < 5)
                {
                    return closestEnemy;
                }
                return null;
            }
            return null;
        }

        public override UnitBehaviour[] GetFriendlies()
        {
            return legionnaires.ToArray();
        }

        public override UnitBehaviour[] GetEnemies()
        {
            return MinionBehaviour.minions.ToArray();
        }

        protected override Vector2? GetDefaultTargetPosition()
        {
            return null;
        }
    }
}

