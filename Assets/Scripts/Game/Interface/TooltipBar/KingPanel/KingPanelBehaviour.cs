using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Interface.TooltipBar.KingPanel
{
	public partial class KingPanelBehaviour : MonoBehaviour 
	{
		private static KingPanelBehaviour current = null;
		// Shockwave and Stomp will not be part of King, implement it differently.
		private static Game.Units.Spells.Buffs.ImmolationTickBuff immolation;
		private static Game.Units.Spells.WhenHits.ThornsWhenHit thorns;

		public static KingPanelBehaviour Current
		{
			get
			{
				if (current == null)
					current = GameObject.Find ("King Panel").GetComponent<KingPanelBehaviour> ();
				return current;
			}
		}

		private static GameObject statsPanel;

		public void ShockwaveUpdate(Game.Units.Spells.Kingspells.ShockwaveBehaviour schkwave)
		{
			statsPanel = Current.transform.Find("Shockwave(Panel)/ShockwaveStats(Panel)").gameObject;

			Damage = schkwave.damage;
			Radius = schkwave.range;
		}
		public void StompUpdate(Game.Units.Spells.Kingspells.StompBehaviour stomp)
		{
			statsPanel = Current.transform.Find ("Stomp(Panel)/StompStats(Panel)").gameObject;

			Damage = stomp.damage;
			Duration = stomp.duration;
		}
		public void ImmolationUpdate()
		{
			
			statsPanel = Current.transform.Find ("Immolation(Panel)/ImmolationStats(Panel)").gameObject;
			immolation = GameObject.Find("King").GetComponent<Game.Units.Spells.Buffs.ImmolationTickBuff> ();
		
			Damage = immolation.DamagePerSecond;
			Radius = immolation.Radius;

		}
		public void ThornsUpdate()
		{

			statsPanel = Current.transform.Find ("Thorns(Panel)/ThornsStats(Panel)").gameObject;
			thorns = GameObject.Find("King").GetComponent<Game.Units.Spells.WhenHits.ThornsWhenHit> ();

			Damage = thorns.ReturnedDamage;

		}
			

	}
}