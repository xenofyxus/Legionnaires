using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

	Text scoreText;
	Text headLine;

	void Start () {
		headLine = GameObject.Find ("Description").GetComponent<Text> ();
		scoreText = transform.GetComponent<Text> ();
		scoreText.text = string.Format("YOUR SCORE:" + '\n' + PlayerPrefs.GetInt("LatestScore", 0));
	}
	void Update(){
		headLine.color = new Color(headLine.color.r, headLine.color.g, headLine.color.b, Mathf.PingPong(Time.time, 0.7f));
	}
}
