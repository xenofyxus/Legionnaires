using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveButton : MonoBehaviour {
	public GameObject playerReady;
	void Start () {
	}
	void Update () {

	}

	public void BtnPressed() {
		playerReady.GetComponent<MinionSpawner> ().PLayerReady ();
	}
}
