using System;
using UnityEngine;

namespace Game.Units.Spells.Abilities
{
	public class SummonAbility:Ability
	{
		[SerializeField]
		private GameObject summonee = null;

		[SerializeField]
		private Vector2 spawnOffset = Vector2.zero;

		public GameObject Summonee {
			get {
				return this.summonee;
			}
			set {
				summonee = value;
			}
		}

		public Vector2 SpawnOffset {
			get {
				return this.spawnOffset;
			}
			set {
				spawnOffset = value;
			}
		}

		protected override void Apply()
		{
			if (this.GetComponent<Animator> () != null) 
			{
				this.GetComponent<Animator> ().SetTrigger ("summon");
			}
			Instantiate(summonee, owner.transform.position + (Vector3)spawnOffset, owner.transform.rotation);
		}
	}
}

