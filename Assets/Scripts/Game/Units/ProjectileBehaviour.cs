using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

namespace Game.Units
{
	public class ProjectileBehaviour : MonoBehaviour
	{
		public event AttackedEventHandler Attacked;

		/// <summary>
		/// The movement speed in <units> per second.
		/// </summary>
		[Tooltip("Movement speed in <units> per second")]
		public float movementSpeed = 2f;

		[NonSerialized]
		public UnitBehaviour owner;

		private Type ownerTeam;

		public Type OwnerTeam
		{
			get
			{
				return this.ownerTeam;
			}
		}

		private AttackType attackType;

		public AttackType AttackType
		{
			get
			{
				return this.attackType;
			}
			set
			{
				attackType = value;
			}
		}

		private float damage;

		public float Damage
		{
			get
			{
				return this.damage;
			}
			set
			{
				damage = value;
			}
		}

		/// <summary>
		/// The target which this projectile is flying towards and going to deal damage to.
		/// </summary>
		[NonSerialized]
		public UnitBehaviour target;

		private Collider2D targetCollider;
		private Vector2 targetPosition;

		private Collider2D thisCollider;

		void Start()
		{
			attackType = owner.AttackType;

			targetCollider = target.GetComponent<Collider2D>();
			thisCollider = GetComponent<Collider2D>();
			targetPosition = target.transform.position;

			ownerTeam = owner.GetType();
		}

		void Update()
		{
			if(target != null)
			{
				transform.rotation = Quaternion.FromToRotation(Vector2.down, target.transform.position - transform.position);
				transform.position = Vector2.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
				targetPosition = target.transform.position;
				if(thisCollider.Distance(targetCollider).isOverlapped)
				{
					float actualDamage;
					target.ApplyDamage(damage, out actualDamage, owner);

					OnAttacked(new AttackedEventArgs(actualDamage, target));

					GameObject.Destroy(gameObject);
				}
			}
			else
			{
				transform.rotation = Quaternion.FromToRotation(Vector2.down, targetPosition - (Vector2)transform.position);
				transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
				if((Vector2)transform.position == targetPosition)
				{
					OnAttacked(new AttackedEventArgs(damage, target));
					GameObject.Destroy(gameObject);
				}
			}
		}

		protected virtual void OnAttacked(AttackedEventArgs eArgs)
		{
			var handler = this.Attacked;
			if(handler != null)
				handler(this, eArgs);
		}
    	
	}
}