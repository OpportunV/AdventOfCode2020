using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public class Day8
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

            RunProgram(lines, out var acc);

            return acc;
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

            var currentChangeIndex = 0;
            int acc;
            var tmpLines = new string[lines.Length];
            Array.Copy(lines, tmpLines, lines.Length);

            while (!RunProgram(tmpLines, out acc))
            {
                while (tmpLines[currentChangeIndex].Contains("acc"))
                {
                    currentChangeIndex++;
                }
                Array.Copy(lines, tmpLines, tmpLines.Length);
                if (tmpLines[currentChangeIndex].Contains("jmp"))
                {
                    tmpLines[currentChangeIndex] = tmpLines[currentChangeIndex].Replace("jmp","nop");
                }
                else
                {
                    tmpLines[currentChangeIndex] = tmpLines[currentChangeIndex].Replace("nop","jmp");
                }
                
                currentChangeIndex++;
            }

            return acc;
        }

        private static bool RunProgram(string[] lines, out int acc)
        {
            var seen = new HashSet<int>();
            var currentLineIndex = 0;
            acc = 0;

            while (!seen.Contains(currentLineIndex))
            {
                seen.Add(currentLineIndex);
                var line = lines[currentLineIndex].Split(' ');
                var command = line[0];
                var value = int.Parse(line[1]);
                switch (command)
                {
                    case "jmp":
                        currentLineIndex += value;
                        break;
                    case "acc":
                        acc += value;
                        currentLineIndex += 1;
                        break;
                    default:
                        currentLineIndex += 1;
                        break;
                }
                
                if (currentLineIndex >= lines.Length - 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

