using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interface.TooltipBar;

namespace Game.Interface.BottomRowBar.KingUpgrades
{
	public class KingUpgradesBehaviour : MonoBehaviour
	{
		private Game.Interface.Infobar.Resources.ResourcesBehaviour resources;

		[Header("Damage")]

		[SerializeField]
		private int damageCost = 10;
		[SerializeField]
		private float damageGain = 10;

		[Header("HP Reg")]

		[SerializeField]
		private int hpRegCost = 10;
		[SerializeField]
		private int hpRegGain = 10;

		[Header("HP")]

		[SerializeField]
		private int hpCost = 10;
		[SerializeField]
		private float hpGain = 10;

		[Header("Shockwave")]

		[SerializeField]
		private int shockwaveCost = 10;
		[SerializeField]
		private float shockwaveDamageGain = 10;
		[SerializeField]
		private float shockwaveRangeGain = 10;

		[Header("Stomp")]

		[SerializeField]
		private int stompCost = 10;
		[SerializeField]
		private float stompDamageGain = 10;
		[SerializeField]
		private float stompDurationGain = 10;

		[Header("Immolation")]

		[SerializeField]
		private int immolationCost = 10;
		[SerializeField]
		private float immolationDpsGain = 10;
		[SerializeField]
		private float immolationRadiusGain = 10;

		[Header("Thorns")]

		[SerializeField]
		private int thornsCost = 10;
		[SerializeField]
		private int thornsDamageGain = 10;

		private UpgradesPanelBehaviour spellsPanel = null;
		private UpgradesPanelBehaviour statsPanel = null;

		private void Awake()
		{
			spellsPanel = transform.Find("Spells").GetComponent<UpgradesPanelBehaviour>();
			statsPanel = transform.Find("Stats").GetComponent<UpgradesPanelBehaviour>();
			resources = Game.Interface.Infobar.Resources.ResourcesBehaviour.Current;
		}

		private void OnEnable()
		{
			spellsPanel.Disable();
			statsPanel.Disable();
		}

		private void OnDisable()
		{
			if (spellsPanel.gameObject.activeSelf || statsPanel.gameObject.activeSelf)
				TooltipBarBehaviour.Current.SetPanel(TooltipBarPanel.Hide);
		}

		public void OpenSpells()
		{
			spellsPanel.Enable();
			statsPanel.Disable();
			TooltipBarBehaviour.Current.SetPanel(TooltipBarPanel.KingPanel);
		}

		public void OpenStats()
		{
			spellsPanel.Disable();
			statsPanel.Enable();
			TooltipBar.TowerPanel.TowerPanelBehaviour.Current.SetUnit(Units.KingBehaviour.Current);
		}

		public void UpgradeDamage()
		{
			if (resources.TryPayingWood(damageCost))
			{
				Units.KingBehaviour.Current.DamageMax += damageGain;
				Units.KingBehaviour.Current.DamageMin += damageGain;
				GetComponent<Game.Interface.Infobar.Resources.ResourcesBehaviour> ().GoldIncome += 1;
			}
		}

		public void UpgradeHpReg()
		{
			if (resources.TryPayingWood(hpRegCost))
			{
				Units.KingBehaviour.Current.HpReg += hpRegGain;
				GetComponent<Game.Interface.Infobar.Resources.ResourcesBehaviour> ().GoldIncome += 1;
			}
		}

		public void UpgradeHpMax()
		{
			if (resources.TryPayingWood(hpCost))
			{
				Units.KingBehaviour king = Units.KingBehaviour.Current;
				float hpRatio = king.Hp / king.HpMax;
				king.HpMax.AddAdder(hpGain);
				king.ApplyHeal(hpRatio * hpGain, null);
				GetComponent<Game.Interface.Infobar.Resources.ResourcesBehaviour> ().GoldIncome += 1;
			}
		}

		public void UpgradeShockwave()
		{
			if (resources.TryPayingWood(shockwaveCost))
			{
				Units.KingBehaviour.Current.ShockwaveDamage += shockwaveDamageGain;
				Units.KingBehaviour.Current.ShockwaveRange += shockwaveRangeGain;
			}
		}

		public void UpgradeStomp()
		{
			if (resources.TryPayingWood(stompCost))
			{
				Units.KingBehaviour.Current.StompDamage += stompDamageGain;
				Units.KingBehaviour.Current.StompDuration += stompDurationGain;
			}
		}

		public void UpgradeImmolation()
		{
			if (resources.TryPayingWood(immolationCost))
			{
				Units.KingBehaviour.Current.GetComponent<Units.Spells.Buffs.ImmolationTickBuff>().DamagePerSecond += immolationDpsGain;
				Units.KingBehaviour.Current.GetComponent<Units.Spells.Buffs.ImmolationTickBuff>().Radius += immolationRadiusGain;
			}
		}

		public void UpgradeThorns()
		{
			if (resources.TryPayingWood(thornsCost))
			{
				Units.KingBehaviour.Current.GetComponent<Units.Spells.Passives.ThornsPassive>().ReturnedDamage = thornsDamageGain;
			}
		}

		public void Toggle()
		{
			gameObject.SetActive(!gameObject.activeSelf);
		}
	}
}