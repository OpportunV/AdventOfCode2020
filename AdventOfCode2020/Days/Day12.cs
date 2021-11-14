using System;
using System.IO;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day12
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

            var x = 0;
            var y = 0;
            var rotation = 0; // east

            foreach (var line in lines)
            {
                var command = line[0];
                var value = int.Parse(line.Substring(1));

                switch (command)
                {
                    case 'N':
                        y += value;
                        break;
                    case 'S':
                        y -= value;
                        break;
                    case 'E':
                        x += value;
                        break;
                    case 'W':
                        x -= value;
                        break;
                    case 'L':
                        rotation = (rotation + value) % 360;
                        break;
                    case 'R':
                        rotation = (rotation - value + 360) % 360;
                        break;
                    case 'F':
                        x += rotation == 0 
                            ? value 
                            : rotation == 180 
                                ? -value 
                                : 0;
                        y += rotation == 90 
                            ? value 
                            : rotation == 270 
                                ? -value 
                                : 0;
                        break;
                }
            }
            
            return Math.Abs(x) + Math.Abs(y);
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
            
            var shipX = 0;
            var shipY = 0;
            var waypointX = 10;
            var waypointY = 1;

            foreach (var line in lines)
            {
                var command = line[0];
                var value = int.Parse(line.Substring(1));

                switch (command)
                {
                    case 'N':
                        waypointY += value;
                        break;
                    case 'S':
                        waypointY -= value;
                        break;
                    case 'E':
                        waypointX += value;
                        break;
                    case 'W':
                        waypointX -= value;
                        break;
                    case 'L':
                        for (int i = 0; i < value / 90; i++)
                        {
                            (waypointX, waypointY) = (-waypointY, waypointX);
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < value / 90; i++)
                        {
                            (waypointX, waypointY) = (waypointY, -waypointX);
                        }
                        break;
                    case 'F':
                        shipX += waypointX * value;
                        shipY += waypointY * value;
                        break;
                }
            }
            
            return Math.Abs(shipX) + Math.Abs(shipY);
        }
    }
}

