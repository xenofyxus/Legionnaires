using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Units.Spells.Auras
{
    public class DamageModifierAura:Aura
    {
		[SerializeField]
        [Range(0f, 2f)]
		private float multiplier = 0f;

        public float Multiplier
        {
            get{ return multiplier; }
            set{ multiplier = value; }
        }

        private Dictionary<UnitBehaviour,OnHits.OnHit> onHits = new Dictionary<UnitBehaviour, Game.Units.Spells.OnHits.OnHit>();

        protected override void Apply(UnitBehaviour unit)
        {
            var effect = unit.gameObject.AddComponent<OnHits.CritOnHit>();
			effect.HitChance = 100;
            effect.Multiplier = multiplier;
            onHits.Add(unit, effect);
        }

        protected override void Remove(UnitBehaviour unit)
        {
            OnHits.OnHit onHit;
            if(onHits.TryGetValue(unit, out onHit))
            {
                GameObject.Destroy(onHit);
            }
        }
    }
}

