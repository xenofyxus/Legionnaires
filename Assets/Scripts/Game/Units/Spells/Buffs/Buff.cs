using System;
using UnityEngine;

namespace Game.Units.Spells.Buffs
{
    public abstract class Buff : Spell
	{
        [Header("Spell data")]

		[SerializeField]
		protected float duration = 0;

		public float Duration
		{
			get{return duration;}
			set{duration = value;}
		}

		protected abstract void Apply();

		protected abstract void Remove();

		protected override void Start()
		{
			base.Start();
			Apply();
		}

		protected override void Update()
		{
			base.Update();
			if(duration > 0)
				duration -= Time.deltaTime;
			else
			{
				Destroy(this);
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (owner != null) {
				Remove ();
			}
			
		}
	}
}

