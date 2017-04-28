using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.Units
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        /// <summary>
        /// The movement speed in <units> per second.
        /// </summary>
        [Tooltip("Movement speed in <units> per second")]
        public float movementSpeed = 2f;

        [NonSerialized]
        public UnitBehaviour owner;

        private float damageMax;
        private float damageMin;
        private AttackType attackType;

        private List<Spells.OnHits.OnHit> onHits = new List<Spells.OnHits.OnHit>();

        /// <summary>
        /// The target which this projectile is flying towards and going to deal damage to.
        /// </summary>
        [NonSerialized]
        public UnitBehaviour target;

        private Collider2D targetCollider;
        private Vector2 targetPosition;

        private Collider2D thisCollider;

        // Use this for initialization
        void Start()
        {
            damageMax = owner.damageMax;
            damageMin = owner.damageMin;
            attackType = owner.attackType;

            onHits.AddRange(GetComponents<Spells.OnHits.OnHit>());
            targetCollider = target.GetComponent<Collider2D>();
            thisCollider = GetComponent<Collider2D>();
            targetPosition = target.transform.position;
        }
	
        // Update is called once per frame
        void Update()
        {
            if(target != null)
            {
                transform.rotation = Quaternion.FromToRotation(Vector2.down, target.transform.position - transform.position);
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
                targetPosition = target.transform.position;
                if(thisCollider.Distance(targetCollider).isOverlapped)
                {
                    float baseDamage = UnityEngine.Random.Range(damageMin, damageMax + 1);

                    float totalDamage = baseDamage;

                    var postDamageEffects = new List<PostDamageEffect>();

                    foreach(Spells.OnHits.OnHit onHit in onHits)
                    {
                        PostDamageEffect postDamageEffect;
                        totalDamage += onHit.Hit(baseDamage, target, out postDamageEffect);
                        if(postDamageEffect != null)
                            postDamageEffects.Add(postDamageEffect);
                    }

                    totalDamage *= DamageRatios.GetRatio(target.armorType, attackType);

                    target.ApplyDamage(totalDamage);

                    float totalHeals = 0;

                    foreach(var postDamageEffect in postDamageEffects)
                    {
                        totalHeals += postDamageEffect(totalDamage, target, owner);
                    }

                    if(owner != null)
                    {
                        // Applies negative damage which gives negative heals the ability to kill the owner
                        if(owner.ApplyDamage(-totalHeals))
                            return;
                    }

                    GameObject.Destroy(gameObject);
                }
            }
            else
            {
                transform.rotation = Quaternion.FromToRotation(Vector2.down, targetPosition - (Vector2)transform.position);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
                if((Vector2)transform.position == targetPosition)
                    GameObject.Destroy(gameObject);
            }
        }
    }
}