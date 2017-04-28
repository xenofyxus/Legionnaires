using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Spells.Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        [Header("Spell info")]

        public string spellName;

        [Multiline()]
        public string description;

        public Sprite icon;

        [Header("Spell data")]

        public AbilityTarget targets;

        public float cooldown;

        private float cooldownTimer = 0;

        protected UnitBehaviour owner;

        protected abstract void Apply(UnitBehaviour unit);

        void Start()
        {
            owner = GetComponent<UnitBehaviour>();
        }

        void Update()
        {
            if(cooldownTimer == 0)
            {
                cooldownTimer += Time.deltaTime;
                UnitBehaviour target = null;
                switch(targets)
                {
                    case AbilityTarget.Friendlies:
                        List<UnitBehaviour> friendlies = new List<UnitBehaviour>(owner.GetFriendlies());
                        while(target == null)
                        {
                            target = friendlies[UnityEngine.Random.Range(0, friendlies.Count)];
                            if(Vector2.Distance(owner.transform.position, target.transform.position) > owner.range)
                            {
                                target = null;
                                friendlies.Remove(target);
                                if(friendlies.Count == 0)
                                    return;
                            }
                        }
                        break;
                    case AbilityTarget.Enemies:
                        target = owner.GetTarget();
                        if(target == null)
                            return;
                        break;
                    default:
                        break;
                }
                Apply(target);
            }
            else if(cooldownTimer < cooldown)
            {
                cooldownTimer += Time.deltaTime;
            }
            else
            {
                cooldownTimer = 0;
            }
        }
    }

    public enum AbilityTarget
    {
        Friendlies,
        Enemies
    }
}

