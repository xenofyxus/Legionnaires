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
                    float damage = UnityEngine.Random.Range(damageMin, damageMax);

                    StatModifier damageModifier = new StatModifier();

                    var postDamageEffects = new List<PostDamageEffect>();

                    foreach(Spells.OnHits.OnHit onHit in GetComponents<Spells.OnHits.OnHit>())
                    {
                        PostDamageEffect postDamageEffect;
                        onHit.Hit(damage, damageModifier, target, out postDamageEffect);
                        if(postDamageEffect != null)
                            postDamageEffects.Add(postDamageEffect);
                    }

                    foreach(Spells.WhenHits.WhenHit whenHit in target.GetComponents<Spells.WhenHits.WhenHit>())
                    {
                        whenHit.Hit(damage, damageModifier, owner);
                    }

                    damage = damageModifier.Modify(damage);

                    damage *= DamageRatios.GetRatio(target.armorType, attackType);

                    target.ApplyDamage(damage);

                    StatModifier healModifier = new StatModifier();

                    foreach(var postDamageEffect in postDamageEffects)
                    {
                        postDamageEffect(damage, healModifier, target);
                    }

                    if(owner != null)
                    {
                        
                        // Applies negative damage which gives negative heals the ability to kill the owner
                        if(owner.ApplyDamage(-healModifier.Modify(0)))
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