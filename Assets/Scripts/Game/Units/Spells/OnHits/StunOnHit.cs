using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Units.Spells.OnHits
{
    [System.Serializable]
    public class StunOnHit : OnHit
    {
        [UnityEngine.Range(0f, 5f)]
        public float stunDuration;
        //private int activeStun = 0;
			
        protected override float Apply(float baseDamage, UnitBehaviour target, out PostDamageEffect postDamageEffect)
        {
            Buffs.StunBuff activeBuff = target.gameObject.GetComponent<Buffs.StunBuff>();
            Buffs.StunBuff stunBuff = target.gameObject.AddComponent<Buffs.StunBuff>();
            postDamageEffect = null;

            if(activeBuff == null)
            {
                stunBuff.duration = stunDuration;
                return 0f;
            }
            else if(activeBuff.duration > stunBuff.duration)
            {
                Destroy(stunBuff);
                return 0f;
            }
            else
            {
                Destroy(activeBuff);
                stunBuff.duration = stunDuration;
                return 0f;
            }
        }
    }
}