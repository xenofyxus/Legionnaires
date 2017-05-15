using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Interface.HealthBar
{
    public class NoRotation : MonoBehaviour
    {
        Quaternion rotation;


        void Awake()
        {
			rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }

        void LateUpdate()
        {
            transform.rotation = rotation;
        }
    }

}