using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Interface.Infobar.Resources
{
    public class ResourcesBehaviour : MonoBehaviour
    {
        private static ResourcesBehaviour current;

        public static ResourcesBehaviour Current
        {
            get
            {
                if(current == null)
                    current = GameObject.Find("GameInterface/Infobar(panel)/Resources(panel)").GetComponent<ResourcesBehaviour>();
                return current;
            }
        }

        private int gold = 50;
        private Text goldText = null;

        public int Gold
        {
            get
            {
                return gold;
            }
            set
            {
                if(goldText == null)
                    goldText = transform.Find("Gold/Gold(Text)").GetComponent<Text>();
                goldText.text = gold.ToString();
                gold = value;
            }
        }

        private int goldIncome = 0;
        private Text goldIncomeText = null;

        public int GoldIncome
        {
            get
            {
                return goldIncome;
            }
            set
            {
                if(goldIncomeText == null)
                    goldIncomeText = transform.Find("GoldIncome/GoldIncome(Text)").GetComponent<Text>();
                goldIncomeText.text = goldIncome + "/w";
                goldIncome = value;
            }
        }

        private int wood = 100;
        private Text woodText = null;

        public int Wood
        {
            get
            {
                return wood;
            }
            set
            {
                if(woodText == null)
                    woodText = transform.Find("Wood/Wood(Text)").GetComponent<Text>();
                woodText.text = wood.ToString();
                wood = value;
            }
        }

        private int woodIncome = 2;
        private Text woodIncomeText = null;

        public int WoodIncome
        {
            get
            {
                return woodIncome;
            }
            set
            {
                if(woodIncomeText == null)
                    woodIncomeText = transform.Find("WoodIncome/Income(Text)").GetComponent<Text>();
                woodIncomeText.text = woodIncome.ToString();
                woodIncome = value;
            }
        }

        private int supply;
        private Text supplyText = null;

        public int Supply
        {
            get
            {
                return supply;
            }
            set
            {
                if(supplyText == null)
                    supplyText = transform.Find("Supply/Supply(Text)").GetComponent<Text>();
                supplyText.text = supply + "/" + supplyMax;
                supply = value;
            }
        }

        private int supplyMax = 10;

        public int SupplyMax
        {
            get
            {
                return supplyMax;
            }
            set
            {
                if(supplyText == null)
                    supplyText = transform.Find("Supply/Supply(Text)").GetComponent<Text>();
                supplyText.text = supply + "/" + supplyMax;
                supplyMax = value;
            }
        }

        private float woodIncomeTimer = 0f;
        private const float woodIncomeDelay = 10f;

        void Update()
        {
            woodIncomeTimer += Time.deltaTime;
            if(woodIncomeTimer >= woodIncomeDelay)
            {
                woodIncomeTimer = 0;
                Wood += WoodIncome;
            }
        }

        public bool TryPayingGold(int gold)
        {
            if(gold > Gold)
                return false;
            Gold -= gold;
            return true;
        }

        public bool TryPayingWood(int wood)
        {
            if(wood > Wood)
                return false;
            Gold -= wood;
            return true;
        }

        public void ApplyGoldIncome()
        {
            Gold += GoldIncome;
        }
    }
}