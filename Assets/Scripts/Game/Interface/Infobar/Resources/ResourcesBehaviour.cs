﻿using System.Collections;
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
                    current = GameObject.Find("GameInterface/Infobar(Panel)/Resources(Panel)").GetComponent<ResourcesBehaviour>();
                return current;
            }
        }

        [SerializeField]
        private int gold = 50;

        public int Gold
        {
            get{ return gold; }
            set{ gold = value; }
        }

        [SerializeField]
        private int goldIncome = 100;

        public int GoldIncome
        {
            get{ return goldIncome; }
            set{ goldIncome = value; }
        }

        [SerializeField]
        private int wood = 100;

        public int Wood
        {
            get { return wood; }
            set{ wood = value; }
        }

        [SerializeField]
        private int woodIncome = 2;

        public int WoodIncome
        {
            get { return woodIncome; }
            set{ woodIncome = value; }
        }

        [SerializeField]
        private float woodIncomeDelay = 5f;
        private float woodIncomeTimer = 0f;

        private int supply = 0;

        public int Supply
        {
            get{ return supply; }
            set{ supply = value; }
        }

        [SerializeField]
        private int supplyMax = 10;

        public int SupplyMax
        {
            get{ return supplyMax; }
            set{ supplyMax = value; }
        }

        private Text goldText = null;
        private Text goldIncomeText = null;
        private Text woodText = null;
        private Text woodIncomeText = null;
        private Text supplyText = null;

        void Update()
        {
            woodIncomeTimer += Time.deltaTime;
            if(woodIncomeTimer >= woodIncomeDelay)
            {
                woodIncomeTimer = 0;
                Wood += WoodIncome;
            }
            UpdateInfo();
        }

        void UpdateInfo()
        {
            if(goldText == null)
                goldText = transform.Find("Gold/Gold(Text)").GetComponent<Text>();
            goldText.text = gold.ToString();

            if(goldIncomeText == null)
                goldIncomeText = transform.Find("GoldIncome/GoldIncome(Text)").GetComponent<Text>();
            goldIncomeText.text = goldIncome + "/w";

            if(woodText == null)
                woodText = transform.Find("Wood/Wood(Text)").GetComponent<Text>();
            woodText.text = wood.ToString();

            if(woodIncomeText == null)
                woodIncomeText = transform.Find("WoodIncome/Income(Text)").GetComponent<Text>();
            woodIncomeText.text = woodIncome.ToString();

            if(supplyText == null)
                supplyText = transform.Find("Supply/Supply(Text)").GetComponent<Text>();
            supplyText.text = supply + "/" + supplyMax;
        }

        public bool TryPayingGold(int gold)
        {
            if(gold > this.gold)
                return false;
            this.gold -= gold;
            return true;
        }

        public bool TryPayingWood(int wood)
        {
            if(wood > this.wood)
                return false;
            this.gold -= wood;
            return true;
        }

        public void ApplyGoldIncome()
        {
            gold += goldIncome;
        }
    }
}