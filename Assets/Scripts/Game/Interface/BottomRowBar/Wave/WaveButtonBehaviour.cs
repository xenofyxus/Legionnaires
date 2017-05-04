using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface.BottomRowBar.Wave
{
	public class WaveButtonBehaviour : MonoBehaviour
	{
		public GameObject playerReady;

		void Start ()
		{
		}

		void Update ()
		{

		}

		public void BtnPressed ()
		{
            playerReady.GetComponent<Spawners. MinionSpawnerBehaviour> ().PlayerReady ();
		}
	}
}
