using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameInterface
{
	public class WaveButton : MonoBehaviour
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
			playerReady.GetComponent<MinionSpawner> ().PlayerReady ();
		}
	}
}
