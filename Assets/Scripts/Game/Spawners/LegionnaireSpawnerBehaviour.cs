//Legionnaires
//Author: Daniel Karlsson, Victor Carle
//Updates:
//Date: 24/4-17

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Spawners
{
    public class LegionnaireSpawnerBehaviour : MonoBehaviour
    {

        public GameObject towerToSpawn;

        public List<GameObject> towerSpawned = new List<GameObject>();
        public List<Vector3> originalPositions = new List<Vector3>();

        void Start()
        {
        }

        void Update()
        {
            //TODO Drag and drop Legionnaires

            if(Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = gameObject.transform.position.z;
                towerSpawned.Add(Instantiate(towerToSpawn, mouseWorldPos, transform.rotation));
                originalPositions.Add(mouseWorldPos);
            }

            //testing resettowerpositions 
            if(Input.GetKeyDown("r"))
            {
                Reset();
            }

            //could be used later to sell legionnaires
            if(Input.GetKeyDown("y"))
            {
                GameObject.Destroy(towerSpawned[0]);
                towerSpawned.RemoveAt(0);
            }
        }

        public void Reset()
        {
            for(int i = 0; i < originalPositions.Count; i++)
            {
                GameObject.Destroy(towerSpawned[i]);
                towerSpawned.RemoveAt(i);
                towerSpawned.Insert(i, Instantiate(towerToSpawn, originalPositions[i], transform.rotation));
            }
        }
    }
}
