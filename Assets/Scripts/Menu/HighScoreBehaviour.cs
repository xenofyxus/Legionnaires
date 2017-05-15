using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreBehaviour : MonoBehaviour {

	public static int[] highScore = new int[10];

	public static void UpdateHighScore(int score){
		string high;

		for (int i = 0; i < highScore.Length; i++) {
			high = string.Format ("HighScore" + (i + 1));
			highScore [i] = PlayerPrefs.GetInt (high, 0);
			if (highScore [i] < score) {
				PlayerPrefs.SetInt (high, score);
				PlayerPrefs.Save ();
				score = highScore [i];
			}
		}
	}

	public void UpdateLeaderboard(){
		Text setScore;
		string setScores;
		string high;
		for (int i = 0; i < highScore.Length; i++) {
			high = string.Format ("HighScore" + (i + 1));
			setScores = string.Format ("StartMenu/HighScoresPanel/Panel/" + high);
			setScore = GameObject.Find (string.Format("StartMenu/HighScoresPanel/Panel/" + high)).GetComponent<Text> ();
			setScore.text = string.Format((i+1) + "." + " " + PlayerPrefs.GetInt(high, 0));
			
		}
	}
}
