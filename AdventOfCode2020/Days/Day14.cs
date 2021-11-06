using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public class Day14
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
            
            long currentOrMask = 0;
            long currentAndMask = 0;
            var memory = new Dictionary<int, long>();

            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    var mask = line.Split('=')[1].Trim();
                    currentAndMask = Convert.ToInt64(mask.Replace('X', '1'), 2);
                    currentOrMask = Convert.ToInt64(mask.Replace('X', '0'), 2);
                }
                else
                {
                    var firstInd = line.IndexOf("[", StringComparison.Ordinal) + 1;
                    var lenght = line.IndexOf("]", StringComparison.Ordinal) - firstInd;
                    var address = int.Parse(line.Substring(firstInd, lenght));
                    var data = long.Parse(line.Split('=')[1].Trim());

                    memory[address] = data & currentAndMask | currentOrMask;
                }
            }

            return memory.Values.Sum();
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
            
            var memory = new Dictionary<long, long>();
            long initialAndMask = 0;
            var floatingPos = new List<int>();
            var addressMasks = new List<long>();

            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    floatingPos.Clear();
                    addressMasks.Clear();
                    var mask = line.Split('=')[1].Trim();
                    for (int i = mask.Length - 1; i >= 0; i--)
                    {
                        if (mask[i] == 'X')
                        {
                            floatingPos.Add(mask.Length - 1 - i);
                        }
                    }

                    var andMask = mask.Replace('0', '1').Replace('X', '0');
                    initialAndMask = Convert.ToInt64(andMask, 2);
                    var orMask = mask.Replace('X', '0');
                    var initialOrMask = Convert.ToInt64(orMask, 2);
                    
                    var permutationsAmount = Math.Pow(2, floatingPos.Count);
                    for (long i = 0; i < permutationsAmount; i++)
                    {
                        var tmp = initialOrMask;

                        for (int j = 0; j < floatingPos.Count; j++)
                        {
                            if (((1L << j) & i) > 0)
                            {
                                var oneBitMask = 1L << floatingPos[j];
                                tmp |= oneBitMask;
                            }
                        }
                        
                        addressMasks.Add(tmp);
                    }
                }
                else
                {
                    var firstInd = line.IndexOf("[", StringComparison.Ordinal) + 1;
                    var lenght = line.IndexOf("]", StringComparison.Ordinal) - firstInd;
                    var address = long.Parse(line.Substring(firstInd, lenght));
                    var data = long.Parse(line.Split('=')[1].Trim());

                    address &= initialAndMask;
                    
                    foreach (var mask in addressMasks)
                    {
                        var curAddress = address | mask;
                        memory[curAddress] = data;
                    }
                }
            }

            return memory.Values.Sum();
        }
    }
}

