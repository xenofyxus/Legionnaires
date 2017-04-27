using System;
using UnityEngine;

namespace Game.Units.Buffs
{
	public abstract class Buff : MonoBehaviour
	{
		public float duration = 0;

		public UnitBehaviour owner;

		public abstract void Apply ();

		public abstract void Remove ();

		void Update ()
		{
			if (duration > 0)
				duration -= Time.deltaTime;
			else {
				Remove ();
				GameObject.Destroy (this);
			}
		}
	}
}

