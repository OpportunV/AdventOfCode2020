using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public class Day6
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

            var counter = 0;
            var currentSet = new HashSet<char>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    counter += currentSet.Count;
                    currentSet.Clear();
                    continue;
                }
                
                currentSet.UnionWith(line);
            }
            counter += currentSet.Count;

            return counter;
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
            var counter = 0;
            var currentSet = new HashSet<char>(lines[0]);

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    counter += currentSet.Count;
                    currentSet = new HashSet<char>(lines[i + 1]);
                    continue;
                }

                currentSet.IntersectWith(line);
            }

            counter += currentSet.Count;

            return counter;
        }
    }
}

