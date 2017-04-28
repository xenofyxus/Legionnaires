/*
 * Author: Alexander Krantz
 * Author: Anton Anderzén
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace Game.Units
{
    public abstract class UnitBehaviour : MonoBehaviour
    {
        [Header("Unit Stats")]

        /// <summary>
        /// Gets or sets the type of the armor.
        /// </summary>
        public ArmorType armorType;

        /// <summary>
        /// Gets or sets the type of the attack.
        /// </summary>
        public AttackType attackType;

        /// <summary>
        /// Gets or sets the attack range.
        /// </summary>
        [Tooltip("Attack range in <units>")]
        public float range;

        private float rangeBase;

        [NonSerialized]
        public StatModifier rangeModifier = new StatModifier();

        /// <summary>
        /// Gets or sets the movement speed.
        /// </summary>
        [Tooltip("Movement speed in <units> per second")]
        public float movementSpeed;

        private float movementSpeedBase;

        [NonSerialized]
        public StatModifier movementSpeedModifier = new StatModifier();

        /// <summary>
        /// Gets or sets the attack speed.
        /// </summary>
        [Tooltip("Number of attacks per second")]
        public float attackSpeed;

        private float attackSpeedBase;

        [NonSerialized]
        public StatModifier attackSpeedModifier = new StatModifier();

        /// <summary>
        /// Gets or sets the Hit Points.
        /// </summary>
        [Tooltip("Amount of Hit Points")]
        public float hp;

        [NonSerialized]
        public float hpMax;

        private float hpMaxBase;

        [NonSerialized]
        public StatModifier hpMaxModifier = new StatModifier();

        /// <summary>
        /// The HP reg defined in +HP/sec.
        /// </summary>
        [Tooltip("Amount of Hit Points to be generated per second")]
        public float hpReg;

        private float hpRegBase;

        [NonSerialized]
        public StatModifier hpRegModifier = new StatModifier();

        /// <summary>
        /// Gets or sets the maximum damage.
        /// </summary>
        [Tooltip("Maximum damage, must be higher or equal to Damage Min")]
        public float damageMax;

        private float damageMaxBase;

        [NonSerialized]
        public StatModifier damageMaxModifier = new StatModifier();

        /// <summary>
        /// Gets or sets the minimum damage.FFFFF
        /// </summary>
        /// <value>The minimum damage.</value>
        [Tooltip("Minimum damage, must be lower or equal to Damage Max")]
        public float damageMin;

        private float damageMinBase;

        [NonSerialized]
        public StatModifier damageMinModifier = new StatModifier();

        [Header("Projectile")]

        /// <summary>
        /// Gets or sets the projectile. Set to null if there should be none.
        /// </summary>
        [Tooltip("The projectile prefab to use, leave empty if no projectile is wanted")]
        public GameObject projectile;

        /// <summary>
        /// The projectile offset along the unit's back vector.
        /// </summary>
        [Tooltip("The offset from the center where the projectile will spawn, y axis is along the unit's back vector")]
        public Vector2 projectileOffset;

        /// <summary>
        /// The projectile speed.
        /// </summary>
        [Tooltip("The speed of the projectile, set to anything above 0 to override the projectile's default setting")]
        public float projectileSpeed;

        private float attackDelayTimer = 0;

        Animator anim;

        Collider2D[] otherColliders = new Collider2D[100];
        CircleCollider2D thisCollider;

        void Start()
        {
            hpMax = hp;

            // Setting base stats
            rangeBase = range;
            movementSpeedBase = movementSpeed;
            attackSpeedBase = attackSpeed;
            hpMaxBase = hpMax;
            hpRegBase = hpReg;
            damageMaxBase = damageMax;
            damageMinBase = damageMin;

            thisCollider = GetComponent<CircleCollider2D>();

            anim = GetComponent<Animator>();
        }

        void Update()
        {
            // Updating stats according to modifiers
            range = rangeModifier.Modify(rangeBase);
            movementSpeed = movementSpeedModifier.Modify(movementSpeedBase);
            attackSpeed = attackSpeedModifier.Modify(attackSpeedBase);
            hpMax = hpMaxModifier.Modify(hpMaxBase);
            hpReg = hpRegModifier.Modify(hpRegBase);
            damageMax = damageMaxModifier.Modify(damageMaxBase);
            damageMin = damageMinModifier.Modify(damageMinBase);

            if(attackDelayTimer > 0)
            {
                attackDelayTimer += Time.deltaTime;
                if(attackDelayTimer >= 1f / attackSpeed)
                {
                    attackDelayTimer = 0;
                }
            }

            UnitBehaviour target = GetTarget();

            if(target == null)
            {
                Vector2? defaultTarget = GetDefaultTargetPosition();

                if(defaultTarget.HasValue)
                {
                    MoveTowards(defaultTarget.Value);
                }
                else
                {
                    // TODO Fix and sync
                    anim.SetBool("fight", false);
                    anim.SetFloat("speed", 1f / 0f);

                }
            }
            else
            {
                Collider2D targetCollider = target.GetComponent<Collider2D>();
                if(thisCollider.Distance(targetCollider).distance <= range)
                {
                    if(attackDelayTimer == 0)
                    {
                        attackDelayTimer += Time.deltaTime;

                        if(projectile == null)
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
                                whenHit.Hit(damage, damageModifier, this);
                            }

                            damage = damageModifier.Modify(damage);

                            damage *= DamageRatios.GetRatio(target.armorType, attackType);

                            target.ApplyDamage(damage);

                            StatModifier healModifier = new StatModifier();

                            foreach(var postDamageEffect in postDamageEffects)
                            {
                                postDamageEffect(damage, healModifier, target);
                            }

                            if(ApplyDamage(-healModifier.Modify(0)))
                                return;
                        }
                        else
                        {
                            ProjectileBehaviour newProjectile = Instantiate(projectile, (Vector2)transform.position + projectileOffset, transform.rotation).GetComponent<ProjectileBehaviour>();
                            newProjectile.transform.RotateAround(transform.position, Vector3.forward, Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward).eulerAngles.z);
                            newProjectile.transform.rotation = transform.rotation;
                            newProjectile.owner = this;
                            newProjectile.target = target;
                            if(projectileSpeed > 0)
                                newProjectile.movementSpeed = projectileSpeed;

                            foreach(Spells.OnHits.OnHit onHit in GetComponents<Spells.OnHits.OnHit>())
                            {
                                Spells.OnHits.OnHit newOnHit = (Spells.OnHits.OnHit)newProjectile.gameObject.AddComponent(onHit.GetType());
                                FieldInfo[] fieldInfos = onHit.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                                foreach(FieldInfo fieldInfo in fieldInfos)
                                {
                                    fieldInfo.SetValue(newOnHit, fieldInfo.GetValue(onHit));
                                }
                            }
                        }
                    }
                    if(anim != null)
                    {
                        // TODO Fix and sync
                        anim.SetBool("fight", true);
                        anim.SetFloat("speed", (1f / attackSpeed) / 3 * Time.deltaTime);
                    }
                    RotateTowards(target.transform.position);
                }
                else
                {
                    MoveTowards(target.transform.position);
                }
            }

            // Applies negative damage which gives negative hp regeneration the ability to kill this unit
            if(ApplyDamage(-hpReg * Time.deltaTime))
                return;
        }

        void OnDestroy()
        {
            foreach(var aura in GetComponents<Spells.Auras.Aura>())
            {
                Destroy(aura);
            }
            foreach(var buff in GetComponents<Buffs.Buff>())
            {
                Destroy(buff);
            }
        }

        /// <summary>
        /// Moves the Unit towards a position and checks for collision.
        /// </summary>
        /// <param name="targetPos">Target position.</param>
        void MoveTowards(Vector2 targetPos)
        {
            Vector2 velocity = Vector2.ClampMagnitude(targetPos - (Vector2)transform.position, movementSpeed * Time.deltaTime);
            transform.position = (Vector2)transform.position + velocity;

            Vector2 collisionOffset = Vector2.zero;
            int colliderCount = thisCollider.OverlapCollider(new ContactFilter2D(), otherColliders);
            for(int i = 0; i < colliderCount; i++)
            {
                Collider2D collider = otherColliders[i];
                if(collider.GetComponent<ProjectileBehaviour>() == null)
                {
                    ColliderDistance2D colliderDistance = thisCollider.Distance(collider);
                    collisionOffset += (colliderDistance.pointA - colliderDistance.pointB).normalized * colliderDistance.distance;
                }
            }
            
            transform.position = (Vector2)transform.position + collisionOffset;
            RotateTowards(targetPos);

            if(anim != null)
            {
                // TODO Fix and sync
                anim.SetFloat("speed", 1f / movementSpeed * Time.deltaTime);
                anim.SetBool("fight", false);
            }
        }

        void RotateTowards(Vector2 targetPos)
        {
            Quaternion targetRotation = Quaternion.FromToRotation(Vector2.down, targetPos - (Vector2)transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime);
        }

        /// <summary>
        /// Applies damage.
        /// </summary>
        /// <returns><c>true</c>, if the unit died, <c>false</c> otherwise.</returns>
        /// <param name="damage">Damage.</param>
        /// <returns>True if the unit dies</returns>
        public bool ApplyDamage(float damage)
        {
            hp -= damage;
            if(hp < 1)
            {
                GameObject.Destroy(gameObject);
                return true;
            }
            else if(hp > hpMax)
            {
                hp = hpMax;
            }
            return false;
        }

        /// <summary>
        /// Gets the target to attack.
        /// </summary>
        /// <returns>The target.</returns>
        public abstract UnitBehaviour GetTarget();

        /// <summary>
        /// Gets all friendlies.
        /// </summary>
        /// <returns>The friendlies.</returns>
        public abstract UnitBehaviour[] GetFriendlies();

        /// <summary>
        /// Gets all enemies.
        /// </summary>
        /// <returns>The enemies.</returns>
        public abstract UnitBehaviour[] GetEnemies();

        /// <summary>
        /// Gets the default target position.
        /// </summary>
        /// <returns>The default target position.</returns>
        public abstract Vector2? GetDefaultTargetPosition();
    }

    public delegate void PostDamageEffect(float damage,StatModifier healModifier,UnitBehaviour target);

    public enum ArmorType
    {
        Unarmored,
        Light,
        Medium,
        Heavy,
        BossArmor,
        Invulnerable
    }

    public enum AttackType
    {
        Magic,
        Normal,
        Pierce,
        Blunt,
        True
    }
}