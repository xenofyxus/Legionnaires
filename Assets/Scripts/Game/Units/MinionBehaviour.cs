using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units
{
    public class MinionBehaviour : UnitBehaviour
    {
        [Header("Minion specific attributes")]
        public int value;

        public static List<MinionBehaviour> minions = new List<MinionBehaviour>();

        void Awake()
        {
            minions.Add(this);
        }

        void OnDestroy()
        {
            minions.Remove(this);
        }

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
                if(Vector2.Distance(transform.position, closestEnemy.transform.position) < 6)
                {
                    return closestEnemy.GetComponent<UnitBehaviour>();
                }

            }
            return null;
        }

        public override UnitBehaviour[] GetFriendlies()
        {
            GameObject[] friendlies = GameObject.FindGameObjectsWithTag("Minion");
            UnitBehaviour[] friendlyBehaviours = new UnitBehaviour[friendlies.Length];
            for(int i = 0; i < friendlies.Length; i++)
            {
                friendlyBehaviours[i] = friendlies[i].GetComponent<UnitBehaviour>();
            }
            return friendlyBehaviours;
        }

        public override UnitBehaviour[] GetEnemies()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Legionnaire");
            UnitBehaviour[] enemyBehaviours = new UnitBehaviour[enemies.Length];
            for(int i = 0; i < enemies.Length; i++)
            {
                enemyBehaviours[i] = enemies[i].GetComponent<UnitBehaviour>();
            }
            return enemyBehaviours;
        }

        protected override Vector2? GetDefaultTargetPosition()
        {
            return new Vector2(transform.position.x, -15);
        }
    }
}

