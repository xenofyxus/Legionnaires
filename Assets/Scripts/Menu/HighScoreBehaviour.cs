using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreBehaviour : MonoBehaviour {

	public static int[] highScore = new int[10];

	public static void UpdateHighScore(int score){
		string high;
		string highString;
		PlayerPrefs.SetInt ("LatestScore", score);
		for (int i = 0; i < highScore.Length; i++) {
			high = string.Format ("HighScore" + (i + 1));
			highString = string.Format ("HighScore" + (i + 11));
			highScore [i] = PlayerPrefs.GetInt (high, 0);
			if (highScore [i] < score) {
				PlayerPrefs.SetInt (high, score);
				PlayerPrefs.SetString (highString, Settings.Current.LegionnaireBuilder.ToString ());
				PlayerPrefs.Save ();
				score = highScore [i];
			}
		}
	}

	public void UpdateLeaderboard(){
		Text setScore;
		string setScores;
		string high;
		string highString;
		for (int i = 0; i < highScore.Length; i++) {
			high = string.Format ("HighScore" + (i + 1));
			highString = string.Format ("HighScore" + (i + 11));
			setScore = GameObject.Find (string.Format("StartMenu/HighScoresPanel/Panel/" + high)).GetComponent<Text> ();
			setScore.text = string.Format((i+1) + "." + " " + PlayerPrefs.GetInt(high, 0) + " " + "(" + PlayerPrefs.GetString(highString, "") + ")");
			
		}
	}
}
