using UnityEngine;
using UnityEngine.UI;
using System;

namespace Game.Units.Spells.Kingspells
{

    public class ShockwaveBehaviour : MonoBehaviour
    {

        GameObject ShockwaveCopy;
        Vector2 kingPosition = new Vector2(0.17f, -14.49f);
        Vector2 direction;
        Vector2 mousePosition;

        //When a copy spawns get the mouseposition of the last click to get direction and assign ShockwaveCopy to that object.
        void Awake()
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = mousePosition + (100 * (mousePosition - kingPosition));
            ShockwaveCopy = this.gameObject;
        }


        //Destroy the shockwave travelled outside of range from the king else move towards the mouseclick
        void FixedUpdate()
        {
            if(ShockwaveCopy != null)
            {
                if(Vector2.Distance(kingPosition, ShockwaveCopy.transform.position) > KingBehaviour.Current.ShockwaveRange)
                {
                    Destroy(ShockwaveCopy);
                }
                else
                {
                    ShockwaveCopy.transform.position = Vector2.MoveTowards(ShockwaveCopy.transform.position, direction, 0.2f);
                }
            }
        }


        //Apply damage to minions when they trigger the rigidbody
        void OnTriggerEnter2D(Collider2D other)
        {
            float dummyVar;
            if(other.gameObject.GetComponent<Game.Units.MinionBehaviour>() != null)
            {
                other.GetComponent<UnitBehaviour>().ApplyDamage(KingBehaviour.Current.ShockwaveDamage, out dummyVar, null);
            }
        }

    }
}
