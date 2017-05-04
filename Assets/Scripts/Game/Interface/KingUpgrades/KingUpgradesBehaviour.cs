using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface.KingUpgrades
{
    public class KingUpgradesBehaviour : MonoBehaviour
    {



        public void UpgradeHpMax()
        {
            Units.KingBehaviour.Current.HpMax.AddAdder(100);
        }

        public void UpgradeDamage()
        {
            Units.KingBehaviour.Current.DamageMax.AddAdder(10);
            Units.KingBehaviour.Current.DamageMin.AddAdder(10);
        }

        public void UpgradeHpReg()
        {
            Units.KingBehaviour.Current.HpReg.AddAdder(2);
        }
    }
}