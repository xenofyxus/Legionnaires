/// Legionnaires
/// <summary>
/// Minion spawner.
/// summary>

//Author: Daniel Karlsson, Victor Carle
//Date: 24/4-17
//Updates: Anton Anderzen, Victor Carle
//Added dynamic wave count and minion amount/wave.
//Date: 26/4-17

using System.Collections;
using System.Collections.Generic;
using UnityEngine;	
using UnityEngine.UI;
using UnityEditor;

namespace Game.Spawners
{
    public class MinionSpawnerBehaviour : MonoBehaviour
    {

        private static MinionSpawnerBehaviour current = null;

        public static MinionSpawnerBehaviour Current
        {
            get
            {
                if(current == null)
                    current = GameObject.Find("MinionSpawner").GetComponent<MinionSpawnerBehaviour>();
                return current;
            }
        }

        public static int waveNumber = 0;
        [Tooltip("What minion and how many of that minion to Spawn")]
        public List<Game.Spawners.WaveObject> waveObjList = new List<Game.Spawners.WaveObject>();

        public GameObject gridScript;
        private GameObject[] waveObjects;

        private bool newWave;
		private bool reset;
        public bool playerReady = false;
        private int numberOfUnitsSpawned = 0;
        private float instantiateTimer = 5f;
        //time until next wave starts.
        private GameObject gridBuilder;
        private float MinX = -3;
        private float MaxX = 3;
        private float MinY = 16;
        private float MaxY = 17;
		GameObject waveBtn;
		GameObject kingspellsPanel;
		GameObject shockwaveBtn;
		GameObject stompBtn;
		[SerializeField]
		private float waveTime;
		private float waveCountdown;
		Image waveTimerGO;

        void Start()
        {
			waveTimerGO = GameObject.Find ("Wave timer").GetComponent <Image>();
			waveCountdown = waveTime;
        	gridBuilder = GameObject.Find("GridBuilder");
			waveBtn = GameObject.Find ("Wave(Button)");
			kingspellsPanel = GameObject.Find ("BottomRowBar(Panel)").transform.FindChild ("KingSpells(Panel)").gameObject;
			shockwaveBtn = kingspellsPanel.transform.FindChild ("Shockwave(Button)").gameObject;
			stompBtn = kingspellsPanel.transform.FindChild ("Stomp(Button)").gameObject;
        }

        void LateUpdate()
        {


			if(Game.Units.MinionBehaviour.Minions.Count == 0 && playerReady == false)
            {
				gridBuilder.SetActive(true);
				gridScript.GetComponent<Interface.GridBuilder.GridBuilderBehaviour>().ResetSprite();
				if (reset == false) {
					Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.ApplyGoldIncome ();
				}
				reset = true;
				//Show wavebutton, reset the cooldown and hide the kingspellspanel
				waveBtn.SetActive (true); 
				kingspellsPanel.SetActive (false);
            }

			if (waveCountdown <= 0) {
				playerReady = true;
				waveCountdown = waveTime;
			} else if (Game.Units.MinionBehaviour.Minions.Count == 0 && !playerReady){
				waveTimerGO.fillAmount = waveCountdown / waveTime;
				waveCountdown -= Time.deltaTime;
			}

			if (reset && playerReady) {
				reset = false;
				gridScript.GetComponent<Interface.GridBuilder.GridBuilderBehaviour> ().Reset ();
				newWave = true;
			}

			if (newWave && playerReady) {
					NextWave();
			}
        }


        void NextWave()
        {
			gridBuilder.SetActive (false);
			waveBtn.SetActive (false);
			kingspellsPanel.SetActive (true);
            for(int i = 0; i < Mathf.Ceil(((float)waveObjList[waveNumber].amountOfMinions) / 4); i++)
            {
                instantiateTimer -= Time.deltaTime;
                if(instantiateTimer <= 0)
                {
                    instantiateTimer = 10f;
                    for(int j = 0; j < 4; j++)
                    {		// Spawning 4 units per "instantiateTimer"-delay
                        if(numberOfUnitsSpawned >= waveObjList[waveNumber].amountOfMinions)
                        {
							playerReady = false;
							print (playerReady);
                            break;
                        }
                        float x = Random.Range(MinX, MaxX);
                        float y = Random.Range(MinY, MaxY);
                        Instantiate(waveObjList[waveNumber].minion, new Vector2(x, y), transform.rotation);
                        numberOfUnitsSpawned++;

                    }

                }
            }
            if(numberOfUnitsSpawned >= waveObjList[waveNumber].amountOfMinions)
            { // When done spawning goes on to next wave.
				playerReady = false;
                instantiateTimer = 5f;
                waveNumber++;
                numberOfUnitsSpawned = 0;
                if(waveNumber == waveObjList.Count)
                {
                    waveNumber = 0;
                }
                newWave = false;
            }
			shockwaveBtn.GetComponent<Image>().fillAmount = 1;
			stompBtn.GetComponent<Image>().fillAmount = 1;
			stompBtn.GetComponent<Units.Spells.Kingspells.StompButton> ().CooldownTimer = 0f; 
			shockwaveBtn.GetComponent<Units.Spells.Kingspells.ShockwaveButton> ().CooldownTimer = 0f;

        }

        public void PlayerReady()
        {
            playerReady = true;
        }
    }

}