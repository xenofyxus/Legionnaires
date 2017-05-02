using System;
using UnityEngine;

namespace Game.Units.Buffs
{
	public abstract class Buff : MonoBehaviour
	{
		[SerializeField]
		protected float duration = 0;

		public float Duration
		{
			get{return duration;}
			set{duration = value;}
		}

		protected object metaData;

		/// <summary>
		/// Gets or sets the meta data.
		/// </summary>
		public object MetaData
		{
			get{return metaData;}
			set{metaData = value;}
		}

		protected UnitBehaviour owner;

		protected abstract void Apply();

		protected abstract void Remove();


		void Start()
		{
			owner = GetComponent<UnitBehaviour>();
			Apply();
		}

		void Update()
		{
			if(duration > 0)
				duration -= Time.deltaTime;
			else
			{
				Destroy(this);
			}
		}

		void OnDestroy()
		{
			if (owner != null) {
				Remove ();
			}
			
		}
	}
}

