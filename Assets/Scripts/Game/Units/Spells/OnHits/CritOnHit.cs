using System;

namespace Game.Units.Spells.OnHits
{
    public class CritOnHit : OnHit
    {
        [UnityEngine.SerializeField]
        protected float multiplier;

        public float Multiplier
        {
            get{ return multiplier; }
            set{ multiplier = value; }
        }

		protected override void Apply(UnitStat damage, UnitBehaviour target, out PostDamageEffect postDamageEffect)
        {
            postDamageEffect = null;
            damage.AddMultiplier(multiplier);
        }
    }
}

