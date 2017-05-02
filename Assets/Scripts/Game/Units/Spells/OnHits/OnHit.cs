/*
 * Author: Alexander Krantz
 * Author: Anton Anderzén
 */
using UnityEngine;

namespace Game.Units.Spells.OnHits
{
    [System.Serializable]
	public abstract class OnHit : Spell
    {
		[Header("Spell data")]

		[SerializeField]
        [Range(1, 100)]
		protected int hitChance = 100;

		public int HitChance
		{
			get{ return hitChance; }
			set{ hitChance = value; }
		}

        private const int minHitChance = 1;
        private const int maxHitChance = 100;

        /// <summary>
        /// Do not set owner if you're adding this OnHit to a UnitBehaviour
        /// </summary>
        [System.NonSerialized]
		private UnitBehaviour owner;

		public UnitBehaviour Owner
		{
			get{ return owner; }
			set{ owner = value; }
		}

		public void Hit(UnitStat damage, UnitBehaviour target, out PostDamageEffect postDamageEffect)
        {
            postDamageEffect = null;
            if(Random.Range(minHitChance, maxHitChance + 1) <= hitChance)
            {
				Apply(damage, target, out postDamageEffect);
            }
        }

        /// <summary>
        /// Base method for applying on hit effects defined by derived classes.
        /// </summary>
		protected abstract void Apply(UnitStat damage, UnitBehaviour target, out PostDamageEffect postDamageEffect);

        protected virtual void Start()
        {
            if(owner == null)
                owner = GetComponent<UnitBehaviour>();
        }
    }
}

