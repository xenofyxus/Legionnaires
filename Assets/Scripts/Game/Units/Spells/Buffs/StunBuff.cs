using System;
using UnityEngine;

namespace Game.Units.Spells.Buffs
{
	public class StunBuff : Buff
	{
		
		GameObject StunPrefab; 
		GameObject StunPrefabCopy;

		protected override void Apply()
		{	
			owner.AttackSpeed.AddMultiplier(0f);
			owner.MovementSpeed.AddMultiplier(0f);
			StunPrefabCopy = Instantiate(Resources.Load("StunPrefab"), owner.transform.position, Quaternion.identity) as GameObject;
		}

		protected override void Remove()
		{
			owner.AttackSpeed.RemoveMultiplier(0f);
			owner.MovementSpeed.RemoveMultiplier(0f);
			Destroy (StunPrefabCopy);
		}
			

	}
}

