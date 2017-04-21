using System;
using UnityEngine;

namespace Game.Units
{
	public class MinionBehaviour : UnitBehaviour
	{
		public int value;

        // TODO: Add king as secondary target
        protected override UnitBehaviour GetTarget()
		{
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Legionnaire");
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
	}
}

