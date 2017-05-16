using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interface.TooltipBar;
using UnityEngine.UI;
#pragma warning disable 0219

namespace Game.Interface.BottomRowBar.KingUpgrades
{
	public class KingUpgradesBehaviour : MonoBehaviour
	{
		private Game.Interface.Infobar.Resources.ResourcesBehaviour resources;

		[Header("Damage")]

		[SerializeField]
		[Tooltip("Starting price for upgrading damgage")]
		private int damageCost = 10;

		[SerializeField]
		[Tooltip("Damage gained per upgrade")]
		private float damageGain = 10;

		[SerializeField]
		[Tooltip("Increment of price per tier step")]
		private int damageCostIncrement = 0;

		[SerializeField]
		[Tooltip("Increment of damage gained per tier step")]
		private int damageGainIncrement = 0;

		[SerializeField]
		[Tooltip("Interval length between steps of upgrade tiers")]
		private int damageStepInterval = 2;

		private int damageCurrentStep = 1;



		[Header("HP Reg")]

		[SerializeField]
		[Tooltip("Starting price for upgrading hpReg")]
		private int hpRegCost = 10;

		[SerializeField]
		[Tooltip("HpReg gained per upgrade")]
		private float hpRegGain = 10;

		[SerializeField]
		[Tooltip("Increment of price per tier step")]
		private int hpRegCostIncrement = 0;

		[SerializeField]
		[Tooltip("Increment of hpReg gained per tier step")]
		private int hpRegGainIncrement = 0;

		[SerializeField]
		[Tooltip("Interval length between steps of upgrade tiers")]
		private int hpRegStepInterval = 2;

		private int hpRegCurrentStep = 1;

		[Header("HP")]

		[SerializeField]
		[Tooltip("Starting price for upgrading Hp")]
		private int hpCost = 10;

		[SerializeField]
		[Tooltip("Hp gained per upgrade")]
		private float hpGain = 10;

		[SerializeField]
		[Tooltip("Increment of price per tier step")]
		private int hpCostIncrement = 0;

		[SerializeField]
		[Tooltip("Increment of hp gained per tier step")]
		private int hpGainIncrement = 0;

		[SerializeField]
		[Tooltip("Interval length between steps of upgrade tiers")]
		private int hpStepInterval = 2;

		private int hpCurrentStep = 1;


		[Header("Shockwave")]

		[SerializeField]
		[Tooltip("Starting price for upgrading Shockwave")]
		private int shockwaveCost = 10;

		[SerializeField]
		[Tooltip("Damage gained per upgrade")]
		private float shockwaveDamageGain = 10;

		[SerializeField]
		[Tooltip("Increment of price per tier step")]
		private int shockwaveCostIncrement = 0;

		[SerializeField]
		[Tooltip("Increment of Damage gained per tier step")]
		private int shockwaveDamageGainIncrement = 0;

		[SerializeField]
		[Tooltip("Interval length between steps of upgrade tiers")]
		private int shockwaveStepInterval = 2;

		private int shockwaveCurrentStep = 1;

		[Header("Stomp")]

		[SerializeField]
		[Tooltip("Starting price for upgrading Shockwave")]
		private int stompCost = 10;

		[SerializeField]
		[Tooltip("Damage gained per upgrade")]
		private float stompDamageGain = 10;

		[SerializeField]
		[Tooltip("Stun duration gained per upgrade")]
		private float stompDurationGain = 10;

		[SerializeField]
		[Tooltip("Increment of price per tier step")]
		private int stompCostIncrement = 0;

		[SerializeField]
		[Tooltip("Increment of Damage gained per tier step")]
		private int stompDamageGainIncrement = 0;

		[SerializeField]
		[Tooltip("Interval length between steps of upgrade tiers")]
		private int stompStepInterval = 2;

		private int stompCurrentStep = 1;


		[Header("Immolation")]

