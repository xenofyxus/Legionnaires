using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface.TooltipBar
{
	public class TooltipBarBehaviour : MonoBehaviour
	{
		private static TooltipBarBehaviour current = null;

		public static TooltipBarBehaviour Current {
			get {
				if (current == null)
					current = GameObject.Find("GameInterface").transform.Find("TooltipBar(Panel)").GetComponent<TooltipBarBehaviour>();
				return current;
			}
		}

		public bool Enabled {
			get {
				return Settings.Current.TooltipBarEnabled;
			}
			set {
				Settings.Current.TooltipBarEnabled = value;
				if (!value)
					SetPanel(TooltipBarPanel.Hide);
			}
		}

		private GameObject towerPanel = null;

		public GameObject TowerPanel {
			get {
				if (towerPanel == null)
					towerPanel = transform.FindChild("Tower Panel").gameObject;
				return towerPanel;
			}
		}

		private GameObject kingPanel = null;

		public GameObject KingPanel {
			get {
				if (kingPanel == null)
					kingPanel = transform.FindChild("King Panel").gameObject;
				return kingPanel;
			}
		}

		private GameObject waveInfoPanel = null;

		public GameObject WaveInfoPanel {
			get {
				if (waveInfoPanel == null)
					waveInfoPanel = transform.Find("WaveInfo Panel").gameObject;
				return waveInfoPanel;
			}
		}

		public void SetPanel(TooltipBarPanel panel)
		{
			if (Enabled)
			{
				switch (panel)
				{
				case TooltipBarPanel.TowerPanel:
					gameObject.SetActive(true);
					TowerPanel.SetActive(true);
					KingPanel.SetActive(false);
					WaveInfoPanel.SetActive(false);
					break;

				case TooltipBarPanel.KingPanel:
					gameObject.SetActive(true);
					TowerPanel.SetActive(false);
					KingPanel.SetActive(true);
					WaveInfoPanel.SetActive(false);
					break;
				case TooltipBarPanel.WaveInfo:
					gameObject.SetActive(true);
					TowerPanel.SetActive(false);
					KingPanel.SetActive(false);
					WaveInfoPanel.SetActive(true);
					break;
				default:
					gameObject.SetActive(false);
					break;
				}
			}
		}

		public void SetPanel(string panelName)
		{
			SetPanel((TooltipBarPanel)System.Enum.Parse(typeof(TooltipBarPanel), panelName, true));
		}
            
	}

	public enum TooltipBarPanel
	{
		Hide,
		TowerPanel,
		KingPanel,
		WaveInfo
	}
}