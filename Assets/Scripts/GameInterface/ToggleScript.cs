using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour {

	public GameObject current;
	public Sprite toggleOn;
	public Sprite toggleOff;

	private bool state = true;

	public void toggleMe()
	{
		Image img = current.GetComponent<Image> ();
		state = !state;
		if (state) {
			img.sprite = toggleOn;
			//Set Sound On.
		} else {
			img.sprite = toggleOff;
			//Set Sound Off.
		}
	}
		
		
}
