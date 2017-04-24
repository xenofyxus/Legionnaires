//Author: Victor Carle och Daniel Karlsson
//Date: 20/04/2017
//
//

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
