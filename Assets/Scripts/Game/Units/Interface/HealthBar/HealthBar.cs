using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Game.Units.Interface.HealthBar
{
    public class HealthBar : MonoBehaviour
    {
        Quaternion rotation;
        public Image HealthBars;
        Game.Units.UnitBehaviour unit;

        void Awake()
        {
            unit = transform.root.GetComponent<Game.Units.UnitBehaviour>();
			rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }

        void LateUpdate()
        {
            HealthBars.fillAmount = unit.Hp / unit.HpMax;
            transform.rotation = rotation;
        }
    }
}