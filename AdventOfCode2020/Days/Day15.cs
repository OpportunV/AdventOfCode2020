using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day15
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

            var startingNumbers = lines[0].Split(',').Select(int.Parse).ToArray();
            var currentPos = 0;
            var targetPos = 2020;
            var positions = new Dictionary<int, int>();
            foreach (var number in startingNumbers)
            {
                positions[number] = ++currentPos;
            }

            var lastNumber = startingNumbers.Last();

            while (currentPos < targetPos)
            {
                var nextNumber = positions.ContainsKey(lastNumber)
                    ? currentPos - positions[lastNumber]
                    : 0;
                positions[lastNumber] = currentPos++;
                lastNumber = nextNumber;
            }
            
            return lastNumber;
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
            var startingNumbers = lines[0].Split(',').Select(int.Parse).ToArray();
            var currentPos = 0;
            var targetPos = 30000000;
            var positions = new Dictionary<int, int>();
            foreach (var number in startingNumbers)
            {
                positions[number] = ++currentPos;
            }

            var lastNumber = startingNumbers.Last();

            while (currentPos < targetPos)
            {
                var nextNumber = positions.ContainsKey(lastNumber)
                    ? currentPos - positions[lastNumber]
                    : 0;
                positions[lastNumber] = currentPos++;
                lastNumber = nextNumber;
            }
            
            return lastNumber;
        }
    }
}

