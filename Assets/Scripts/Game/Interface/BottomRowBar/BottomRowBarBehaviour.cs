using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface.BottomRowBar
{
    public class BottomRowBarBehaviour : MonoBehaviour
    {
        private GameObject kingUpgrades = null;

        public GameObject KingUpgrades
        {
            get
            {
                if(kingUpgrades == null)
                    kingUpgrades = GameObject.Find("GameInterface").transform.Find("KingUpgrades").gameObject;
                return kingUpgrades;
            }
        }

        public void ToggleKingMenu()
        {
            TooltipBar.TooltipBarBehaviour tooltipBar = TooltipBar.TooltipBarBehaviour.Current;

            if(kingUpgrades.activeSelf)
            {
                kingUpgrades.SetActive(false);

                if(tooltipBar.KingPanel.activeSelf)
                {
                    tooltipBar.SetPanel("Hide");
                }
            }
            else
            {
                kingUpgrades.SetActive(true);

                tooltipBar.SetPanel("KingPanel");
            }
        }
	
    }
}