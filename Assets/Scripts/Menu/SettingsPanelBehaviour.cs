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

		void Start()
		{
			tooltipBarEnabledToggle = transform.Find("TooltipBarEnabled").GetComponent<Toggle>();
			tooltipBarEnabledToggle.isOn = Settings.Current.TooltipBarEnabled;

			legionnaireToggle = transform.Find ("LegionnaireToggle").GetComponent<ToggleGroup> ();
		}
	
		void Update()
		{

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
			print (Settings.Current.LegionnaireBuilder);
		}
	}
}