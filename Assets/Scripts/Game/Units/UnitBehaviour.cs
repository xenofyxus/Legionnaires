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
		#region Unit Attributes

		[SerializeField]
		[Multiline(3)]
		private string description = "Please insert description or user will think we sukky sukk";

		public string Description
		{
			get{ return description; }
		}

		[SerializeField]
		private ArmorType armorType = ArmorType.Unarmored;

		/// <summary>
		/// Gets the armor type.
		/// </summary>
		public ArmorType ArmorType
		{
			get{ return armorType; }
			set{ armorType = value; }
		}

		[SerializeField]
		private AttackType attackType = AttackType.Normal;

		/// <summary>
		/// Gets the attack type.
		/// </summary>
		public AttackType AttackType
		{
			get{ return attackType; }
			set{ attackType = value; }
		}

		[Header("Unit Stats")]

		[SerializeField]
		[Tooltip("Minimum attack range, the unit can't attack enemies that are closer than this")]
		private UnitStat minimumRange = 0f;

		/// <summary>
		/// Gets the minimum attack range.
		/// </summary>
		/// <value>The minimum attack range.</value>
		public UnitStat MinimumRange
		{
			get{ return minimumRange; }
			set{ minimumRange = value; }
		}

		[SerializeField]
		[Tooltip("Attack range in <units>")]
		private UnitStat range = 1f;

		/// <summary>
		/// Gets or Modifies the attack range.
		/// </summary>
		public UnitStat Range
		{
			get{ return range; }
			set{ range = value; }
		}

		[SerializeField]
		[Tooltip("Movement speed in <units> per second")]
		private UnitStat movementSpeed = 1f;

		/// <summary>
		/// Gets or Modifies the movement speed.
		/// </summary>
		public UnitStat MovementSpeed
		{
			get{ return movementSpeed; }
			set{ movementSpeed = value; }
		}

		[SerializeField]
		[Tooltip("Number of attacks per second")]
		private UnitStat attackSpeed = 1f;

		/// <summary>
		/// Gets or Modifies the attack speed.
		/// </summary>
		public UnitStat AttackSpeed
		{
			get{ return attackSpeed; }
			set{ attackSpeed = value; }
		}

		[SerializeField]
		[Tooltip("Amount of Hit Points")]
		private float hp = 50f;

		/// <summary>
		/// Gets or Modifies the Hit Points.
		/// </summary>
		public float Hp
		{
			get{ return hp; }
			set{ hp = value; }
		}

		private UnitStat hpMax;

		/// <summary>
		/// Gets or Modifies the Maximum Hit Points.
		/// </summary>
		public UnitStat HpMax
		{
			get{ return hpMax; }
			set{ hpMax = value; }
		}

		[SerializeField]
		[Tooltip("Amount of Hit Points to be generated per second")]
		private UnitStat hpReg = 1f;

		/// <summary>
		/// Gets or Modifies the HP regeneration defined in +HP/sec.
		/// </summary>
		public UnitStat HpReg
		{
			get{ return hpReg; }
			set{ hpReg = value; }
		}

		[SerializeField]
		[Tooltip("Maximum damage, must be higher or equal to Damage Min")]
		private UnitStat damageMax = 10f;

		/// <summary>
		/// Gets or Modifies the maximum damage.
		/// </summary>
		public UnitStat DamageMax
		{
			get{ return damageMax; }
			set{ damageMax = value; }
		}

		[SerializeField]
		[Tooltip("Minimum damage, must be lower or equal to Damage Max")]
		private UnitStat damageMin = 5f;

		/// <summary>
		/// Gets or Modifies the minimum damage.
		/// </summary>
		public UnitStat DamageMin
		{
			get{ return damageMin; }
			set{ damageMin = value; }
		}

		[Header("Projectile")]

		[SerializeField]
		[Tooltip("The projectile prefab to use, leave empty if no projectile is wanted")]
		private GameObject projectile;

		/// <summary>
		/// Gets or sets the projectile. Set to null if there should be none.
		/// </summary>
		public GameObject Projectile
		{
			get{ return projectile; }
			set{ projectile = value; }
		}

		[SerializeField]
		[Tooltip("The offset from the center where the projectile will spawn, y axis is along the unit's back vector")]
		private Vector2 projectileOffset;

		/// <summary>
		/// Gets or Sets the projectile offset along the unit's back vector.
		/// </summary>
		public Vector2 ProjectileOffset
		{
			get{ return projectileOffset; }
			set{ projectileOffset = value; }
		}

		[SerializeField]
		[Tooltip("The speed of the projectile, set to anything above 0 to override the projectile's default setting")]
		private float projectileSpeed;

		/// <summary>
		/// Gets or Sets the projectile speed in <units> per second.
		/// </summary>
		public float ProjectileSpeed
		{
			get{ return projectileSpeed; }
			set{ projectileSpeed = value; }
		}

		#endregion

		#region Event Handlers

		public static event EventHandler UnitSpawning;

		public event AttackingEventHandler Attacking;

		public event AttackedEventHandler Attacked;

		public event TakingDamageEventHandler TakingDamage;

		public event TookDamageEventHandler TookDamage;

		public event TakingHealEventHandler TakingHeal;

		public event TookHealEventHandler TookHeal;

		public event EventHandler Spawned;

		public event EventHandler Dying;

		public event EventHandler Died;

		public event EventHandler MoveStarted;

		public event EventHandler MoveStopped;

		#endregion

		private bool moving = false;
		private Vector2 lastPosition;

		private float attackDelayTimer = 0;

		Animator anim;

		Collider2D[] otherColliders = new Collider2D[100];
		protected CircleCollider2D thisCollider;

		private bool alive = true;

		private UnitBehaviour stickedTarget = null;

		protected virtual void Start()
		{
			hpMax = hp;

			thisCollider = GetComponent<CircleCollider2D>();

			anim = GetComponent<Animator>();

			if(UnitSpawning != null)
				UnitSpawning(this, null);

			if(Spawned != null)
				Spawned(this, null);
		}

		protected virtual void Update()
		{
			if(!alive)
				return;

			lastPosition = (Vector2)transform.position;

			if(attackDelayTimer > 0)
			{
				attackDelayTimer += Time.deltaTime;
				anim.SetBool("fight", false);
				if(attackDelayTimer >= 1f / attackSpeed)
				{
					attackDelayTimer = 0;
				}
			}

			// Stops the unit from attacking / searching for target while beging stunned / disarmed
			if(AttackSpeed != 0)
			{
				UnitBehaviour target = stickedTarget == null ? GetTarget() : stickedTarget;
			    
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
						anim.SetFloat("speed", 0f);
					}
				}
				else
				{
					Collider2D targetCollider = target.GetComponent<Collider2D>();
					float distance = thisCollider.Distance(targetCollider).distance;
					if(distance <= range && distance >= minimumRange)
					{
						stickedTarget = target;
						anim.SetFloat("speed", 0f);
						if(attackDelayTimer == 0)
						{
							anim.SetBool("fight", true);
							attackDelayTimer += Time.deltaTime;

							UnitStat damage = UnityEngine.Random.Range((int)damageMin, (int)damageMax + 1);

							OnAttacking(new AttackingEventArgs(damage, target));

							damage.AddMultiplier(DamageRatios.GetRatio(target.armorType, attackType));

							if(projectile == null)
							{
								float actualDamage;
								target.ApplyDamage(damage, out actualDamage, this);

								OnAttacked(new AttackedEventArgs(actualDamage, target));
							}
							else
							{
								ProjectileBehaviour newProjectile = Instantiate(projectile, (Vector2)transform.position + projectileOffset, transform.rotation).GetComponent<ProjectileBehaviour>();
								newProjectile.transform.RotateAround(transform.position, Vector3.forward, Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward).eulerAngles.z);
								newProjectile.transform.rotation = transform.rotation;
								newProjectile.owner = this;
								newProjectile.target = target;
								newProjectile.Damage = damage;
								newProjectile.Attacked += (sender, e) => OnAttacked(e);
								if(projectileSpeed > 0)
									newProjectile.movementSpeed = projectileSpeed;
							}
						}
						if(anim != null)
						{
							// TODO Fix and sync

						}
						RotateTowards(target.transform.position);
					}
					else if(distance >= minimumRange)
					{
						MoveTowards(target.transform.position);
					}
					else
					{
						stickedTarget = null;
					}
				}
			}
			else
			{
				//Pushar speed 0 till animatorn för att stoppa walking animation
				anim.SetFloat("speed", 0);
			}

			ApplyHeal(hpReg * Time.deltaTime, this);

			if(lastPosition != (Vector2)transform.position)
			{
				if(!moving)
					OnMoveStarted(null);
				moving = true;
			}
			else
			{
				if(moving)
					OnMoveStopped(null);
				moving = false;
			}
		}

		void OnMouseDown()
		{
			Game.Interface.TooltipBar.TowerPanel.TowerPanelBehaviour.Current.SetUnit(this);
		}

		protected virtual void OnDestroy()
		{
		}

		protected virtual void OnAttacking(AttackingEventArgs eArgs)
		{
			var handler = this.Attacking;
			if(handler != null)
				handler(this, eArgs);
		}


		protected virtual void OnAttacked(AttackedEventArgs eArgs)
		{
			var handler = this.Attacked;
			if(handler != null)
				handler(this, eArgs);
		}


		protected virtual void OnTakingDamage(TakingDamageEventArgs eArgs)
		{
			var handler = this.TakingDamage;
			if(handler != null)
				handler(this, eArgs);
		}


		protected virtual void OnTookDamage(TookDamageEventArgs eArgs)
		{
			var handler = this.TookDamage;
			if(handler != null)
				handler(this, eArgs);
		}


		protected virtual void OnTakingHeal(TakingHealEventArgs eArgs)
		{
			var handler = this.TakingHeal;
			if(handler != null)
				handler(this, eArgs);
		}


		protected virtual void OnTookHeal(TookHealEventArgs eArgs)
		{
			var handler = this.TookHeal;
			if(handler != null)
				handler(this, eArgs);
		}


		protected virtual void OnSpawned(EventArgs e)
		{
			var handler = this.Spawned;
			if(handler != null)
				handler(this, e);
		}


		protected virtual void OnDying(EventArgs e)
		{
			var handler = this.Dying;
			if(handler != null)
				handler(this, e);
		}


		protected virtual void OnMoveStarted(EventArgs e)
		{
			var handler = this.MoveStarted;
			if(handler != null)
				handler(this, e);
		}


		protected virtual void OnMoveStopped(EventArgs e)
		{
			var handler = this.MoveStopped;
			if(handler != null)
				handler(this, e);
		}


		protected virtual void OnDied()
		{
			alive = false;
			if(Died != null)
				Died(this, null);
			GameObject.Destroy(gameObject);
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
				if(collider.GetComponent<UnitBehaviour>() != null || collider.name == "Map")
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
				anim.SetFloat("speed", movementSpeed);
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
		public bool ApplyDamage(float damage, out float actualDamage, UnitBehaviour attacker)
		{
			actualDamage = 0f;
			if(!alive)
				return true;

			UnitStat modifiedDamage = damage;

			OnTakingDamage(new TakingDamageEventArgs(modifiedDamage, attacker));
			
			if(modifiedDamage < 0)
			{
				ApplyHeal(modifiedDamage, attacker);
				return false;
			}
			hp -= modifiedDamage;

			OnTookDamage(new TookDamageEventArgs(modifiedDamage, attacker));

			actualDamage = modifiedDamage;

			if(hp < 1)
			{
				OnDying(null);
				
				if(hp < 1)
				{
					OnDied();
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Applies a heal.
		/// </summary>
		/// <param name="heal">Heal.</param>
		public void ApplyHeal(float heal, UnitBehaviour healer)
		{
			UnitStat modifiedHeal = heal;

			OnTakingHeal(new TakingHealEventArgs(modifiedHeal, healer));
            
			hp += modifiedHeal;

			if(hp >= hpMax)
			{
				modifiedHeal.AddAdder(-(hpMax - hp));
				hp = hpMax;
			}

			OnTookHeal(new TookHealEventArgs(modifiedHeal, healer));
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