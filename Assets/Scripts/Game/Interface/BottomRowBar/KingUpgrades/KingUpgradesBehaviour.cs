using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface.BottomRowBar.KingUpgrades
{
    public class KingUpgradesBehaviour : MonoBehaviour
    {
        private Game.Interface.Infobar.Resources.ResourcesBehaviour resources;

        void Awake()
        {
            resources = Game.Interface.Infobar.Resources.ResourcesBehaviour.Current;
        }

        [Header("Damage")]

        [SerializeField]
        private int damageCost = 10;
        [SerializeField]
        private int damageGain = 10;

        [Header("HP Reg")]

        [SerializeField]
        private int hpRegCost = 10;
        [SerializeField]
        private int hpRegGain = 10;

        [Header("HP")]

        [SerializeField]
        private int hpCost = 10;
        [SerializeField]
        private int hpGain = 10;

        [Header("Shockwave")]

        [SerializeField]
        private int shockwaveCost = 10;
        [SerializeField]
        private int shockwaveDamageGain = 10;
        [SerializeField]
        private int shockwaveRangeGain = 10;

        [Header("Stomp")]

        [SerializeField]
        private int stompCost = 10;
        [SerializeField]
        private int stompDamageGain = 10;
        [SerializeField]
        private int stompDurationGain = 10;

        [Header("Immolation")]

        [SerializeField]
        private int immolationCost = 10;
        [SerializeField]
        private int immolationDpsGain = 10;
        [SerializeField]
        private int immolationRadiusGain = 10;

        [Header("Thorns")]

        [SerializeField]
        private int thornsCost = 10;
        [SerializeField]
        private int thornsDamageGain = 10;

        public void UpgradeDamage()
        {
            if(resources.TryPayingWood(damageCost))
            {
                Units.KingBehaviour.Current.DamageMax.AddAdder(damageGain);
                Units.KingBehaviour.Current.DamageMin.AddAdder(damageGain);
            }
        }

        public void UpgradeHpReg()
        {
            if(resources.TryPayingWood(hpRegCost))
            {
                Units.KingBehaviour.Current.HpReg.AddAdder(hpRegGain);
            }
        }

        public void UpgradeHpMax()
        {
            if(resources.TryPayingWood(hpCost))
            {
                Units.KingBehaviour king = Units.KingBehaviour.Current;
                float hpRatio = king.Hp / king.HpMax;
                king.HpMax.AddAdder(hpGain);
                king.ApplyHeal(hpRatio * hpGain, null);
            }
        }

        public void UpgradeShockwave()
        {
            if(resources.TryPayingWood(shockwaveCost))
            {
                Units.KingBehaviour.Current.ShockwaveDamage += shockwaveDamageGain;
                Units.KingBehaviour.Current.ShockwaveRange += shockwaveRangeGain;
            }
        }

        public void UpgradeStomp()
        {
            if(resources.TryPayingWood(stompCost))
            {
                Units.KingBehaviour.Current.StompDamage += stompDamageGain;
                Units.KingBehaviour.Current.StompDuration += stompDurationGain;
            }
        }

        public void UpgradeImmolation()
        {
            if(resources.TryPayingWood(immolationCost))
            {
                Units.KingBehaviour.Current.GetComponent<Units.Spells.Buffs.ImmolationTickBuff>().DamagePerSecond += immolationDpsGain;
                Units.KingBehaviour.Current.GetComponent<Units.Spells.Buffs.ImmolationTickBuff>().Radius += immolationRadiusGain;
            }
        }

        public void UpgradeThorns()
        {
            if(resources.TryPayingWood(thornsCost))
            {
                Units.KingBehaviour.Current.GetComponent<Units.Spells.Passives.ThornsPassive>().ReturnedDamage = thornsDamageGain;
            }
        }

        public void ToggleMe()
        {
            GameObject tooltipbar = GameObject.Find("GameInterface").transform.Find("TooltipBar(Panel)").gameObject;
            GameObject kingpanel = tooltipbar.transform.FindChild("King Panel").gameObject;
            GameObject kingmenubar = GameObject.Find("GameInterface").transform.Find("KingMenuBar(Panel)").gameObject;

            if(tooltipbar.activeSelf && kingpanel.activeSelf && kingmenubar.activeSelf)
            {
                tooltipbar.GetComponent<TooltipBar.TooltipBarBehaviour>().SetPanel(TooltipBar.TooltipBarPanel.Hide);
                kingmenubar.SetActive(false);
            }
            else if(!kingpanel.activeSelf && kingmenubar.activeSelf)
            {
                tooltipbar.GetComponent<TooltipBar.TooltipBarBehaviour>().SetPanel(TooltipBar.TooltipBarPanel.KingPanel);
            }
            else
            {
                tooltipbar.GetComponent<TooltipBar.TooltipBarBehaviour>().SetPanel(TooltipBar.TooltipBarPanel.KingPanel);
                kingmenubar.SetActive(true);
            }
        }

    }
}