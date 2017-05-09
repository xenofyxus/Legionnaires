using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Interface.BottomRowBar.EconomyUpgrades
{
    public class EconomyUpgradesBehaviour : MonoBehaviour
    {
        private Game.Interface.Infobar.Resources.ResourcesBehaviour resources;

        [Header("Miners")]

        [SerializeField]
        [Tooltip("Starting price for miners")]
        private int minerWoodCost = 10;

        [SerializeField]
        [Tooltip("Increment per tier step for buying miners")]
        private int minerWoodCostIncrement = 0;

        [SerializeField]
        [Tooltip("Gold gained per buy")]
        private int goldIncomeGain = 10;

        [SerializeField]
        [Tooltip("Increment of gold gained per tier step")]
        private int goldIncomeGainIncrement = 0;

        [SerializeField]
        [Tooltip("Interval length between steps of purchase tiers")]
        private int minerStepInterval = 1;

        private int minerCurrentStep = 1;

        [Header("Wood Cutters")]

        [SerializeField]
        [Tooltip("Starting price for buying wood cutters")]
        private int woodcutterGoldCost = 10;

        [SerializeField]
        [Tooltip("Increment of price per tier step")]
        private int woodcutterGoldCostIncrement = 0;

        [SerializeField]
        [Tooltip("Wood gained per buy")]
        private int woodIncomeGain = 10;

        [SerializeField]
        [Tooltip("Increment of wood gained per tier step")]
        private int woodIncomeGainIncrement = 0;

        [SerializeField]
        [Tooltip("Interval length between steps of purchase tiers")]
        private int woodcutterStepInterval = 1;

        private int woodcutterCurrentStep = 1;

        [Header("Supplies")]

        [SerializeField]
        [Tooltip("Starting gold price for buying supplies")]
        private int supplyGoldCost = 10;
        [SerializeField]
        [Tooltip("Starting wood price for buying supplies")]
        private int supplyWoodCost = 10;

        [SerializeField]
        [Tooltip("Increment of gold price per tier step")]
        private int supplyGoldCostIncrement = 0;
        [SerializeField]
        [Tooltip("Increment of wood price per tier step")]
        private int supplyWoodCostIncrement = 0;

        [SerializeField]
        [Tooltip("Supply gained per buy")]
        private int supplyGain = 10;

        [SerializeField]
        [Tooltip("Increment of supply gained per tier step")]
        private int supplyGainIncrement = 0;

        [SerializeField]
        [Tooltip("Interval length between steps of purchase tiers")]
        private int supplyStepInterval = 1;

        private int supplyCurrentStep = 1;

        private Text minerWoodCostText = null;
        private Text goldIncomeGainText = null;

        private Text woodcutterGoldCostText = null;
        private Text woodIncomeGainText = null;

        private Text supplyGoldCostText = null;
        private Text supplyWoodCostText = null;
        private Text supplyGainText = null;

        private bool objectsSet = false;

        private void Awake()
        {
            resources = Game.Interface.Infobar.Resources.ResourcesBehaviour.Current;
        }

        private void Update()
        {
            if(!objectsSet)
                GetObjects();
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            minerWoodCostText.text = minerWoodCost.ToString();
            goldIncomeGainText.text = "+" + goldIncomeGain;

            woodcutterGoldCostText.text = woodcutterGoldCost.ToString();
            woodIncomeGainText.text = "+" + woodIncomeGain;

            supplyGoldCostText.text = supplyGoldCost.ToString();
            supplyWoodCostText.text = supplyWoodCost.ToString();
            supplyGainText.text = "+" + supplyGain;
        }

        private void GetObjects()
        {
            minerWoodCostText = transform.Find("Gold/Cost/Wood/Value").GetComponent<Text>();
            goldIncomeGainText = transform.Find("Gold/Gain/Value").GetComponent<Text>();

            woodcutterGoldCostText = transform.Find("Wood/Cost/Gold/Value").GetComponent<Text>();
            woodIncomeGainText = transform.Find("Wood/Gain/Value").GetComponent<Text>();

            supplyGoldCostText = transform.Find("Supply/Cost/Gold/Value").GetComponent<Text>();
            supplyWoodCostText = transform.Find("Supply/Cost/Wood/Value").GetComponent<Text>();
            supplyGainText = transform.Find("Supply/Gain/Value").GetComponent<Text>();
        }


        public void UpgradeGoldIncome()
        {
            if(resources.TryPayingWood(minerWoodCost))
            {
                resources.GoldIncome += goldIncomeGain;
                if(minerCurrentStep++ == minerStepInterval)
                {
                    minerCurrentStep = 1;
                    minerWoodCost += minerWoodCostIncrement;
                    goldIncomeGain += goldIncomeGainIncrement;
                }
            }
        }

        public void UpgradeWoodIncome()
        {
            if(resources.TryPayingGold(woodcutterGoldCost))
            {
                resources.WoodIncome += woodIncomeGain;
                if(woodcutterCurrentStep++ == woodcutterStepInterval)
                {
                    woodcutterCurrentStep = 1;
                    woodcutterGoldCost += woodcutterGoldCostIncrement;
                    woodIncomeGain += woodIncomeGainIncrement;
                }
            }
        }

        public void UpgradeSupply()
        {
            if(resources.TryPaying(supplyGoldCost, supplyWoodCost))
            {
                resources.SupplyMax += supplyGain;
                if(supplyCurrentStep++ == supplyStepInterval)
                {
                    supplyCurrentStep = 1;
                    supplyGoldCost += supplyGoldCostIncrement;
                    supplyWoodCost += supplyWoodCostIncrement;
                    supplyGain += supplyGainIncrement;
                }
            }
        }
    }
}