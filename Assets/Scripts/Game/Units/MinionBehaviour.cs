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
				if (Vector2.Distance (transform.position, closestEnemy.transform.position) < 6) {
					return closestEnemy.GetComponent<UnitBehaviour>();
				}

			}
            return null;
		}
			
		protected override Vector2 GetPreferredTargetPosition ()
		{
			Vector2 defaultPosition = new Vector2(transform.position.x, -15);
			return defaultPosition;
		}
	}
}

