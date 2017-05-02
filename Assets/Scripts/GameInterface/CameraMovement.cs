using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	private GameObject getLegionnaires;

	void Start () {

	}
	void LateUpdate()
	{
		
		if (Game.Units.LegionnaireBehaviour.legionnaires.Count == 0 && Game.Units.MinionBehaviour.Minions.Count != 0) {

			if (Game.Units.MinionBehaviour.Minions [0].transform.position.y < Camera.main.transform.position.y && Camera.main.transform.position.y > -7.3) {
				Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3 (0, Game.Units.MinionBehaviour.Minions [0].transform.position.y, -10), 3 * Time.deltaTime);
			}
		}
	}
}
