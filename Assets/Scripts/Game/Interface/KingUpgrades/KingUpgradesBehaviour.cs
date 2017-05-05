using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface.KingUpgrades
{
    public class KingUpgradesBehaviour : MonoBehaviour
    {
		public GameObject shockwavePrefab;
		public GameObject stompPrefab;

		public void UpgradeHpMax(int health)
        {
            Units.KingBehaviour.Current.HpMax.AddAdder(health);
        }

		public void UpgradeDamage(int damage)
        {
			Units.KingBehaviour.Current.DamageMax.AddAdder(damage);
            Units.KingBehaviour.Current.DamageMin.AddAdder(damage);
        }

		public void UpgradeHpReg(int regen)
        {
            Units.KingBehaviour.Current.HpReg.AddAdder(regen);
        }

		public void UpgradeShockwave(int damage)
		{
			Game.Units.Spells.Kingspells.ShockwaveBehaviour shockwave = shockwavePrefab.GetComponent<Game.Units.Spells.Kingspells.ShockwaveBehaviour> ();

			shockwave.damage = shockwave.damage + damage;
			shockwave.range = shockwave.range + ShockRange;

			GameObject.Find ("King Panel").GetComponent<Game.Interface.TooltipBar.KingPanel.KingPanelBehaviour> ().ShockwaveUpdate (shockwave);
		}

		public void UpgradeStomp(int damage)
		{
			Game.Units.Spells.Kingspells.StompBehaviour stomp = stompPrefab.GetComponent<Game.Units.Spells.Kingspells.StompBehaviour> ();

			stomp.damage = stomp.damage + damage;

			if(stomp.duration != 5.0f)
				stomp.duration = stomp.duration + StompRadius;

			GameObject.Find ("King Panel").GetComponent<Game.Interface.TooltipBar.KingPanel.KingPanelBehaviour> ().StompUpdate (stomp);
		}

		public void UpgradeImmolation(int damage)
		{
			Game.Units.Spells.Buffs.ImmolationTickBuff immolation = GameObject.Find("King").GetComponent<Game.Units.Spells.Buffs.ImmolationTickBuff> ();

			immolation.DamagePerSecond = immolation.DamagePerSecond + damage;
			immolation.Radius = immolation.Radius + immoRadius;

			GameObject.Find ("King Panel").GetComponent<Game.Interface.TooltipBar.KingPanel.KingPanelBehaviour> ().ImmolationUpdate ();
		}

		public void UpgradeThorns(int damage)
		{
			Game.Units.Spells.WhenHits.ThornsWhenHit thorns = GameObject.Find ("King").GetComponent<Game.Units.Spells.WhenHits.ThornsWhenHit> ();

			thorns.ReturnedDamage = thorns.ReturnedDamage + damage;

			GameObject.Find ("King Panel").GetComponent<Game.Interface.TooltipBar.KingPanel.KingPanelBehaviour> ().ThornsUpdate ();
		}

		private float immoRadius = 0.0f;

		public float ImmoRadius {
			get 
			{
				return immoRadius;
			}
			set 
			{
				immoRadius = value;
			}
		}

		private float stompRadius = 0.0f;

		public float StompRadius {
			get 
			{
				return StompRadius;
			}
			set 
			{
				StompRadius = value;
			}
		}

		private float range = 0.0f;

		public float ShockRange {
			get 
			{
				return range;
			}
			set 
			{
				range = value;
			}
		}

    }
}