		[SerializeField]
		[Tooltip("Starting price for upgrading Shockwave")]
		private int immolationCost = 10;

		[SerializeField]
		[Tooltip("Damage gained per upgrade")]
		private float immolationDamageGain = 10;

		[SerializeField]
		[Tooltip("Increment of price per tier step")]
		private int immolationCostIncrement = 0;

		[SerializeField]
		[Tooltip("Increment of Damage gained per tier step")]
		private int immolationDamageGainIncrement = 0;

		[SerializeField]
		[Tooltip("Interval length between steps of upgrade tiers")]
		private int immolationStepInterval = 2;

		private int immolationCurrentStep = 1;

		[Header("Thorns")]

		[SerializeField]
		[Tooltip("Starting price for upgrading Thorns")]
		private int thornsCost = 10;

		[SerializeField]
		[Tooltip("Damage gained per upgrade")]
		private float thornsDamageGain = 10;

		[SerializeField]
		[Tooltip("Increment of price per tier step")]
		private int thornsCostIncrement = 0;

		[SerializeField]
		[Tooltip("Increment of Damage gained per tier step")]
		private int thornsDamageGainIncrement = 0;

		[SerializeField]
		[Tooltip("Interval length between steps of upgrade tiers")]
		private int thornsStepInterval = 2;

		private int thornsCurrentStep = 1;

		private Text immolationWoodCostText = null;
		private Text thornsWoodCostText = null;
		private Text stompWoodCostText = null;
		private Text shockwaveWoodCostText = null;
		private Text hpWoodCostText = null;
		private Text damageWoodCostText = null;
		private Text hpRegWoodCostText = null;

		private UpgradesPanelBehaviour spellsPanel = null;
		private UpgradesPanelBehaviour statsPanel = null;
		private GameObject stompButton;
		private GameObject shockwaveButton;

		private void Awake()
		{
			immolationWoodCostText = GameObject.Find("GameInterface").transform.Find("KingMenuBar(Panel)/Spells/Immolation/Cost/Wood/Value").GetComponent<Text>();
			thornsWoodCostText = GameObject.Find("GameInterface").transform.Find("KingMenuBar(Panel)/Spells/Thorns/Cost/Wood/Value").GetComponent<Text>();
			stompWoodCostText = GameObject.Find("GameInterface").transform.Find("KingMenuBar(Panel)/Spells/Stomp/Cost/Wood/Value").GetComponent<Text>();
			shockwaveWoodCostText = GameObject.Find("GameInterface").transform.Find("KingMenuBar(Panel)/Spells/Shockwave/Cost/Wood/Value").GetComponent<Text>();
			hpWoodCostText = GameObject.Find("GameInterface").transform.Find("KingMenuBar(Panel)/Stats/Hp/Cost/Wood/Value").GetComponent<Text>();
			damageWoodCostText = GameObject.Find("GameInterface").transform.Find("KingMenuBar(Panel)/Stats/Damage/Cost/Wood/Value").GetComponent<Text>();
			hpRegWoodCostText = GameObject.Find("GameInterface").transform.Find("KingMenuBar(Panel)/Stats/HpReg/Cost/Wood/Value").GetComponent<Text>();



			spellsPanel = transform.Find("Spells").GetComponent<UpgradesPanelBehaviour>();
			statsPanel = transform.Find("Stats").GetComponent<UpgradesPanelBehaviour>();
			resources = Game.Interface.Infobar.Resources.ResourcesBehaviour.Current;
			stompButton = GameObject.Find ("BottomRowBar(Panel)").transform.FindChild ("KingSpells(Panel)").FindChild ("StompBackground").gameObject;
			shockwaveButton = GameObject.Find ("BottomRowBar(Panel)").transform.FindChild ("KingSpells(Panel)").FindChild ("ShockwaveBackground").gameObject;
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
				Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.GoldIncome += 1;
				if (damageCurrentStep++ == damageStepInterval)
				{
					damageCurrentStep = 1;
					damageCost += damageCostIncrement;
					damageGain += damageGainIncrement;
				}
			}
			damageWoodCostText.text = damageCost.ToString ();
		}

