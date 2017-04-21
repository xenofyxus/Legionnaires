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
		/// <value>The type of the armor.</value>
		public ArmorType armorType;

		/// <summary>
		/// Gets or sets the type of the attack.
		/// </summary>
		/// <value>The type of the attack.</value>
		public AttackType attackType;

		/// <summary>
		/// Gets or sets the aura spells.
		/// </summary>
		/// <value>The aura spells.</value>
		private Spells.Auras.Aura[] auras;

		/// <summary>
		/// Gets or sets the on hit spells.
		/// </summary>
		/// <value>The on hit spells.</value>
		private Spells.OnHits.OnHit[] onHits;

		/// <summary>
		/// Gets or sets the attack range.
		/// </summary>
		/// <value>The range.</value>
		public int range;

		/// <summary>
		/// Gets or sets the projectile. Set to null if there should be none.
		/// </summary>
		/// <value>The projectile.</value>
		/*public Projectile Projectile {
			get;
			set;
		}*/

		/// <summary>
		/// Gets or sets the movement speed.
		/// </summary>
		/// <value>The movement speed.</value>
		public float movementSpeed;

		/// <summary>
		/// Gets or sets the attack speed.
		/// </summary>
		/// <value>The attack speed.</value>
		public float attackSpeed;

		/// <summary>
		/// Gets or sets the Hit Points.
		/// </summary>
		/// <value>The Hit Points.</value>
		public float hp;

		/// <summary>
		/// The HP reg defined in +HP/sec.
		/// </summary>
		public float hpReg;


		/// <summary>
		/// Gets or sets the maximum damage.
		/// </summary>
		/// <value>The maximum damage.</value>
		public int damageMax;

		/// <summary>
		/// Gets or sets the minimum damage.FFFFF
		/// </summary>
		/// <value>The minimum damage.</value>
		public int damageMin;

		void Start()
		{
			onHits = gameObject.GetComponents<Spells.OnHits.OnHit>();
			auras = gameObject.GetComponents<Spells.Auras.Aura>();
		}

		public void Attack(UnitBehaviour unit)
		{
			
		}
	}

	public enum ArmorType
	{
		Unarmored,
		Light,
		Heavy,
		Medium
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