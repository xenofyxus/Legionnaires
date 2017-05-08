using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface.BottomRowBar.KingUpgrades
{
    public class KingUpgradesBehaviour : MonoBehaviour
    {
		public GameObject shockwavePrefab;
		public GameObject stompPrefab;
		private Game.Interface.Infobar.Resources.ResourcesBehaviour resBeh;

		void Awake()
		{
			resBeh = Game.Interface.Infobar.Resources.ResourcesBehaviour.Current;
		}

		[SerializeField]
		private int hpMaxPrice = 10;

		public void UpgradeHpMax(int health)
        {
			if (resBeh.Gold >= hpMaxPrice) 
			{
				Units.KingBehaviour.Current.HpMax.AddAdder (health);
				resBeh.Gold = resBeh.Gold - hpMaxPrice;
			}
        }

	

		[SerializeField]
		private int damagePrice = 10;

		public void UpgradeDamage(int damage)
        {
			if (resBeh.Gold >= damagePrice) 
			{
				Units.KingBehaviour.Current.DamageMax.AddAdder(damage);
            	Units.KingBehaviour.Current.DamageMin.AddAdder(damage);
				resBeh.Gold = resBeh.Gold - damagePrice;
			}
        }



		[SerializeField]
		private int hpRegPrice = 10;

		public void UpgradeHpReg(int regen)
        {
			if (resBeh.Gold >= hpRegPrice) 
			{
           		Units.KingBehaviour.Current.HpReg.AddAdder(regen);
				resBeh.Gold = resBeh.Gold - hpRegPrice;
			}
        }



		[SerializeField]
		private int shockwavePrice = 10;

		public void UpgradeShockwave(int damage)
		{
			if (resBeh.Gold >= shockwavePrice) 
			{
				Game.Units.Spells.Kingspells.ShockwaveBehaviour shockwave = shockwavePrefab.GetComponent<Game.Units.Spells.Kingspells.ShockwaveBehaviour> ();
				shockwave.damage = shockwave.damage + damage;
				shockwave.range = shockwave.range + ShockRange;
				GameObject.Find ("King Panel").GetComponent<Game.Interface.TooltipBar.KingPanel.KingPanelBehaviour> ().ShockwaveUpdate (shockwave);
				resBeh.Gold = resBeh.Gold - shockwavePrice;
			}
		}



		[SerializeField]
		private int stompPrice = 10;

		public void UpgradeStomp(int damage)
		{
			if (resBeh.Gold >= stompPrice) 
			{
				Game.Units.Spells.Kingspells.StompBehaviour stomp = stompPrefab.GetComponent<Game.Units.Spells.Kingspells.StompBehaviour> ();

				stomp.damage = stomp.damage + damage;

				if(stomp.duration != 5.0f)
					stomp.duration = stomp.duration + StompRadius;

				GameObject.Find ("King Panel").GetComponent<Game.Interface.TooltipBar.KingPanel.KingPanelBehaviour> ().StompUpdate (stomp);
				resBeh.Gold = resBeh.Gold - stompPrice;
			}
		}



		[SerializeField]
		private int immolationPrice = 10;

		public void UpgradeImmolation(int damage)
		{
			if (resBeh.Gold >= immolationPrice) 
			{
				Game.Units.Spells.Buffs.ImmolationTickBuff immolation = GameObject.Find("King").GetComponent<Game.Units.Spells.Buffs.ImmolationTickBuff> ();

				immolation.DamagePerSecond = immolation.DamagePerSecond + damage;
				immolation.Radius = immolation.Radius + immoRadius;

				GameObject.Find ("King Panel").GetComponent<Game.Interface.TooltipBar.KingPanel.KingPanelBehaviour> ().ImmolationUpdate ();
				resBeh.Gold = resBeh.Gold - immolationPrice;
			}
		}



		[SerializeField]
		private int thornsPrice = 10;

		public void UpgradeThorns(int damage)
		{
			if (resBeh.Gold >= thornsPrice) 
			{
				Game.Units.Spells.WhenHits.ThornsWhenHit thorns = GameObject.Find ("King").GetComponent<Game.Units.Spells.WhenHits.ThornsWhenHit> ();

				thorns.ReturnedDamage = thorns.ReturnedDamage + damage;

				GameObject.Find ("King Panel").GetComponent<Game.Interface.TooltipBar.KingPanel.KingPanelBehaviour> ().ThornsUpdate ();
				resBeh.Gold = resBeh.Gold - thornsPrice;
			}
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
				return stompRadius;
			}
			set 
			{
				stompRadius = value;
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