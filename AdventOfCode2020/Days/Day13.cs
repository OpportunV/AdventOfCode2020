using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public class Day13
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
            
            var timestamp = int.Parse(lines[0]);
            var buses = lines[1].Split(',')
                .Where(item => item != "x")
                .Select(int.Parse);

            var closestBus = -1;
            var waitTime = int.MaxValue;
            foreach (var busId in buses)
            {
                var curWaitTime = busId - timestamp % busId;
                if (curWaitTime < waitTime)
                {
                    waitTime = curWaitTime;
                    closestBus = busId;
                }
            }

            return closestBus * waitTime;
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
            
            // https://en.wikipedia.org/wiki/Chinese_remainder_theorem
            var buses = new Dictionary<int, int>();
            var schedule = lines[1].Split(',');
            long product = 1;

            for (var i = 0; i < schedule.Length; i++)
            {
                var curBus = schedule[i];
                if (curBus != "x")
                {
                    var busId = int.Parse(curBus);
                    buses.Add(busId, busId - i % busId);
                    product *= busId;
                }
            }

            long timestamp = 0;
            foreach (var pair in buses)
            {
                var period = pair.Key;
                var a = pair.Value;

                long n = product / period;
                long m = (long)BigInteger.ModPow(n, period - 2, period);
                timestamp += a * m * n;
            }
            
            return timestamp % product;
        }
    }
}

