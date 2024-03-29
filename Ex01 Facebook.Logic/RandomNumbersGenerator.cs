﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex01_Facebook.Logic
{
    public class RandomNumbersGenerator
    {
        private Random m_Random = new Random();

        public List<int> GenerateRandom(int i_CountOfNumberToGenerate, int i_Min, int i_Max)
        {
            // max - min > 0 required to avoid overflow
            if (i_Max <= i_Min || i_CountOfNumberToGenerate < 0 ||
                    (i_CountOfNumberToGenerate > i_Max - i_Min && i_Max - i_Min > 0))
            {
                // need to use 64-bit to support big ranges (negative min, positive max)
                throw new ArgumentOutOfRangeException();
            }

            // generate count random values.
            HashSet<int> candidates = new HashSet<int>();

            // start count values before max, and end at max
            for (int top = i_Max - i_CountOfNumberToGenerate; top < i_Max; top++)
            {
                // May strike a duplicate.
                // Need to add +1 to make inclusive generator
                // +1 is safe even for MaxVal max value because top < max
                if (!candidates.Add(m_Random.Next(i_Min, top + 1)))
                {
                    // collision, add inclusive max.
                    // which could not possibly have been added before.
                    candidates.Add(top);
                }
            }

            // load them in to a list, to sort
            List<int> result = candidates.ToList();

            // shuffle the results because HashSet has messed
            // with the order, and the algorithm does not produce
            // random-ordered results
            for (int i = result.Count - 1; i > 0; i--)
            {
                int k = m_Random.Next(i + 1);
                int tmp = result[k];
                result[k] = result[i];
                result[i] = tmp;
            }

            return result;
        }

        public List<int> GenerateRandom(int i_Count)
        {
            return GenerateRandom(i_Count, 0, int.MaxValue);
        }
    }
}
