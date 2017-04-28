using System;
using System.Collections.Generic;

namespace Game.Units
{
    public class StatModifier
    {
        /// <summary>
        /// Gets the list of adders. These will be summed up and added together after multiplying with the multipliers.
        /// </summary>
        /// <value>The adders.</value>
        public List<float> Adders { get; private set; }

        /// <summary>
        /// Gets the list of multipliers. These will be multiplied together and then multiplied with the stat before adding with the adders.
        /// </summary>
        /// <value>The multipliers.</value>
        public List<float> Multipliers { get; private set; }

        public StatModifier()
        {
            Adders = new List<float>();
            Multipliers = new List<float>();
        }

        /// <summary>
        /// Modify the specified stat. result = stat * productOfMultipliers + sumOfAdders.
        /// </summary>
        /// <param name="stat">Stat.</param>
        public float Modify(float stat)
        {
            float product = 1;
            foreach(float multiplier in Multipliers)
            {
                product *= multiplier;
            }

            float sum = 0;
            foreach(float adder in Adders)
            {
                sum += adder;
            }
            return stat * product + sum;
        }
    }
}

