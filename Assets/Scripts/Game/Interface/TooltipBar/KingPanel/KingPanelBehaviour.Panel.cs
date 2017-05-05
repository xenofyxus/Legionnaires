using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Interface.TooltipBar.KingPanel
{
	public partial class KingPanelBehaviour : MonoBehaviour 
	{
	
		private Text damageObject;
		private Text cooldownObject;
		private Text durationObject;
		private Text radiusObject;
	
		public float Damage
		{
			get
			{
				damageObject = statsPanel.transform.Find("Dmg").gameObject.GetComponent<Text>();
				return int.Parse(damageObject.text);
			}
			set
			{
				damageObject = statsPanel.transform.Find("Dmg").gameObject.GetComponent<Text>();
				damageObject.text = ""  + value;
			}
		}
	
		public float Duration
		{
			get
			{
				durationObject = statsPanel.transform.Find("Duration").gameObject.GetComponent<Text>();
				return int.Parse(durationObject.text);
			}
			set
			{
				durationObject = statsPanel.transform.Find("Duration").gameObject.GetComponent<Text>();
				durationObject.text = "" + value;
			}
		}

		public float Radius
		{
			get
			{
				radiusObject = statsPanel.transform.Find("Radius").gameObject.GetComponent<Text>();
				return int.Parse(radiusObject.text);
			}
			set
			{
				radiusObject = statsPanel.transform.Find("Radius").gameObject.GetComponent<Text>();
				radiusObject.text = "" + value;
			}
		}

	}
}