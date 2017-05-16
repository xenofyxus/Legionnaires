using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Menu
{
	public class SettingsPanelBehaviour : MonoBehaviour
	{
		Toggle tooltipBarEnabledToggle = null;
		ToggleGroup legionnaireToggle = null;
		Toggle audioToggle = null;
		void Start()
		{
			tooltipBarEnabledToggle = transform.Find("TooltipBarEnabled").GetComponent<Toggle>();
			tooltipBarEnabledToggle.isOn = Settings.Current.TooltipBarEnabled;
			audioToggle = transform.Find ("AudioSettings").GetComponent<Toggle> ();
			audioToggle.isOn = Settings.Current.AudioEnabled;
			legionnaireToggle = transform.Find ("LegionnaireToggle").GetComponent<ToggleGroup> ();
		}
	
		void Update()
		{

		}

		public void ToggleAudio(){
			
			Settings.Current.AudioEnabled = audioToggle.isOn;
		}

		public void ToggleTooltipBarEnabled()
		{
			Settings.Current.TooltipBarEnabled = tooltipBarEnabledToggle.isOn;
		}
		public void ToggleLegionnaire()
		{
			if (legionnaireToggle.ActiveToggles ().First ().gameObject.name == "OrcEnabled") {
				Settings.Current.LegionnaireBuilder = LegionnaireBuilder.Orc;
			} else {
				Settings.Current.LegionnaireBuilder = LegionnaireBuilder.Human;
			}
		}
	}
}