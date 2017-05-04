using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Interface.TooltipBar.TowerPanel
{
    public partial class TowerPanelBehaviour : MonoBehaviour
    {
        private Image imageObject = null;

        public Sprite Sprite
        {
            get
            {
                if(imageObject == null)
                    imageObject = transform.FindChild("Image").GetComponent<Image>();
                return imageObject.sprite;
            }
            set
            {
                if(imageObject == null)
                    imageObject = transform.FindChild("Image").GetComponent<Image>();
                imageObject.sprite = value;
            }
        }

        public Quaternion SpriteRotation
        {
            get
            {
                if(imageObject == null)
                    imageObject = transform.FindChild("Image").GetComponent<Image>();
                return imageObject.transform.rotation;
            }
            set
            {
                if(imageObject == null)
                    imageObject = transform.FindChild("Image").GetComponent<Image>();
                imageObject.transform.rotation = value;
            }
        }

        private Text costObject = null;

        public int Cost
        {
            get
            {
                if(costObject == null)
                    costObject = StatsPanel.transform.FindChild("Cost").GetComponent<Text>();
                return int.Parse(costObject.text.Substring("Cost: ".Length));
            }
            set
            {
                if(costObject == null)
                    costObject = StatsPanel.transform.FindChild("Cost").GetComponent<Text>();
                costObject.text = "Cost: " + value;
            }
        }

        private Text supplyObject = null;

        public int Supply
        {
            get
            {
                if (supplyObject==null)
                    supplyObject = statsPanel.transform.FindChild("Supply").GetComponent<Text>();
                return int.Parse(supplyObject.text.Substring("Supply: ".Length));
            }
            set
            {
                if (supplyObject==null)
                    supplyObject = statsPanel.transform.FindChild("Supply").GetComponent<Text>();
                supplyObject.text = "Supply: " + value;
            }
        }

        private Text armorTypeObject = null;

        public Game.Units.ArmorType ArmorType
        {
            get
            {
                if (armorTypeObject==null)
                    armorTypeObject = statsPanel.transform.FindChild("ArmorType").GetComponent<Text>();
                return (Game.Units.ArmorType)Enum.Parse(typeof(Game.Units.ArmorType), armorTypeObject.text.Substring("ArmorType: ".Length));
            }
            set
            {
                if (armorTypeObject==null)
                    armorTypeObject = statsPanel.transform.FindChild("ArmorType").GetComponent<Text>();
                armorTypeObject.text = "ArmorType: " + Enum.GetName(typeof(Game.Units.ArmorType), value);
            }
        }

        private Text attackTypeObject = null;

        public Game.Units.AttackType AttackType
        {
            get
            {
                if (attackTypeObject==null)
                    attackTypeObject = statsPanel.transform.FindChild("AtkType").GetComponent<Text>();
                return (Game.Units.AttackType)Enum.Parse(typeof(Game.Units.AttackType), attackTypeObject.text.Substring("AtkType: ".Length));
            }
            set
            {
                if (attackTypeObject==null)
                    attackTypeObject = statsPanel.transform.FindChild("AtkType").GetComponent<Text>();
                attackTypeObject.text = "AtkType: " + Enum.GetName(typeof(Game.Units.AttackType), value);
            }
        }

        private Text healthObject = null;

        public int Health
        {
            get
            {
                if (healthObject==null)
                    healthObject = statsPanel.transform.FindChild("Health").GetComponent<Text>();
                return int.Parse(healthObject.text.Substring("Health: ".Length));
            }
            set
            {
                if (healthObject==null)
                    healthObject = statsPanel.transform.FindChild("Health").GetComponent<Text>();
                healthObject.text = "Health: " + value;
            }
        }

        private Text attackSpeedObject = null;

        public float AttackSpeed
        {
            get
            {
                if (attackSpeedObject==null)
                    attackSpeedObject = statsPanel.transform.FindChild("AtkSpeed").GetComponent<Text>();
                return float.Parse(attackSpeedObject.text.Substring("AtkSpeed: ".Length));
            }
            set
            {
                if (attackSpeedObject==null)
                    attackSpeedObject = statsPanel.transform.FindChild("AtkSpeed").GetComponent<Text>();
                attackSpeedObject.text = "AtkSpeed: " + value.ToString("0.00");
            }
        }

        private Text rangeObject = null;

        public float Range
        {
            get
            {
                if (rangeObject==null)
                    rangeObject = statsPanel.transform.FindChild("Range").GetComponent<Text>();
                return float.Parse(rangeObject.text.Substring("Range: ".Length));
            }
            set
            {
                if (rangeObject==null)
                    rangeObject = statsPanel.transform.FindChild("Range").GetComponent<Text>();
                rangeObject.text = "Range: " + value.ToString("0.0");
            }
        }

        private Text damageObject = null;

        public string DamageText
        {
            get
            {
                if (damageObject==null)
                    damageObject = statsPanel.transform.FindChild("AtkDmg").GetComponent<Text>();
                return damageObject.text.Substring("AtkDmg: ".Length);
            }
            set
            {
                if (damageObject==null)
                    damageObject = statsPanel.transform.FindChild("AtkDmg").GetComponent<Text>();
                damageObject.text = "AtkDmg: " + value;
            }
        }
    }
}

