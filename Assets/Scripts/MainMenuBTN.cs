//Author: Victor och Daniel
//Date: 20/04/2017
//Edited by:
//Edit date:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBTN : MonoBehaviour {

	public void StartLevel() {
		SceneManager.LoadScene (1); //This decides which Scene will load when the play button is pressed.
	}

}
