using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections;

[Serializable]
public class Settings
{
	public static Settings Current{ get; set; }

	private bool tooltipBarEnabled = true;

	[SerializeField]
	private LegionnaireBuilder legionnaire = LegionnaireBuilder.Human;

	public LegionnaireBuilder LegionnaireBuilder
	{
		get{ return legionnaire; }
		set{ legionnaire = value; }
	}


	public bool TooltipBarEnabled {
		get {
			return this.tooltipBarEnabled;
		}
		set {
			tooltipBarEnabled = value;
		}
	}


	private int sleepTimeout = UnityEngine.SleepTimeout.NeverSleep;

	public int SleepTimeout
	{
		get{
			return Screen.sleepTimeout;
		}
		set{
			Screen.sleepTimeout = value;
			sleepTimeout = value;
		}
	}

	public void Load()
	{
		Screen.sleepTimeout = sleepTimeout;
	}
}

public enum LegionnaireBuilder
{
	Orc,
	Human
}