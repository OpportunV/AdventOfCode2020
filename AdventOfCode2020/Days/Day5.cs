using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public class Day5
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

            var max = 0;
            
            foreach (var line in lines)
            {
                var curId = GetSeatId(line);
            
                max = Math.Max(max, curId);
            }

            return max;
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

            var seatIds = new HashSet<int>(lines.Select(GetSeatId));

            for (int i = 1; i < 127 * 8 + 7; i++)
            {
                if (!seatIds.Contains(i) && seatIds.Contains(i + 1) && seatIds.Contains(i - 1))
                {
                    return i;
                }
            }
            
            return -1;
        }
        
        private static int GetSeatId(string line)
        {
            var curMax = 127;
            var curMin = 0;
            for (int i = 0; i < 7; i++)
            {
                if (line[i] == 'F')
                {
                    curMax -= (curMax - curMin + 1) / 2;
                }
                else
                {
                    curMin += (curMax - curMin + 1) / 2;
                }
            }
            
            var curRow = curMin;
            curMax = 7;
            curMin = 0;
            for (int i = 7; i < 10; i++)
            {
                if (line[i] == 'L')
                {
                    curMax -= (curMax - curMin + 1) / 2;
                }
                else
                {
                    curMin += (curMax - curMin + 1) / 2;
                }
            }
            
            return curMin + curRow * 8;
        }
    }
}

