using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface.TooltipBar
{
    public class TooltipBarBehaviour : MonoBehaviour
    {
        private static TooltipBarBehaviour current = null;

        public static TooltipBarBehaviour Current
        {
            get
            {
                if(current == null)
					current = GameObject.Find("GameInterface").transform.Find ("TooltipBar(Panel)").GetComponent<TooltipBarBehaviour>();
                return current;
            }
        }

        private GameObject towerPanel = null;

        public GameObject TowerPanel
        {
            get
            {
                if(towerPanel == null)
                    towerPanel = transform.FindChild("Tower Panel").gameObject;
                return towerPanel;
            }
        }

        private GameObject economyPanel = null;

        public GameObject EconomyPanel
        {
            get
            {
                if(economyPanel == null)
                    economyPanel = transform.FindChild("Economy Panel").gameObject;
                return economyPanel;
            }
        }

        private GameObject kingPanel = null;

        public GameObject KingPanel
        {
            get
            {
                if(kingPanel == null)
                    kingPanel = transform.FindChild("King Panel").gameObject;
                return kingPanel;
            }
        }

        private GameObject waveInfoPanel = null;

        public GameObject WaveInfoPanel
        {
            get
            {
                if(waveInfoPanel == null)
                    waveInfoPanel = transform.Find("WaveInfo Panel").gameObject;
                return waveInfoPanel;
            }
        }

        public void SetPanel(string panelName)
        {
            switch((TooltipBarPanel)System.Enum.Parse(typeof(TooltipBarPanel), panelName, true))
            {
                case TooltipBarPanel.TowerPanel:
                    gameObject.SetActive(true);
                    TowerPanel.SetActive(true);
                    EconomyPanel.SetActive(false);
                    KingPanel.SetActive(false);
                    WaveInfoPanel.SetActive(false);
                    break;
                case TooltipBarPanel.EconomyPanel:
                    gameObject.SetActive(true);
                    TowerPanel.SetActive(false);
                    EconomyPanel.SetActive(true);
                    KingPanel.SetActive(false);
                    WaveInfoPanel.SetActive(false);
                    break;
                case TooltipBarPanel.KingPanel:
                    gameObject.SetActive(true);
                    TowerPanel.SetActive(false);
                    EconomyPanel.SetActive(false);
                    KingPanel.SetActive(true);
                    WaveInfoPanel.SetActive(false);
                    break;
                case TooltipBarPanel.WaveInfo:
                    gameObject.SetActive(true);
                    TowerPanel.SetActive(false);
                    EconomyPanel.SetActive(false);
                    KingPanel.SetActive(false);
                    WaveInfoPanel.SetActive(true);
                    break;
                default:
                    gameObject.SetActive(false);
                    break;
            }
        }
    }

    public enum TooltipBarPanel
    {
        Hide,
        TowerPanel,
        EconomyPanel,
        KingPanel,
        WaveInfo
    }
}