using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Interface.Infobar.Resources.Remaining
{
    public class RemainingBehaviour : MonoBehaviour
    {

        private int minionCount;
        private Text myText;

        void Update()
        {
            myText = GetComponent<Text>();
            int tags = GameObject.FindGameObjectsWithTag("Minion").Length;
            if(minionCount != tags)
            {
                minionCount = tags;
                myText.text = "" + minionCount; 
				if (minionCount == 0) 
				{
					ResourcesBehaviour.Current.Gold += Spawners.MinionSpawnerBehaviour.waveCounter * 2 + 10;
				}
            } 

        }
    }
}