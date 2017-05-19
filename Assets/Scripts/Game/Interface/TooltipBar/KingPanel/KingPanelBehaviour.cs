using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Units;

namespace Game.Interface.TooltipBar.KingPanel
{
    public partial class KingPanelBehaviour : MonoBehaviour
    {
        private static KingPanelBehaviour current = null;

        public static KingPanelBehaviour Current
        {
            get
            {
                if(current == null)
                    current = GameObject.Find("GameInterface").transform.Find("TooltipBar(Panel)/King Panel").GetComponent<KingPanelBehaviour>();
                return current;
            }
        }

        private Text shockwaveDamageText = null;

        private Text stompDamageText = null;
        private Text stompDurationText = null;

        private Text immolationDpsText = null;

        private Text thornsDamageText = null;

        void Start()
        {
            GetTextObjects();
        }

        void Update()
        {
            UpdateInfo();
        }

        private void GetTextObjects()
        {
            shockwaveDamageText = transform.Find("Shockwave(Panel)/ShockwaveStats(Panel)/Dmg").GetComponent<Text>();

            stompDamageText = transform.Find("Stomp(Panel)/StompStats(Panel)/Dmg").GetComponent<Text>();
            stompDurationText = transform.Find("Stomp(Panel)/StompStats(Panel)/Duration").GetComponent<Text>();

            immolationDpsText = transform.Find("Immolation(Panel)/ImmolationStats(Panel)/Dmg").GetComponent<Text>();

            thornsDamageText = transform.Find("Thorns(Panel)/ThornsStats(Panel)/Dmg").GetComponent<Text>();
        }

        private void UpdateInfo()
        {
			if(KingBehaviour.Current.ShockwaveDamage != 10){
            shockwaveDamageText.text = KingBehaviour.Current.ShockwaveDamage.ToString("##");
			}
			if (KingBehaviour.Current.StompDamage != 5) {
				stompDamageText.text = KingBehaviour.Current.StompDamage.ToString ("##");
				stompDurationText.text = KingBehaviour.Current.StompDuration.ToString ("##");
			}

			if (KingBehaviour.Current.GetComponent<Units.Spells.Buffs.ImmolationTickBuff> ().DamagePerSecond != 0) {
				immolationDpsText.text = KingBehaviour.Current.GetComponent<Units.Spells.Buffs.ImmolationTickBuff> ().DamagePerSecond.ToString ("##") + "/sec";
			}
			if (KingBehaviour.Current.GetComponent<Units.Spells.Passives.ThornsPassive> ().ReturnedDamage != 0) {
				thornsDamageText.text = KingBehaviour.Current.GetComponent<Units.Spells.Passives.ThornsPassive> ().ReturnedDamage.ToString ("##") + "%";
			}
        }
    }
}