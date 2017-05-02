using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Units.Spells.OnHits
{
    [System.Serializable]
    public class StunOnHit : OnHit
    {
		[SerializeField]
        [UnityEngine.Range(0f, 5f)]
		protected float duration;

		public float Duration
		{
			get{ return duration; }
			set{ duration = value; }
		}
			
        protected override void Apply(UnitStat damage, UnitBehaviour target, out PostDamageEffect postDamageEffect)
        {
            Buffs.StunBuff activeBuff = target.gameObject.GetComponent<Buffs.StunBuff>();
            Buffs.StunBuff stunBuff = target.gameObject.AddComponent<Buffs.StunBuff>();

            if(activeBuff == null)
            {
                stunBuff.Duration = duration;
            }
            else if(activeBuff.Duration > stunBuff.Duration)
            {
                Destroy(stunBuff);
            }
            else
            {
                Destroy(activeBuff);
                stunBuff.Duration = duration;
            }

			postDamageEffect = null;
        }
    }
}