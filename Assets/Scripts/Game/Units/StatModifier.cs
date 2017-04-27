using System;
using System.Collections.Generic;

namespace Game.Units
{
    public class StatModifier
    {
        public List<float> Adders { get; private set; }

        public List<float> Multipliers { get; private set; }

        public StatModifier()
        {
            Adders = new List<float>();
            Multipliers = new List<float>();
        }

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

