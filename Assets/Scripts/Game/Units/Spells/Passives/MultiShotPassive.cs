using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.Passives
{
	public class MultiShotPassive : Passive
	{
		[SerializeField]
		private int shotCount = 3;

		public int ShotCount
		{
			get{ return shotCount; }
			set{ shotCount = value; }
		}

		[SerializeField]
		private int splitAmount = 2;

		public int SplitAmount
		{
			get{ return splitAmount; }
			set{ splitAmount = value; }
		}

		private int shotCounter = 0;

		protected override void OwnerAttacking(object sender, AttackingEventArgs e)
		{
			if(shotCounter >= shotCount)
			{
				shotCounter = 0;

				List<UnitBehaviour> enemies = new List<UnitBehaviour>(owner.GetEnemies());
				for(int i = 0; i < splitAmount; i++)
                {
					UnitBehaviour target = null;
					while(target == null)
					{
                        if(enemies.Count == 0)
                            return;

						target = enemies[UnityEngine.Random.Range(0, enemies.Count)];

						if(Vector2.Distance(owner.transform.position, target.transform.position) > owner.Range + 1)
						{
							enemies.Remove(target);
							target = null;
						}
					}
					
					ProjectileBehaviour newProjectile = Instantiate(Owner.Projectile, (Vector2)Owner.transform.position + Owner.ProjectileOffset, transform.rotation).GetComponent<ProjectileBehaviour>();
					newProjectile.transform.RotateAround(Owner.transform.position, Vector3.forward, Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward).eulerAngles.z);
					newProjectile.transform.rotation = Owner.transform.rotation;
					newProjectile.owner = Owner;
					newProjectile.target = target;
                    newProjectile.Damage = Random.Range(owner.DamageMin, owner.DamageMax);
					if(Owner.ProjectileSpeed > 0)
						newProjectile.movementSpeed = Owner.ProjectileSpeed;
				}
			}
			else
			{
				shotCounter++;
			}
		}
	}
}