using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface.BottomRowBar
{
    public class BottomRowBarBehaviour : MonoBehaviour
    {
        [SerializeField]
        private GameObject kingUpgrades = null;

        public void ToggleKingMenu()
        {
            TooltipBar.TooltipBarBehaviour tooltipBar = TooltipBar.TooltipBarBehaviour.Current;

            if(kingUpgrades.activeSelf)
            {
                kingUpgrades.SetActive(false);

                if(tooltipBar.transform.FindChild("King Panel").gameObject.activeSelf)
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