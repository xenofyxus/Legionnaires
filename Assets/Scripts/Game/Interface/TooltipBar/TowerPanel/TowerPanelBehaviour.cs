using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Interface.TooltipBar.TowerPanel
{
    public partial class TowerPanelBehaviour : MonoBehaviour
    {
        private static TowerPanelBehaviour current = null;

        public static TowerPanelBehaviour Current
        {
            get
            {
                if(current == null)
                    current = TooltipBarBehaviour.Current.TowerPanel.GetComponent<TowerPanelBehaviour>();
                return current;
            }
        }

        private static GameObject statsPanel;

        public static GameObject StatsPanel
        {
            get
            {
                if(statsPanel == null)
                    statsPanel = Current.transform.FindChild("TowerText(Panel)").FindChild("TowerStats(Panel)").gameObject;
                return statsPanel;
            }
        }

        private Units.LegionnaireBehaviour unit = null;

        void Update()
        {
            if(unit == null)
            {
                TooltipBarBehaviour.Current.SetPanel("Hide");
            }
            else
            {
                UpdateInfo();
            }
        }

        public void SetUnit(Game.Units.LegionnaireBehaviour unit)
        {
            this.unit = unit;
            TooltipBarBehaviour.Current.SetPanel("TowerPanel");
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            Sprite = unit.GetComponent<SpriteRenderer>().sprite;
            SpriteRotation = unit.transform.rotation;
            Cost = unit.Cost;
            Supply = unit.Supply;
            ArmorType = unit.ArmorType;
            AttackType = unit.AttackType;
            Health = (int)unit.Hp;
            AttackSpeed = unit.AttackSpeed;
            Range = unit.Range;
            DamageText = (float)unit.DamageMin + " - " + (float)unit.DamageMax;
        }
    }
}