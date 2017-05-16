﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Interface.TooltipBar.WaveInfo
{
    public class WaveInfoBehaviour : MonoBehaviour
    {
        private GameObject nextMinion;

        private int lastWaveNumber = -1;

        private Transform valuesPanel = null;

        private Image currentImage = null;
        private Text armorTypeText = null;
        private Text attackTypeText = null;
        private Text healthText = null;
        //private Text recommendedValueText = null;
        private Text minionCountText = null;
        private Text minionNameText = null;

        void Update()
        {
			if(Game.Spawners.MinionSpawnerBehaviour.waveNumber != lastWaveNumber && GameObject.Find ("GameInterface").transform.FindChild ("BottomRowBar(Panel)/Wave(Button)").gameObject.activeSelf)
            {
                nextMinion = Game.Spawners.MinionSpawnerBehaviour.Current.waveObjList[Game.Spawners.MinionSpawnerBehaviour.waveNumber].minion;
				//CurrentMinionStats ();
				//if(Game.Spawners.MinionSpawnerBehaviour.Current.waveLoop > 0)
					//nextMinion.GetComponent<Game.Units.UnitBehaviour>().Hp *= Game.Spawners.MinionSpawnerBehaviour.Current.waveLoop * Game.Spawners.MinionSpawnerBehaviour.Current.waveLoopFactor;
				
                lastWaveNumber = Game.Spawners.MinionSpawnerBehaviour.waveNumber;
                SetInfo();
            }
        }

		private void CurrentMinionStats(){
			if(Game.Spawners.MinionSpawnerBehaviour.Current.waveLoop > 1)
				nextMinion.GetComponent<Game.Units.UnitBehaviour>().Hp *= Game.Spawners.MinionSpawnerBehaviour.Current.waveLoop * Game.Spawners.MinionSpawnerBehaviour.Current.waveLoopFactor;
		}

        public void SetInfo()
        {
            if(valuesPanel == null)
                valuesPanel = transform.Find("WaveInfo");

            if(currentImage == null)
                currentImage = transform.Find("MinionSprite").GetComponent<Image>();
            currentImage.sprite = nextMinion.GetComponent<SpriteRenderer>().sprite;

            if(armorTypeText == null)
                armorTypeText = valuesPanel.Find("Armor/ArmorType").GetComponent<Text>();
            armorTypeText.text = System.Enum.GetName(typeof(Game.Units.ArmorType), nextMinion.GetComponent<Game.Units.UnitBehaviour>().ArmorType);
	    
            if(attackTypeText == null)
                attackTypeText = valuesPanel.Find("Attack/AttackType").GetComponent<Text>();
            attackTypeText.text = System.Enum.GetName(typeof(Game.Units.AttackType), nextMinion.GetComponent<Game.Units.UnitBehaviour>().AttackType);
	
            if(healthText == null)
                healthText = valuesPanel.Find("Health/HealthValue").GetComponent<Text>();
			if (Game.Spawners.MinionSpawnerBehaviour.Current.waveLoop > 0) {
				healthText.text = ((int)nextMinion.GetComponent<Game.Units.UnitBehaviour> ().Hp * Game.Spawners.MinionSpawnerBehaviour.Current.waveLoop * Game.Spawners.MinionSpawnerBehaviour.Current.waveLoopFactor).ToString ();
			} else {
				healthText.text = ((int)nextMinion.GetComponent<Game.Units.UnitBehaviour> ().Hp).ToString ();
			}

            if(minionCountText == null)
                minionCountText = valuesPanel.Find("Count/CountValue").GetComponent<Text>();
            minionCountText.text = Game.Spawners.MinionSpawnerBehaviour.Current.waveObjList[Game.Spawners.MinionSpawnerBehaviour.waveNumber].amountOfMinions.ToString();
	
            if(minionNameText == null)
                minionNameText = transform.Find("MinionName").GetComponent<Text>();
            minionNameText.text = nextMinion.name;

        }
    }
}
