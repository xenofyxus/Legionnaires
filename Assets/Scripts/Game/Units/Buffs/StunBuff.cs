using System;
using UnityEngine;

namespace Game.Units.Buffs
{
	public class StunBuff : Buff
	{
		float movementSpeed;
		float attackSpeed;

		public override void Apply(){	
			owner.attackSpeed = 0;
			owner.movementSpeed = 0;
		}

		public override void Remove(){
			owner.attackSpeed = attackSpeed;
			owner.movementSpeed = movementSpeed;
		}

		public void setStats(float movementS, float attackS){
			movementSpeed = movementS;
			attackSpeed = attackS;
		}
	}
}

