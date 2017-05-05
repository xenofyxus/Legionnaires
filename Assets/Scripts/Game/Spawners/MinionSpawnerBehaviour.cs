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
        public bool playerReady = false;
        private int numberOfUnitsSpawned = 0;
        private float instantiateTimer = 5f;
        //time until next wave starts.
        private GameObject gridBuilder;
        private float MinX = -3;
        private float MaxX = 3;
        private float MinY = 16;
        private float MaxY = 17;

        void Start()
        {
            gridBuilder = GameObject.Find("GridBuilder");
        }

        void Update()
        {
            waveObjects = GameObject.FindGameObjectsWithTag("Minion");
            if(waveObjects.Length == 0)
            {
				gridBuilder.SetActive(true);
				gridScript.GetComponent<Interface.GridBuilder.GridBuilderBehaviour>().ResetSprite();
                newWave = true;
            }
            if(newWave && playerReady)
            {
				gridBuilder.SetActive(true);
				gridScript.GetComponent<Interface.GridBuilder.GridBuilderBehaviour>().Reset();
                NextWave();
            }
        }


        void NextWave()
        {
            gridBuilder.SetActive(false);
            for(int i = 0; i < Mathf.Ceil(((float)waveObjList[waveNumber].amountOfMinions) / 4); i++)
            {
                instantiateTimer -= Time.deltaTime;
                if(instantiateTimer <= 0)
                {
                    instantiateTimer = 2.5f;
                    for(int j = 0; j < 4; j++)
                    {		// Spawning 4 units per "instantiateTimer"-delay
                        if(numberOfUnitsSpawned >= waveObjList[waveNumber].amountOfMinions)
                        {
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
                instantiateTimer = 5f;
                waveNumber++;
                numberOfUnitsSpawned = 0;
                if(waveNumber == waveObjList.Count)
                {
                    waveNumber = 0;
                }
                playerReady = false;
                newWave = false;
            }
        }

        public void PlayerReady()
        {
            playerReady = true;
        }
    }

}