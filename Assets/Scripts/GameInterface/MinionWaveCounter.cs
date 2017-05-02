using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionWaveCounter : MonoBehaviour {

    private int minionCount;
    private Text myText;

	void Update()
	{
		myText = GetComponent<Text>();
		int tags = GameObject.FindGameObjectsWithTag("Minion").Length;
		if(minionCount != tags)
		{
			minionCount = tags;
			myText.text = "Remaining: " + minionCount; 
		} 
	}
}
