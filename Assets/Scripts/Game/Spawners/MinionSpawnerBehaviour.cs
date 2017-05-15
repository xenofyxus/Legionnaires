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

namespace Game.Spawners
{
	public class MinionSpawnerBehaviour : MonoBehaviour
	{

		private static MinionSpawnerBehaviour current = null;

		public static MinionSpawnerBehaviour Current {
			get {
				if (current == null)
					current = GameObject.Find ("MinionSpawner").GetComponent<MinionSpawnerBehaviour> ();
				return current;
			}
		}
			
		public static int waveNumber;
		public static int waveCounter;
		//The two init's is for the static int to reset to 0 when we reset the gamescene
		private int waveNumberInit = 0;
		private int waveCounterInit = 0;

		private int waveLoop = 0;
		[SerializeField]
		[Tooltip ("How much do you want each loop of 10 to scale? 10 = 10x hp and 5x dps")]
		private float waveLoopFactor;
		[SerializeField]
		[Tooltip ("How long time until the wave starts by itself?")]
		private float waveTime;
		private float waveCountdown;
		[Tooltip ("What minion and how many of that minion to Spawn")]
		public List<Game.Spawners.WaveObject> waveObjList = new List<Game.Spawners.WaveObject> ();

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

		Image waveTimerGO;
		GameObject lastSpawned;


		void Awake ()
		{
			waveNumber = waveNumberInit;
			waveCounter = waveCounterInit;
		}

		void Start ()
		{
			waveTimerGO = GameObject.Find ("Wave timer").GetComponent <Image> ();
			waveCountdown = waveTime;
			gridBuilder = GameObject.Find ("GridBuilder");
			waveBtn = GameObject.Find ("Wave(Button)");
			kingspellsPanel = GameObject.Find ("BottomRowBar(Panel)").transform.FindChild ("KingSpells(Panel)").gameObject;
			shockwaveBtn = kingspellsPanel.transform.FindChild ("Shockwave(Button)").gameObject;
			stompBtn = kingspellsPanel.transform.FindChild ("Stomp(Button)").gameObject;
		}

		void LateUpdate ()
		{
			waveCounter = (waveLoop) * 10 + waveNumber;
			if (Game.Units.MinionBehaviour.Minions.Count == 0 && playerReady == false) {
				gridBuilder.SetActive (true);

				//Activate WaveTooltip
				if (Interface.TowerMenu.TowerMenuBehaviour.nextWaveStarted)
					Game.Interface.TooltipBar.TooltipBarBehaviour.Current.SetPanel("WaveInfo");

				Interface.TowerMenu.TowerMenuBehaviour.nextWaveStarted = false;
				gridScript.GetComponent<Interface.GridBuilder.GridBuilderBehaviour> ().ResetSprite ();
				if (reset == false) {
					waveBtn.SetActive (true);
					Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.ApplyGoldIncome ();
				}
				reset = true;
				//Show wavebutton, reset the cooldown and hide the kingspellspanel
				 
				kingspellsPanel.SetActive (false);
			}

			if (waveCountdown <= 0) {
				playerReady = true;
				waveTimerGO.fillAmount = 1;
			} else if (Game.Units.MinionBehaviour.Minions.Count == 0 && !playerReady) {
				waveTimerGO.fillAmount = waveCountdown / waveTime;
				waveCountdown -= Time.deltaTime;
			}

			if (reset && playerReady) {
				//give player wood if starting wave early.
				int woodIncrease = ((int)(waveCountdown / Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.WoodIncomeDelay) * Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.WoodIncome);
				Game.Interface.Infobar.Resources.ResourcesBehaviour.Current.Wood += woodIncrease;

				waveCountdown = waveTime;
				waveTimerGO.fillAmount = 0;
				reset = false;
				gridScript.GetComponent<Interface.GridBuilder.GridBuilderBehaviour> ().Reset ();
				newWave = true;
			}

			if (newWave && playerReady) {
				NextWave ();
				Interface.TowerMenu.TowerMenuBehaviour.nextWaveStarted = true;
			}
		}


		void NextWave ()
		{
			gridBuilder.SetActive (false);
			waveBtn.SetActive (false);
			kingspellsPanel.SetActive (true);
			for (int i = 0; i < Mathf.Ceil (((float)waveObjList [waveNumber].amountOfMinions) / 4); i++) {
				instantiateTimer -= Time.deltaTime;
				if (instantiateTimer <= 0) {
					instantiateTimer = 4f;
					for (int j = 0; j < 4; j++) {		// Spawning 4 units per "instantiateTimer"-delay
						if (numberOfUnitsSpawned >= waveObjList [waveNumber].amountOfMinions) {
							playerReady = false;
							break;
						}
						float x = Random.Range (MinX, MaxX);
						float y = Random.Range (MinY, MaxY);

						//Referens the last spawned minion and change it's value based on which loop it is

						lastSpawned = Instantiate (waveObjList [waveNumber].minion, new Vector2 (x, y), transform.rotation);
						if (waveLoop > 0) {
							lastSpawned.gameObject.GetComponent<Units.MinionBehaviour> ().DamageMax *= waveLoop * waveLoopFactor / 2.5f;
							lastSpawned.gameObject.GetComponent<Units.MinionBehaviour> ().DamageMin *= waveLoop * waveLoopFactor / 2.5f;
							lastSpawned.gameObject.GetComponent<Units.MinionBehaviour> ().Hp *= waveLoop * waveLoopFactor;
							lastSpawned.gameObject.GetComponent<Units.MinionBehaviour> ().Reward *= waveLoop * 2;

							if (lastSpawned.gameObject.GetComponent<Units.Spells.Passives.DotPassive> () != null) {
								lastSpawned.gameObject.GetComponent<Units.Spells.Passives.DotPassive> ().TotalDamage *= waveLoop * waveLoopFactor / 2;
							}
						}
						numberOfUnitsSpawned++;
					}

				}
			}
			if (numberOfUnitsSpawned >= waveObjList [waveNumber].amountOfMinions) { // When done spawning goes on to next wave.
				playerReady = false;
				instantiateTimer = 5f;
				waveNumber++;
				numberOfUnitsSpawned = 0;
				if (waveNumber == waveObjList.Count) {
					waveLoop++;
					waveNumber = 0;
				}
				newWave = false;
			}
			shockwaveBtn.GetComponent<Image> ().fillAmount = 1;
			stompBtn.GetComponent<Image> ().fillAmount = 1;
			stompBtn.GetComponent<Units.Spells.Kingspells.StompButton> ().CooldownTimer = 0f; 
			shockwaveBtn.GetComponent<Units.Spells.Kingspells.ShockwaveButton> ().CooldownTimer = 0f;

		}

		public void PlayerReady ()
		{
			playerReady = true;
		}
	}

}