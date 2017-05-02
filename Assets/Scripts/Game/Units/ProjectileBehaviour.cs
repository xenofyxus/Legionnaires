using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

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
			damageMax = owner.DamageMax;
			damageMin = owner.DamageMin;
			attackType = owner.AttackType;

            targetCollider = target.GetComponent<Collider2D>();
            thisCollider = GetComponent<Collider2D>();
            targetPosition = target.transform.position;

			if(owner != null)
			{
				foreach(Spells.OnHits.OnHit onHit in owner.GetComponents<Spells.OnHits.OnHit>())
				{
					Spells.OnHits.OnHit newOnHit = (Spells.OnHits.OnHit)gameObject.AddComponent(onHit.GetType());
					FieldInfo[] fieldInfos = onHit.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
					foreach(FieldInfo fieldInfo in fieldInfos)
					{
						fieldInfo.SetValue(newOnHit, fieldInfo.GetValue(onHit));
					}
				}
			}
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
					UnitStat damage = UnityEngine.Random.Range((int)damageMin, (int)damageMax + 1);

                    var postDamageEffects = new List<PostDamageEffect>();

					foreach(Spells.OnHits.OnHit onHit in GetComponents<Spells.OnHits.OnHit>())
                    {
                        PostDamageEffect postDamageEffect;
                        onHit.Hit(damage, target, out postDamageEffect);
                        if(postDamageEffect != null)
                            postDamageEffects.Add(postDamageEffect);
                    }

					damage *= DamageRatios.GetRatio(target.ArmorType, attackType);

					target.ApplyDamage(damage);

					UnitStat healing = 0f;

                    foreach(var postDamageEffect in postDamageEffects)
                    {
						postDamageEffect(damage, healing, target);
                    }

                    if(owner != null)
                    {
                        // Applies negative damage which gives negative heals the ability to kill the owner
                        if(owner.ApplyDamage(-healing))
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