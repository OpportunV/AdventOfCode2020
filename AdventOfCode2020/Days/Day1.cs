using System.IO;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day1
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
            
            foreach (var firstLine in lines)
            {
                var first = int.Parse(firstLine);
                foreach (var secondLine in lines)
                {
                    var second = int.Parse(secondLine);
                    if (first + second == 2020)
                    {
                        return first * second;
                    }
                }
            }
            
            return -1;
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
            
            foreach (var firstLine in lines)
            {
                var first = int.Parse(firstLine);
                foreach (var secondLine in lines)
                {
                    var second = int.Parse(secondLine);
                    foreach (var thirdLine in lines)
                    {
                        var third = int.Parse(thirdLine);
                        if (first + second + third == 2020)
                        {
                            return first * second * third;
                        }
                    }
                }
            }
            
            return -1;
        }
    }
}