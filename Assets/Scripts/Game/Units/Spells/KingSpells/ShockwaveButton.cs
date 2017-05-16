using UnityEngine;
using UnityEngine.UI;
using System;

namespace Game.Units.Spells.Kingspells
{
    public class ShockwaveButton : MonoBehaviour
    {
        [SerializeField]
        private float cooldown;
        private float cooldownTimer;

        GameObject ShockwaveCopy;
        Vector2 mousePosition;
        Vector2 kingPosition = new Vector2(0.17f, -14.49f);
        bool isClicked = false;
        Image shockwaveBtn;

        //Initiate the timer to the cooldown value
        void Start()
        {
            shockwaveBtn = this.gameObject.GetComponent<Image>();
        }


        public float Cooldown
        {
            get
            {
                return this.cooldown;
            }
            set
            {
                cooldown = value;
            }
        }

        public float CooldownTimer
        {
            get
            {
                return this.cooldownTimer;
            }
            set
            {
                cooldownTimer = value;
            }
        }

        //Assign mouseposition if cooldowntimer allows us
        public void onClick()
        {
            if(cooldownTimer <= 0f)
            {
                 isClicked = true;
                shockwaveBtn.color = new Color(255, 0, 0);
            }


        }


        void Update()
        {
            //Count down the timer
            if(cooldownTimer > 0f)
            {
                cooldownTimer -= Time.deltaTime;
                shockwaveBtn.fillAmount = 1 - (cooldownTimer / cooldown);
            }


			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Checks when it's clicked a second time and if so, spawns shockwave copy
			if(isClicked && Input.GetMouseButtonDown(0)  ) 
            {
				 shockwaveBtn.color = new Color(255, 255, 255);

                ShockwaveCopy = Instantiate(Resources.Load("ShockwavePrefab"), kingPosition, Quaternion.identity) as GameObject;
                ShockwaveCopy.transform.rotation = Quaternion.FromToRotation(Vector2.up, mousePosition - (Vector2)ShockwaveCopy.transform.position);

                isClicked = false;
                cooldownTimer = cooldown;
            } 
        }
    }
}
