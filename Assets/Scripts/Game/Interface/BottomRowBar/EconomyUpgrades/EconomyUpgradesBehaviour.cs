using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface.BottomRowBar.EconomyUpgrades
{
	public class EconomyUpgradesBehaviour : MonoBehaviour {


		private Game.Interface.Infobar.Resources.ResourcesBehaviour resBeh;

		[SerializeField]
		private int goldIncPrice = 10;
		[SerializeField]
		private int woodIncPrice = 10;
		[SerializeField]
		private int supplyMaxPrice = 10;


		void Awake()
		{
			resBeh = Game.Interface.Infobar.Resources.ResourcesBehaviour.Current;
		}


		public void UpgradeGoldIncome(int increment)
		{
			if (resBeh.Gold >= goldIncPrice) {
				resBeh.GoldIncome = resBeh.GoldIncome + increment;
				resBeh.Gold = resBeh.Gold - goldIncPrice;
			}
		}

		public void UpgradeWoodIncome(int increment)
		{
			if (resBeh.Gold >= woodIncPrice) {
				resBeh.WoodIncome = resBeh.WoodIncome + increment;
				resBeh.Gold = resBeh.Gold - woodIncPrice;
			}
		}

		public void UpgradeSupply(int increment)
		{
			if (resBeh.Gold >= supplyMaxPrice) {
				resBeh.SupplyMax = resBeh.SupplyMax + increment;
				resBeh.Gold = resBeh.Gold - supplyMaxPrice;
			}
		}
	
	}
}