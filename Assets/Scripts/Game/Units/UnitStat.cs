using System;
using System.Collections.Generic;

namespace Game.Units
{
    [Serializable]
    public class UnitStat
    {
        [UnityEngine.SerializeField]
        private float value;

        public float BaseValue
        {
            get{ return value; }
        }

        private float modifiedValue;
        private bool modifiedValueIsUpdated;

        private List<float> adders = new List<float>();
        private List<float> multipliers = new List<float>();

        public UnitStat(float value)
        {
            this.value = value;
        }

        /// <summary>
        /// Adds a multiplier to the stat which will be applied according to: result = value * productOfMultipliers + sumOfAdders.
        /// </summary>
        /// <param name="multiplier">The multiplier.</param>
        public void AddMultiplier(float multiplier)
        {
            multipliers.Add(multiplier);
            modifiedValueIsUpdated = false;
        }

        /// <summary>
        /// Removes a multiplier that is identical to the specified value.
        /// </summary>
        /// <returns><c>true</c>, if multiplier was removed, <c>false</c> otherwise.</returns>
        /// <param name="multiplier">The multiplier.</param>
        public bool RemoveMultiplier(float multiplier)
        {
            modifiedValueIsUpdated = false;
            return multipliers.Remove(multiplier);

        }

        /// <summary>
        /// Adds an adder to the stat which will be applied according to: result = value * productOfMultipliers + sumOfAdders.
        /// </summary>
        /// <param name="adder">The adder.</param>
        public void AddAdder(float adder)
        {
            adders.Add(adder);
            modifiedValueIsUpdated = false;
        }

        /// <summary>
        /// Removes an adder that is identical to the specified value.
        /// </summary>
        /// <returns><c>true</c>, if adder was removed, <c>false</c> otherwise.</returns>
        /// <param name="adder">The adder.</param>
        public bool RemoveAdder(float adder)
        {
            modifiedValueIsUpdated = false;
            return adders.Remove(adder);
        }

        public static implicit operator float(UnitStat unitStat)
        {
            return unitStat.modifiedValueIsUpdated ? unitStat.modifiedValue : unitStat.Modify();
        }

        public static implicit operator UnitStat(float value)
        {
            return new UnitStat(value);
        }

        private float Modify()
        {
            float product = 1f;
            foreach(float multiplier in multipliers)
            {
                product *= multiplier;
            }
            float sum = 0;
            foreach(float adder in adders)
            {
                sum += adder;
            }
            modifiedValue = value * product + sum;
            modifiedValueIsUpdated = true;
            return modifiedValue;
        }
    }
}

