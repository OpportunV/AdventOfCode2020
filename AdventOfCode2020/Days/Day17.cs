using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day17
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static int _offset = 1;
        
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

            var activeCubes = new HashSet<(int, int, int)>();

            for (int i = 0; i < lines.Length; i++)
            {
                var curLine = lines[i];
                for (int j = 0; j < curLine.Length; j++)
                {
                    if (curLine[j] == '#')
                    {
                        activeCubes.Add((i, j, 0));
                    }
                }
            }
            
            for (int i = 0; i < 6; i++)
            {
                var xMinBound = activeCubes.Min(item => item.Item1) - _offset;
                var xMaxBound = activeCubes.Max(item => item.Item1) + _offset;
                var yMinBound = activeCubes.Min(item => item.Item2) - _offset;
                var yMaxBound = activeCubes.Max(item => item.Item2) + _offset;
                var zMinBound = activeCubes.Min(item => item.Item3) - _offset;
                var zMaxBound = activeCubes.Max(item => item.Item3) + _offset;
                var newActiveCubes = new HashSet<(int, int, int)>();

                for (int x = xMinBound; x <= xMaxBound; x++)
                {
                    for (int y = yMinBound; y <= yMaxBound; y++)
                    {
                        for (int z = zMinBound; z <= zMaxBound; z++)
                        {
                            var activeCounter = Helper.Adjacent3D((x, y, z)).Count(pos => activeCubes.Contains(pos));
                            if (activeCubes.Contains((x, y, z)) && (activeCounter == 2 || activeCounter == 3))
                            {
                                newActiveCubes.Add((x, y, z));
                            }

                            if (!activeCubes.Contains((x, y, z)) && activeCounter == 3)
                            {
                                newActiveCubes.Add((x, y, z));
                            }
                        }
                    }
                }

                activeCubes = new HashSet<(int, int, int)>(newActiveCubes);
            }

            return activeCubes.Count;
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
            var activeCubes = new HashSet<(int, int, int, int)>();

            for (int i = 0; i < lines.Length; i++)
            {
                var curLine = lines[i];
                for (int j = 0; j < curLine.Length; j++)
                {
                    if (curLine[j] == '#')
                    {
                        activeCubes.Add((i, j, 0, 0));
                    }
                }
            }
            
            for (int i = 0; i < 6; i++)
            {
                var xMinBound = activeCubes.Min(item => item.Item1) - _offset;
                var xMaxBound = activeCubes.Max(item => item.Item1) + _offset;
                var yMinBound = activeCubes.Min(item => item.Item2) - _offset;
                var yMaxBound = activeCubes.Max(item => item.Item2) + _offset;
                var zMinBound = activeCubes.Min(item => item.Item3) - _offset;
                var zMaxBound = activeCubes.Max(item => item.Item3) + _offset;
                var wMinBound = activeCubes.Min(item => item.Item4) - _offset;
                var wMaxBound = activeCubes.Max(item => item.Item4) + _offset;
                var newActiveCubes = new HashSet<(int, int, int, int)>();

                for (int x = xMinBound; x <= xMaxBound; x++)
                {
                    for (int y = yMinBound; y <= yMaxBound; y++)
                    {
                        for (int z = zMinBound; z <= zMaxBound; z++)
                        {
                            for (int w = wMinBound; w <= wMaxBound; w++)
                            {
                                var activeCounter =
                                    Helper.Adjacent4D((x, y, z, w)).Count(pos => activeCubes.Contains(pos));
                                if (activeCubes.Contains((x, y, z, w)) && (activeCounter == 2 || activeCounter == 3))
                                {
                                    newActiveCubes.Add((x, y, z, w));
                                }

                                if (!activeCubes.Contains((x, y, z, w)) && activeCounter == 3)
                                {
                                    newActiveCubes.Add((x, y, z, w));
                                }
                            }
                        }
                    }
                }

                activeCubes = new HashSet<(int, int, int, int)>(newActiveCubes);
            }

            return activeCubes.Count;
        }
    }
}

