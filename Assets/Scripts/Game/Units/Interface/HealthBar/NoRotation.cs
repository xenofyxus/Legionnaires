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
            rotation = transform.rotation;
        }

        void LateUpdate()
        {
            transform.rotation = rotation;
        }
    }

}