using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units
{
    public class KingBehaviour :  UnitBehaviour
    {
        private UnitBehaviour stickedTarget;

        private const float viewDistance = 8;

        protected override UnitBehaviour GetTarget()
        {
            if(stickedTarget == null)
            {
                UnitBehaviour[] enemies = GetEnemies();
                if(enemies.Length > 0)
                {
                    UnitBehaviour closestEnemy = enemies[0];
                    float closestDistance = Vector2.Distance(transform.position, closestEnemy.transform.position);
                    foreach(UnitBehaviour enemy in enemies)
                    {
                        float distance = Vector2.Distance(transform.position, enemy.transform.position);
                        if(distance < closestDistance)
                        {
                            closestEnemy = enemy;
                            closestDistance = distance;
                        }
                    }
                    if(closestDistance <= viewDistance)
                    {
                        stickedTarget = closestEnemy;
                        return closestEnemy;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else if(Vector2.Distance(transform.position, stickedTarget.transform.position) <= viewDistance)
            {
                return stickedTarget;
            }
            else
            {
                stickedTarget = null;
                return null;
            }
        }

        public override UnitBehaviour[] GetEnemies()
        {
            return MinionBehaviour.minions.ToArray();
        }

        public override UnitBehaviour[] GetFriendlies()
        {
            return LegionnaireBehaviour.legionnaires.ToArray();
        }

        protected override Vector2? GetDefaultTargetPosition()
        {
            return null;
        }
    }
}