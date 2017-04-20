using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{

	public static class DamageRatios
	{

		public static float getRatio (ArmorType targetArmor, AttackType unitAttack)
		{
			switch (armor) {
			case ArmorType.Unarmored:
				switch (unitAttack) {
				case AttackType.Magic:
					return 1.15f;
				case AttackType.Normal:
					return 1.15f;
				case AttackType.Pierce:
					return 0.90f;
				case AttackType.Blunt:
					return 1.15f;
				case AttackType.True:
					return 1.15f;
				}
					

			case ArmorType.Light:
				switch (unitAttack) {
				case AttackType.Magic:
					return 0.90f;
				case AttackType.Normal:
					return 0.90f;
				case AttackType.Pierce:
					return 0.80f;
				case AttackType.Blunt:
					return 1.10f;
				case AttackType.True:
					return 1.00f;
				}
			case ArmorType.Medium:
				switch (unitAttack) {
				case AttackType.Magic:
					return 0.80f;
				case AttackType.Normal:
					return 0.90f;
				case AttackType.Pierce:
					return 1.10f;
				case AttackType.Blunt:
					return 1.00f;
				case AttackType.True:
					return 1.00f;
				}
			case ArmorType.Heavy:
				switch (unitAttack) {
				case AttackType.Magic:
					return 1.40f;
				case AttackType.Normal:
					return 0.90f;
				case AttackType.Pierce:
					return 1.20f;
				case AttackType.Blunt:
					return 0.90f;
				case AttackType.True:
					return 1.00f;
				}
			}
		}
	}
}