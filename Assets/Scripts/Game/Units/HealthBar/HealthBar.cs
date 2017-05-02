using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Interface.Units
{

	public class HealthBar : MonoBehaviour {

		Quaternion rotation;
		public Image HealthBars;
		Game.Units.UnitBehaviour unit;
	
		void Awake()
		{
			unit = transform.root.GetComponent<Game.Units.UnitBehaviour> ();
			rotation = transform.rotation;
		}
		void LateUpdate()
		{
			HealthBars.fillAmount = unit.Hp / unit.HpMax;
			transform.rotation = rotation;
		}
	}
}