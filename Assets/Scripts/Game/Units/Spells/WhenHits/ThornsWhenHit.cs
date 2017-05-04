using System;

namespace Game.Units.Spells.WhenHits
{
    public class ThornsWhenHit:WhenHit
    {
        [UnityEngine.SerializeField]
        private float returnedDamage;

        public float ReturnedDamage
        {
            get{ return returnedDamage; }
            set{ returnedDamage = value; }
        }

        protected override void Apply(UnitStat damage, UnitBehaviour attacker)
        {
            if(attacker != null)
                attacker.ApplyDamage(returnedDamage);
        }
    }
}

