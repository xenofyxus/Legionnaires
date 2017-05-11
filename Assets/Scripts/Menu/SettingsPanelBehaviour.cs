using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
	public class SettingsPanelBehaviour : MonoBehaviour
	{
		Toggle tooltipBarEnabledToggle = null;

		// Use this for initialization
		void Start()
		{
			tooltipBarEnabledToggle = transform.Find("TooltipBarEnabled").GetComponent<Toggle>();
			tooltipBarEnabledToggle.isOn = Settings.Current.TooltipBarEnabled;
		}
	
		// Update is called once per frame
		void Update()
		{
		
		}

		public void ToggleTooltipBarEnabled()
		{
			Settings.Current.TooltipBarEnabled = tooltipBarEnabledToggle.isOn;
		}
	}
}