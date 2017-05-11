using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class Settings
{
	public static Settings Current{ get; set; }

	private bool tooltipBarEnabled = true;

	public bool TooltipBarEnabled {
		get {
			return this.tooltipBarEnabled;
		}
		set {
			tooltipBarEnabled = value;
		}
	}
}