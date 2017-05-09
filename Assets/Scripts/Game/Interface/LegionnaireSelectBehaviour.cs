using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface
{
    public class LegionnaireSelectBehaviour : MonoBehaviour
    {
        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                foreach(Collider2D collider in Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    Units.LegionnaireBehaviour legionnaire = collider.GetComponent<Units.LegionnaireBehaviour>();
                    if(legionnaire != null)
                    {
                        TooltipBar.TowerPanel.TowerPanelBehaviour.Current.SetUnit(legionnaire);
                        break;
                    }
                }
            }
        }
    }
}