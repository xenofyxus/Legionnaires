using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Units;

namespace Game.Interface.TooltipBar.TowerPanel
{
	public partial class TowerPanelBehaviour : MonoBehaviour
	{
		private static TowerPanelBehaviour current = null;

		public static TowerPanelBehaviour Current {
			get {
				if (current == null)
					current = TooltipBarBehaviour.Current.TowerPanel.GetComponent<TowerPanelBehaviour>();
				return current;
			}
		}

		private Text nameText = null;
		private Image modelImage = null;
		private Image healthImage = null;
		private Text healthText = null;
		private Text descriptionText = null;

		private Transform legionnaireDataPanel = null;
		private Text goldCostText = null;
		private Text supplyCostText = null;

		private Transform minionDataPanel = null;
		private Text rewardText = null;

		private Transform kingDataPanel = null;

		private Text armorTypeText = null;
		private Text attackTypeText = null;
		private Text damageText = null;
		private Text attackSpeedText = null;
		private Text rangeText = null;

		private Text spell1NameText = null;
		private Text spell1DescText = null;
		private Text spell2NameText = null;
		private Text spell2DescText = null;

		private bool objectsSet = false;

		private Units.UnitBehaviour unit = null;

		Units.Spells.Spell spell1;
		Units.Spells.Spell spell2;

		void Update()
		{
			if (unit == null)
			{
				TooltipBarBehaviour.Current.SetPanel("Hide");
			}
			else
			{
				UpdateInfo();
			}
		}

		public void SetUnit(UnitBehaviour unit)
		{
			this.unit = unit;

			spell1 = null;
			spell2 = null;
			foreach (Units.Spells.Spell spell in unit.GetComponents<Units.Spells.Spell>())
			{
				if (spell.Description != "")
				{
					if (spell1 == null)
					{
						spell1 = spell;
					}
					else if (spell2 == null)
					{
						spell2 = spell;
					}
					else
					{
						break;
					}
				}
			}
			TooltipBarBehaviour.Current.SetPanel("TowerPanel");
			UpdateInfo();
		}

		public void UpdateInfo()
		{
			if (!objectsSet)
				GetObjects();

			nameText.text = unit.name.Replace("(Clone)", "");

			modelImage.sprite = unit.GetComponent<SpriteRenderer>().sprite;
			modelImage.transform.rotation = unit.transform.rotation;

			// HpMax is null for prefabs cus they are set in Start
			healthImage.fillAmount = unit.Hp / (unit.HpMax != null ? unit.HpMax : 1);
			healthText.text = unit.Hp.ToString("####") + ((unit.HpMax == null || unit.HpMax == 0f) ? "" : "/" + ((float)unit.HpMax).ToString("####"));

			descriptionText.text = unit.Description;

			if (unit.GetType() == typeof(LegionnaireBehaviour))
			{
				LegionnaireBehaviour legionnaire = unit as LegionnaireBehaviour;

				legionnaireDataPanel.gameObject.SetActive(true);
				minionDataPanel.gameObject.SetActive(false);
				kingDataPanel.gameObject.SetActive(false);

				goldCostText.text = legionnaire.Cost.ToString();
				supplyCostText.text = legionnaire.Supply.ToString();
			}
			else if (unit.GetType() == typeof(MinionBehaviour))
			{
				MinionBehaviour minion = unit as MinionBehaviour;

				legionnaireDataPanel.gameObject.SetActive(false);
				minionDataPanel.gameObject.SetActive(true);
				kingDataPanel.gameObject.SetActive(false);

				rewardText.text = minion.Value.ToString();
			}
			else if (unit.GetType() == typeof(KingBehaviour))
			{
				KingBehaviour king = unit as KingBehaviour;
				legionnaireDataPanel.gameObject.SetActive(false);
				minionDataPanel.gameObject.SetActive(false);
				kingDataPanel.gameObject.SetActive(true);
			}

			armorTypeText.text = System.Enum.GetName(typeof(Units.ArmorType), unit.ArmorType);
			attackTypeText.text = System.Enum.GetName(typeof(Units.AttackType), unit.AttackType);
			damageText.text = ((float)unit.DamageMin).ToString("####") + "-" + ((float)unit.DamageMax).ToString("####");
			attackSpeedText.text = ((float)unit.AttackSpeed).ToString("0.0");
			rangeText.text = ((float)unit.Range).ToString("0.0");

			if (spell1 != null)
			{
				spell1NameText.text = spell1.SpellName;
				spell1DescText.text = spell1.Description;
			}
			else
			{
				spell1NameText.text = "No Spell";
				spell1DescText.text = "";
			}

			if (spell2 != null)
			{
				spell2NameText.text = spell2.SpellName;
				spell2DescText.text = spell2.Description;
			}
			else
			{
				spell2NameText.text = "No Spell";
				spell2DescText.text = "";
			}
		}

		private void GetObjects()
		{
			nameText = transform.Find("Name").GetComponent<Text>();
			modelImage = transform.Find("Image").GetComponent<Image>();
			healthImage = transform.Find("Health/ForeGround").GetComponent<Image>();
			healthText = transform.Find("Health/Value").GetComponent<Text>();

			descriptionText = transform.Find("Stats/Description/Value").GetComponent<Text>();

			legionnaireDataPanel = transform.Find("Stats/Legionnaire");
			goldCostText = legionnaireDataPanel.transform.Find("Cost/Gold/Value").GetComponent<Text>();
			supplyCostText = legionnaireDataPanel.transform.Find("Cost/Supply/Value").GetComponent<Text>();

			minionDataPanel = transform.Find("Stats/Minion");
			rewardText = minionDataPanel.transform.Find("Reward/Gold/Value").GetComponent<Text>();

			kingDataPanel = transform.Find("Stats/King");

			armorTypeText = transform.Find("Stats/ArmorType/Value").GetComponent<Text>();
			attackTypeText = transform.Find("Stats/AttackType/Value").GetComponent<Text>();
			damageText = transform.Find("Stats/Damage/Value").GetComponent<Text>();
			attackSpeedText = transform.Find("Stats/AttackSpeed/Value").GetComponent<Text>();
			rangeText = transform.Find("Stats/Range/Value").GetComponent<Text>();

			spell1NameText = transform.Find("Spells/Spell1/Name").GetComponent<Text>();
			spell1DescText = transform.Find("Spells/Spell1/Description").GetComponent<Text>();
			spell2NameText = transform.Find("Spells/Spell2/Name").GetComponent<Text>();
			spell2DescText = transform.Find("Spells/Spell2/Description").GetComponent<Text>();

			objectsSet = true;
		}
	}
}