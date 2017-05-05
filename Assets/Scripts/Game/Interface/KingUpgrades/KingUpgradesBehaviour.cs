using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface.KingUpgrades
{
    public class KingUpgradesBehaviour : MonoBehaviour
    {
		public GameObject shockwavePrefab;
		public GameObject stompPrefab;

        public void UpgradeHpMax()
        {
            Units.KingBehaviour.Current.HpMax.AddAdder(100);
        }

        public void UpgradeDamage()
        {
            Units.KingBehaviour.Current.DamageMax.AddAdder(10);
            Units.KingBehaviour.Current.DamageMin.AddAdder(10);
        }

        public void UpgradeHpReg()
        {
            Units.KingBehaviour.Current.HpReg.AddAdder(2);
        }

		public void UpgradeShockwave()
		{
			Game.Units.Spells.Kingspells.ShockwaveBehaviour shockwave = shockwavePrefab.GetComponent<Game.Units.Spells.Kingspells.ShockwaveBehaviour> ();

			shockwave.damage = shockwave.damage + 10;
			shockwave.range = shockwave.range + 0.1f;

			GameObject.Find ("King Panel").GetComponent<Game.Interface.TooltipBar.KingPanel.KingPanelBehaviour> ().ShockwaveUpdate (shockwave);
		}

		public void UpgradeStomp()
		{
			Game.Units.Spells.Kingspells.StompBehaviour stomp = stompPrefab.GetComponent<Game.Units.Spells.Kingspells.StompBehaviour> ();

			stomp.damage = stomp.damage + 10;

			if(stomp.duration != 5.0f)
				stomp.duration = stomp.duration + 0.1f;

			GameObject.Find ("King Panel").GetComponent<Game.Interface.TooltipBar.KingPanel.KingPanelBehaviour> ().StompUpdate (stomp);
		}

		public void UpgradeImmolation()
		{
			Game.Units.Spells.Buffs.ImmolationTickBuff immolation = GameObject.Find("King").GetComponent<Game.Units.Spells.Buffs.ImmolationTickBuff> ();

			immolation.DamagePerSecond = immolation.DamagePerSecond + 2;
			immolation.Radius = immolation.Radius + 0.1f;

			GameObject.Find ("King Panel").GetComponent<Game.Interface.TooltipBar.KingPanel.KingPanelBehaviour> ().ImmolationUpdate ();
		}

		public void UpgradeThorns()
		{
			Game.Units.Spells.WhenHits.ThornsWhenHit thorns = GameObject.Find ("King").GetComponent<Game.Units.Spells.WhenHits.ThornsWhenHit> ();

			thorns.ReturnedDamage = thorns.ReturnedDamage + 10;

			GameObject.Find ("King Panel").GetComponent<Game.Interface.TooltipBar.KingPanel.KingPanelBehaviour> ().ThornsUpdate ();
		}
    }
}