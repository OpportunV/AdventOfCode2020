using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day11
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static int _width;
        private static int _height;
        
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

            _width = lines[0].Length;
            _height = lines.Length;

            var seen = new HashSet<string>();
            while (!seen.Contains(string.Join("", lines)))
            {
                seen.Add(string.Join("", lines));
                var currentState = new string[_height];
                for (int i = 0; i < _height; i++)
                {
                    var newLine = new StringBuilder();
                    var currentLine = lines[i];
                    for (int j = 0; j < _width; j++)
                    {
                        var currentChar = currentLine[j];
                        var counter = 0;
                        foreach (var (x, y) in Helper.Adjacent((i, j), _width, _height))
                        {
                            counter += lines[x][y] == '#'
                                ? 1
                                : 0;
                        }
                        switch (currentChar)
                        {
                            case 'L' when counter == 0:
                                newLine.Append("#");
                                break;
                            case '#' when counter >= 4:
                                newLine.Append("L");
                                break;
                            default:
                                newLine.Append(currentChar);
                                break;
                        }
                    }

                    currentState[i] = newLine.ToString();
                }
                
                Array.Copy(currentState, lines, currentState.Length);
            }

            var occupied = lines.Select(line => line.Count(chr => chr == '#')).Sum();
            
            return occupied;
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
            
            var seen = new HashSet<string>();
            while (!seen.Contains(string.Join("", lines)))
            {
                seen.Add(string.Join("", lines));
                var currentState = new string[_height];
                for (int i = 0; i < _height; i++)
                {
                    var newLine = new StringBuilder();
                    var currentLine = lines[i];
                    for (int j = 0; j < _width; j++)
                    {
                        var currentChar = currentLine[j];
                        var counter = Helper.CountOccupiedAdjacentWithDistance((i, j), _width, _height, lines);
                        switch (currentChar)
                        {
                            case 'L' when counter == 0:
                                newLine.Append("#");
                                break;
                            case '#' when counter >= 5:
                                newLine.Append("L");
                                break;
                            default:
                                newLine.Append(currentChar);
                                break;
                        }
                    }

                    currentState[i] = newLine.ToString();
                }
                
                Array.Copy(currentState, lines, currentState.Length);
            }

            var occupied = lines.Select(line => line.Count(chr => chr == '#')).Sum();
            
            return occupied;
        }
    }
}

