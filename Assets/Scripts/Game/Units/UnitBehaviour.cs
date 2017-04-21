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

		/// <summary>
		/// Gets or sets the aura spells.
		/// </summary>
		private Spells.Auras.Aura aura;

		/// <summary>
		/// Gets or sets the on hit spells.
		/// </summary>
		private Spells.OnHits.OnHit onHit;

        // TODO: Add buffs

		/// <summary>
		/// Gets or sets the attack range.
		/// </summary>
		public int range;

		/// <summary>
		/// Gets or sets the projectile. Set to null if there should be none.
		/// </summary>
        public GameObject projectile;

		/// <summary>
		/// Gets or sets the movement speed.
		/// </summary>
		public float movementSpeed;

		/// <summary>
		/// Gets or sets the attack speed.
		/// </summary>
		public float attackSpeed;

		/// <summary>
		/// Gets or sets the Hit Points.
		/// </summary>
		public float hp;

		/// <summary>
		/// The HP reg defined in +HP/sec.
		/// </summary>
		public float hpReg;


		/// <summary>
		/// Gets or sets the maximum damage.
		/// </summary>
		public int damageMax;

		/// <summary>
		/// Gets or sets the minimum damage.FFFFF
		/// </summary>
		/// <value>The minimum damage.</value>
		public int damageMin;

		void Start()
		{
			onHit = gameObject.GetComponent<Spells.OnHits.OnHit>();
			aura = gameObject.GetComponent<Spells.Auras.Aura>();
		}

		void OnUpdate()
		{

		}

        protected void Attack(UnitBehaviour unit)
		{
            
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
		Heavy
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