		public void UpgradeHpReg()
		{
			if (resources.TryPayingWood(hpRegCost))
			{
				Units.KingBehaviour.Current.HpReg += hpRegGain;
				Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.GoldIncome += 1;
				if (hpRegCurrentStep++ == hpRegStepInterval)
				{
					hpRegCurrentStep = 1;
					hpRegCost += hpRegCostIncrement;
					hpRegGain += hpRegGainIncrement;
				}
			}
			hpRegWoodCostText.text = hpRegCost.ToString ();
		}

		public void UpgradeHpMax()
		{
			if (resources.TryPayingWood(hpCost))
			{
				Units.KingBehaviour king = Units.KingBehaviour.Current;
				float hpRatio = king.Hp / king.HpMax;
				king.HpMax.AddAdder(hpGain);
				king.ApplyHeal(hpRatio * hpGain, null);
				Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.GoldIncome += 1;
				if (hpCurrentStep++ == hpStepInterval)
				{
					hpCurrentStep = 1;
					hpCost += hpCostIncrement;
					hpGain += hpGainIncrement;
				}
			}
			hpWoodCostText.text = hpCost.ToString ();
		}

		public void UpgradeShockwave()
		{
			if (resources.TryPayingWood(shockwaveCost))
			{
				if (!shockwaveButton.activeSelf) 
				{
					shockwaveButton.SetActive (true);
				}
				Units.KingBehaviour.Current.ShockwaveDamage += shockwaveDamageGain;
				if (shockwaveCurrentStep++ == shockwaveStepInterval)
				{
					shockwaveCurrentStep = 1;
					shockwaveCost += shockwaveCostIncrement;
					shockwaveDamageGain += shockwaveDamageGainIncrement;
				}
				shockwaveWoodCostText.text = shockwaveCost.ToString ();
			}
		}

		public void UpgradeStomp()
		{
			if (resources.TryPayingWood(stompCost))
			{
				if (!stompButton.activeSelf) 
				{
					stompButton.SetActive (true);
				}
				Units.KingBehaviour.Current.StompDamage += stompDamageGain;
				if (Units.KingBehaviour.Current.StompDuration < 10) {
					Units.KingBehaviour.Current.StompDuration += stompDurationGain;
				}
				if (stompCurrentStep++ == stompStepInterval)
				{
					stompCurrentStep = 1;
					stompCost += stompCostIncrement;
					stompDamageGain += stompDamageGainIncrement;
				}
			}
			stompWoodCostText.text = stompCost.ToString ();
		}

		public void UpgradeImmolation()
		{
			if (resources.TryPayingWood(immolationCost))
			{
				Units.KingBehaviour.Current.GetComponent<Units.Spells.Buffs.ImmolationTickBuff>().DamagePerSecond += immolationDamageGain;
			}
			if (immolationCurrentStep++ == immolationStepInterval)
			{
				immolationCurrentStep = 1;
				immolationCost += immolationCostIncrement;
				immolationDamageGain += immolationDamageGainIncrement;
			}
			immolationWoodCostText.text = immolationCost.ToString ();
		}

		public void UpgradeThorns()
		{
			if (resources.TryPayingWood(thornsCost))
			{
				Units.KingBehaviour.Current.GetComponent<Units.Spells.Passives.ThornsPassive>().ReturnedDamage = thornsDamageGain;
			}
			if (thornsCurrentStep++ == thornsStepInterval)
			{
				thornsCurrentStep = 1;
				thornsCost += thornsCostIncrement;
				thornsDamageGain += thornsDamageGainIncrement;
			}
			thornsWoodCostText.text = thornsCost.ToString ();
		}

		public void Toggle()
		{
			gameObject.SetActive(!gameObject.activeSelf);
		}
	}
}