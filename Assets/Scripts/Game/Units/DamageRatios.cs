/// <summary>
/// Author: Anton Anderzén, Alexander Krantz.
/// Date: 2017-04-24
/// Edit:
/// Date:
/// Class dealing with the damagemultiplier to be applied with 
/// </summary>


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units
{

	public static class DamageRatios
	{
		public static float GetRatio (ArmorType targetArmor, AttackType unitAttack)
		{
			float ratio = 1.00f;

			switch (targetArmor) {

			case ArmorType.Invulnerable:
				ratio = 0.00f;
				break;

			case ArmorType.Unarmored:
				switch (unitAttack) {
				case AttackType.Magic:
					ratio = 1.15f;
					break;
				case AttackType.Normal:
					ratio = 1.15f;
					break;
				case AttackType.Pierce:
					ratio = 1.15f;
					break;
				case AttackType.Blunt:
					ratio = 1.15f;
					break;
				case AttackType.True:
					ratio = 1.15f;
					break;
				}
				break;

			case ArmorType.Light:
				switch (unitAttack) {
				case AttackType.Magic:
					ratio = 0.90f;
					break;
				case AttackType.Normal:
					ratio = 1.10f;
					break;
				case AttackType.Pierce:
					ratio = 0.90f;
					break;
				case AttackType.Blunt:
					ratio = 1.15f;
					break;
				case AttackType.True:
					ratio = 1.00f;
					break;
				}
				break;
			case ArmorType.Medium:
				switch (unitAttack) {
				case AttackType.Magic:
					ratio = 0.90f;
					break;
				case AttackType.Normal:
					ratio = 0.90f;
					break;
				case AttackType.Pierce:
					ratio = 1.00f;
					break;
				case AttackType.Blunt:
					ratio = 1.10f;
					break;
				case AttackType.True:
					ratio = 1.00f;
					break;
				}
				break;
			case ArmorType.Heavy:
				switch (unitAttack) {
				case AttackType.Magic:
					ratio = 1.20f;
					break;
				case AttackType.Normal:
					ratio = 0.80f;
					break;
				case AttackType.Pierce:
					ratio = 1.10f;
					break;
				case AttackType.Blunt:
					ratio = 0.80f;
					break;
				case AttackType.True:
					ratio = 1.00f;
					break;
				}
				break;
			}

			return ratio;
		}
	}
}