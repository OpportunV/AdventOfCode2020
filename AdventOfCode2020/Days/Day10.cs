using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public class Day10
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");
        
        public static object Part1()
        {
            string[] lines;
            try
            {
                lines = Helper.GetInput(_inputPath);
            }
            catch (FileNotFoundException)
            {
                return -1;
            }

            var adapters = lines.Select(int.Parse).ToList();
            adapters.Add(adapters.Max() + 3);
            adapters.Sort();

            var currentJolt = 0;

            var joltDiffCounter = new Dictionary<int, int>
            {
                {1, 0},
                {2, 0},
                {3, 0},
            };

            foreach (var adapter in adapters)
            {
                joltDiffCounter[adapter - currentJolt]++;
                currentJolt = adapter;
            }

            return joltDiffCounter[1] * joltDiffCounter[3];
        }
        
        public static object Part2()
        {
            string[] lines;
            try
            {
                lines = Helper.GetInput(_inputPath);
            }
            catch (FileNotFoundException)
            {
                return -1;
            }
            
            var tmp = lines.Select(int.Parse).ToList();
            tmp.Add(0);
            tmp.Add(tmp.Max() + 3);
            tmp.Sort();
            var adapters = tmp.ToArray();

            var cache = new Dictionary<int, long>();

            long WaysToEnd(int currentIndex)
            {
                long ways = 0;
                if (currentIndex == adapters.Length - 1)
                {
                    return 1;
                }

                if (cache.ContainsKey(currentIndex))
                {
                    return cache[currentIndex];
                }

                for (int i = currentIndex + 1; i < adapters.Length; i++)
                {
                    if (adapters[i] - adapters[currentIndex] > 3)
                    {
                        break;
                    }

                    ways += WaysToEnd(i);
                }

                cache[currentIndex] = ways;
                return ways;
            }
            
            return WaysToEnd(0);
        }
    }
}

