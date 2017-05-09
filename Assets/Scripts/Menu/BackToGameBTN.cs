using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToGameBTN : MonoBehaviour {
	GameObject gridFather;

	public void onClick () {
		gridFather = GameObject.Find ("GameInterface").gameObject.transform.FindChild ("GridFather").gameObject as GameObject;
		gridFather.SetActive(true);
	}

}
