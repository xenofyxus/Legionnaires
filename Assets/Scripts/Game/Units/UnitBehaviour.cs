/*
 * Author: Alexander Krantz
 * Author: Anton Anderzén
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Game.Units
{
    public abstract class UnitBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Gets or sets the type of the armor.
        /// </summary>
        public ArmorType armorType;

        /// <summary>
        /// Gets or sets the type of the attack.
        /// </summary>
        public AttackType attackType;

        [NonSerialized]
        public List<Spells.Auras.Aura> auras = new List<Game.Units.Spells.Auras.Aura>();

        [NonSerialized]
        public List<Spells.OnHits.OnHit> onHits = new List<Game.Units.Spells.OnHits.OnHit>();

        [NonSerialized]
        public List<Buffs.Buff> buffs = new List<Game.Units.Buffs.Buff>();

        /// <summary>
        /// Gets or sets the attack range.
        /// </summary>
        [Tooltip("Attack range in <units>")]
        public float range;

        /// <summary>
        /// Gets or sets the projectile. Set to null if there should be none.
        /// </summary>
        [Tooltip("The projectile prefab to use, leave empty if no projectile is wanted")]
        public GameObject projectile;

        /// <summary>
        /// The projectile offset along the unit's forward vector.
        /// </summary>
        [Tooltip("The offset from the center where the projectile will spawn, y axis is along the unit's forward vector")]
        public Vector2 projectileOffset;

        /// <summary>
        /// Gets or sets the movement speed.
        /// </summary>
        [Tooltip("Movement speed in <units> per second")]
        public float movementSpeed;

        /// <summary>
        /// Gets or sets the attack speed.
        /// </summary>
        [Tooltip("Number of attacks per second")]
        public float attackSpeed;

        /// <summary>
        /// Gets or sets the Hit Points.
        /// </summary>
        [Tooltip("Amount of Hit Points")]
        public float hp;

        [NonSerialized]
        public float hpMax;

        /// <summary>
        /// The HP reg defined in +HP/sec.
        /// </summary>
        [Tooltip("Amount of Hit Points to be generated per second")]
        public float hpReg;

        /// <summary>
        /// Gets or sets the maximum damage.
        /// </summary>
        [Tooltip("Maximum damage, must be higher or equal to Damage Min")]
        public float damageMax;

        /// <summary>
        /// Gets or sets the minimum damage.FFFFF
        /// </summary>
        /// <value>The minimum damage.</value>
        [Tooltip("Minimum damage, must be lower or equal to Damage Max")]
        public float damageMin;

        private float attackDeltaTime = -1;

        Animator anim;

        Collider2D[] colliders = new Collider2D[100];
        CircleCollider2D circleCollider;

        void Start()
        {
            circleCollider = GetComponent<CircleCollider2D>();
            anim = GetComponent<Animator>();
            hpMax = hp;
            onHits.AddRange(GetComponents<Spells.OnHits.OnHit>());
            auras.AddRange(GetComponents<Spells.Auras.Aura>());
            foreach(var aura in auras)
            {
                switch(aura.target)
                {
                    case Spells.Auras.AuraTarget.Friendlies:
                        aura.Apply(GetFriendlies());
                        break;
                    case Spells.Auras.AuraTarget.Enemies:
                        aura.Apply(GetEnemies());
                        break;
                    default:
                        break;
                }
            }

        }

        void Update()
        {
            UnitBehaviour target = GetTarget();

            if(attackDeltaTime >= 0)
            {
                attackDeltaTime += Time.deltaTime;
                if(attackDeltaTime >= 1f / attackSpeed)
                {
                    attackDeltaTime = -1;
                }
            }

            Vector2 lastPosition = transform.position;

            if(target == null)
            {
                Vector2 defaultTarget = GetDefaultTargetPosition();

                if(defaultTarget.x != Mathf.Infinity)
                {
                    MoveTowards(defaultTarget);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector2.down, defaultTarget - (Vector2)transform.position), 360 * Time.deltaTime);
                    if(anim != null)
                    {
                        anim.SetFloat("speed", movementSpeed * Time.deltaTime);
                        anim.SetBool("fight", false);
                    }
                }
            }
            else
            {
                if(Vector2.Distance(transform.position, target.transform.position) <= range)
                {
                    if(attackDeltaTime == -1)
                    {
                        attackDeltaTime = 0;


                        if(projectile == null)
                        {
                            float baseDamage = UnityEngine.Random.Range(damageMin, damageMax + 1);

                            float totalDamage = baseDamage;

                            var postDamageEffects = new List<PostDamageEffect>();

                            foreach(Spells.OnHits.OnHit onHit in onHits)
                            {
                                PostDamageEffect postDamageEffect;
                                totalDamage += onHit.Hit(baseDamage, target, this, out postDamageEffect);
                                if(postDamageEffect != null)
                                    postDamageEffects.Add(postDamageEffect);
                            }

                            totalDamage *= DamageRatios.GetRatio(target.armorType, attackType);

                            target.ApplyDamage(totalDamage);

                            float totalHeals = 0;

                            foreach(var postDamageEffect in postDamageEffects)
                            {
                                totalHeals += postDamageEffect(totalDamage, target, this);
                            }

                            // Applies negative damage which gives negative heals the ability to kill this unit
                            if(ApplyDamage(-totalHeals))
                                return;
                        }
                        else
                        {
                            ProjectileBehaviour newProjectile = Instantiate(projectile, (Vector2)transform.position + projectileOffset, transform.rotation).GetComponent<ProjectileBehaviour>();
                            newProjectile.transform.RotateAround(transform.position, Vector3.forward, Quaternion.Angle(Quaternion.AngleAxis(0, Vector3.forward), transform.rotation));
                            newProjectile.transform.rotation = transform.rotation;
                            newProjectile.owner = this;
                            newProjectile.target = target;

                            foreach(Spells.OnHits.OnHit onHit in onHits)
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
                        anim.SetBool("fight", true);
                }
                else
                {
                    MoveTowards(target.transform.position);
                    if(anim != null)
                        anim.SetBool("fight", false);
                }
                if(anim != null)
                    anim.SetFloat("speed", movementSpeed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector2.down, target.transform.position - transform.position), 360 * Time.deltaTime);
            }

            lastPosition = transform.position;

            // Applies negative damage which gives negative hp regeneration the ability to kill this unit
            if(ApplyDamage(-hpReg * Time.deltaTime))
                return;
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
            int colliderCount = circleCollider.OverlapCollider(new ContactFilter2D(), colliders);
            for(int i = 0; i < colliderCount; i++)
            {
                Collider2D collider = colliders[i];
                if(collider.GetComponent<ProjectileBehaviour>() == null)
                {
                    ColliderDistance2D colliderDistance = circleCollider.Distance(collider);
                    collisionOffset += (colliderDistance.pointA - colliderDistance.pointB).normalized * colliderDistance.distance;
                }
            }
            
            transform.position = (Vector2)transform.position + collisionOffset;
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
                foreach(var aura in auras)
                {
                    aura.Remove();
                }
                foreach(var buff in buffs)
                {
                    buff.Remove();
                }
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
        protected abstract UnitBehaviour GetTarget();

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
        protected abstract Vector2 GetDefaultTargetPosition();
    }

    public delegate float PostDamageEffect(float damage,UnitBehaviour target,UnitBehaviour owner);

    public enum ArmorType
    {
        Unarmored,
        Light,
        Medium,
        Heavy,
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