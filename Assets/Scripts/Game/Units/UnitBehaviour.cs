/*
 * Author: Alexander Krantz
 * Author: Anton Anderzén
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private Spells.Auras.Aura aura;

        private Spells.OnHits.OnHit onHit;

        private List<Buffs.Buff> buffs = new List<Game.Units.Buffs.Buff>();

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

        void Start()
        {
            onHit = GetComponent<Spells.OnHits.OnHit>();
            aura = gameObject.GetComponent<Spells.Auras.Aura>();
        }

        void Update()
        {
            UnitBehaviour target = GetTarget();

            if(target != null)
            {

                if(attackDeltaTime >= 0)
                {
                    attackDeltaTime += Time.deltaTime;
                    if(attackDeltaTime >= 1f / attackSpeed)
                    {
                        attackDeltaTime = -1;
                    }
                }

                if(Vector2.Distance(transform.position, target.transform.position) <= range)
                {
                    if(attackDeltaTime == -1)
                    {
                        attackDeltaTime = 0;

                        float damage = UnityEngine.Random.Range(damageMin, damageMax + 1);

                        if(onHit)
                        {
                            float? newDamage = onHit.Hit(damage, target);
                            if(newDamage != null)
                                damage = (int)newDamage;
                        }

                        damage *= DamageRatios.GetRatio(target.armorType, attackType);

                        target.SendMessage("ApplyDamage", damage);
                    }
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
                }

                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector2.down, target.transform.position - transform.position), 360*Time.deltaTime);
            }
        }

        public void ApplyDamage(int damage)
        {
            hp -= damage;
            if(hp < 1)
            {
                // TODO maybe remove aura buffs?
                GameObject.Destroy(gameObject);
            }
        }

        /// <summary>
        /// Gets the target to attack.
        /// </summary>
        /// <returns>The target.</returns>
        protected abstract UnitBehaviour GetTarget();
    }

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