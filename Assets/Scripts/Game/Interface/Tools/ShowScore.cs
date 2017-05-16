using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

	Text scoreText;
	Text headLine;
	Text messageText;
	string message = "You let the king down";

	void Start () {
		headLine = GameObject.Find ("Description").GetComponent<Text> ();
		scoreText = transform.GetComponent<Text> ();
		messageText = GameObject.Find ("Message").GetComponent<Text> ();

		if(PlayerPrefs.GetInt("LatestScore", 0) > 2000){
			message = "Not too shabby";
			if(PlayerPrefs.GetInt("LatestScore", 0) > 5000){
				message = "Well done";
				if(PlayerPrefs.GetInt("LatestScore", 0) > 10000){
					message = "You play too much";
					if(PlayerPrefs.GetInt("LatestScore", 0) > 15000){
						message = "Are you cheating?";
						if(PlayerPrefs.GetInt("LatestScore", 0) > 25000){
							message = "Got nothing more to say";
						}
					}
				}
			}

		}
		messageText.text = message;
		scoreText.text = string.Format("YOUR SCORE:" + '\n' + PlayerPrefs.GetInt("LatestScore", 0));
	}
	void Update(){
		headLine.color = new Color(headLine.color.r, headLine.color.g, headLine.color.b, Mathf.PingPong(Time.time, 0.7f));
	}
}
