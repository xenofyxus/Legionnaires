using System;
using UnityEngine;

namespace Game.Units
{
    public class LegionnaireBehaviour : UnitBehaviour
    {
        public int cost;

        public int supply;

        protected override UnitBehaviour GetTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Minion");
            if(enemies.Length > 0)
            {
                GameObject closestEnemy = enemies[0];
                for(int i = 1; i < enemies.Length; i++)
                {
                    if(Vector2.Distance(transform.position, enemies[i].transform.position) < Vector2.Distance(transform.position, closestEnemy.transform.position))
                    {
                        closestEnemy = enemies[i];
                    }
                }
                return closestEnemy.GetComponent<UnitBehaviour>();
            }
            return null;
        }

        protected override UnitBehaviour[] GetFriendlies()
        {
            GameObject[] friendlies = GameObject.FindGameObjectsWithTag("Legionnaire");
            UnitBehaviour[] friendlyBehaviours = new UnitBehaviour[friendlies.Length];
            for(int i = 0; i < friendlies.Length; i++)
            {
                friendlyBehaviours[i] = friendlies[i].GetComponent<UnitBehaviour>();
            }
            return friendlyBehaviours;
        }

        protected override UnitBehaviour[] GetEnemies()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Minion");
            UnitBehaviour[] enemyBehaviours = new UnitBehaviour[enemies.Length];
            for(int i = 0; i < enemies.Length; i++)
            {
                enemyBehaviours[i] = enemies[i].GetComponent<UnitBehaviour>();
            }
            return enemyBehaviours;
        }
    }
}

