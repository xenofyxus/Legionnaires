﻿using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Units.Spells.Auras
{
    public class DamageModifierAura:Aura
    {
        [Range(0f, 2f)]
        public float bonusMultiplier = 0f;

        private Dictionary<UnitBehaviour,OnHits.OnHit> onHits = new Dictionary<UnitBehaviour, Game.Units.Spells.OnHits.OnHit>();

        protected override void Applying(UnitBehaviour unit)
        {
            var effect = unit.gameObject.AddComponent<OnHits.CritOnHit>();
            effect.hitChance = 100;
            effect.multiplier = bonusMultiplier;
            unit.onHits.Add(effect);
            onHits.Add(unit, effect);
        }

        protected override void Removing(UnitBehaviour unit)
        {
            OnHits.OnHit onHit;
            if(onHits.TryGetValue(unit, out onHit))
            {
                unit.onHits.Remove(onHit);
                GameObject.Destroy(onHit);
            }
        }
    }
}

