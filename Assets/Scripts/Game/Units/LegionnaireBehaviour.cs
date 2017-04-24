//Legionnaires
//Author: Alexander Krantz
//Updates: visionRange added by Daniel Karlsson, Victor Carle
//

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
				//Added visionRange returns enemy if in vision.
				if (Vector2.Distance(transform.position, closestEnemy.transform.position) < 5) {
					return closestEnemy.GetComponent<UnitBehaviour>();
				}
                return null;
            }
            return null;
        }

        public override UnitBehaviour[] GetFriendlies()
        {
            GameObject[] friendlies = GameObject.FindGameObjectsWithTag("Legionnaire");
            UnitBehaviour[] friendlyBehaviours = new UnitBehaviour[friendlies.Length];
            for(int i = 0; i < friendlies.Length; i++)
            {
                friendlyBehaviours[i] = friendlies[i].GetComponent<UnitBehaviour>();
            }
            return friendlyBehaviours;
        }

        public override UnitBehaviour[] GetEnemies()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Minion");
            UnitBehaviour[] enemyBehaviours = new UnitBehaviour[enemies.Length];
            for(int i = 0; i < enemies.Length; i++)
            {
                enemyBehaviours[i] = enemies[i].GetComponent<UnitBehaviour>();
            }
            return enemyBehaviours;
        }


		protected override Vector2 GetPreferredTargetPosition ()
		{
			Vector2 defaultTarget = new Vector2 (Mathf.Infinity, Mathf.Infinity);
			return defaultTarget;
        }
    }
}

