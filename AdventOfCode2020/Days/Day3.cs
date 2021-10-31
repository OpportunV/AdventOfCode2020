using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public class Day3
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

            var lenY = lines.Length;
            var lenX = lines[0].Length;
            var curLineX = 3;
            var curLineY = 1;
            var counter = 0;
            while (curLineY < lenY)
            {
                if (lines[curLineY][curLineX % lenX] == '#')
                {
                    counter++;
                }

                curLineX += 3;
                curLineY++;
            }

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

            var slopes = new List<(int, int)>
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2),
            };
            
            var lenY = lines.Length;
            var lenX = lines[0].Length;
            var total = 1d;

            foreach (var (dx, dy) in slopes)
            {
                var curLineX = dx;
                var curLineY = dy;
                var counter = 0;
                while (curLineY < lenY)
                {
                    if (lines[curLineY][curLineX % lenX] == '#')
                    {
                        counter++;
                    }

                    curLineX += dx;
                    curLineY += dy;
                }

                total *= counter;
            }

            return total;
        }
    }
}