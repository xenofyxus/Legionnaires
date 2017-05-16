using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu{
public class ToggleAudioScript : MonoBehaviour {

	Toggle audioToggle = null;

	public void Start()
	{
			audioToggle = transform.Find ("Audio(Button)").GetComponent<Toggle> ();
			audioToggle.isOn = !Settings.Current.AudioEnabled;
	}
	public void ToggleAudio(){

		Settings.Current.AudioEnabled = !audioToggle.isOn;
	}
	}
		
}
