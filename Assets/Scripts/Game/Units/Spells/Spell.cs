using System;
using UnityEngine;

namespace Game.Units.Spells
{
	public abstract class Spell : MonoBehaviour
	{
		[Header("Spell info")]

		[SerializeField]
		protected string spellName = "";

		/// <summary>
		/// Gets the name of the spell.
		/// </summary>
		public string SpellName {
			get
			{
				return spellName;
			}
		}

		[SerializeField]
		[Multiline()]
		protected string description = "";

		/// <summary>
		/// Gets the spell description.
		/// </summary>
		public string Description {
			get
			{
				return description;
			}
		}

		[SerializeField]
		protected Sprite icon = null;

		/// <summary>
		/// Gets the spell icon.
		/// </summary>
		public Sprite Icon {
			get
			{
				return icon;
			}
		}
	}
}

