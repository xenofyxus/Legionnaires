using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Units;

namespace Game.Interface.MainCamera
{
    public class MainCameraBehaviour : MonoBehaviour
    {

        private MinionBehaviour followingUnit = null;

        void Start()
        {

        }

        void LateUpdate()
        {
		
            if(LegionnaireBehaviour.legionnaires.Count == 0 && MinionBehaviour.Minions.Count != 0)
            {
                if(followingUnit == null)
                {
                    MinionBehaviour closestMinion = MinionBehaviour.Minions[0];
                    foreach(MinionBehaviour minion in MinionBehaviour.Minions)
                    {
                        if(minion.transform.position.y - closestMinion.transform.position.y < 0)
                            closestMinion = minion;
                    }
                    followingUnit = closestMinion;
                }
                if(followingUnit.transform.position.y < Camera.main.transform.position.y && Camera.main.transform.position.y > -7.3)
                {
                    Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(0, followingUnit.transform.position.y, -10), 3 * Time.deltaTime);
                }
            }
            if(Game.Units.MinionBehaviour.Minions.Count == 0)
            {
                Camera.main.transform.position = new Vector3(0f, 8.9f, -10);
            }
        }
    }
}