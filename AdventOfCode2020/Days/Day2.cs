using System.IO;
using System.Linq;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day2
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

            var counter = 0;

            foreach (var line in lines)
            {
                ParseInput(line, out var min, out var max, out var letter, out var password);
                var curCount = password.Count(item => item == letter);
                if (curCount >= min && curCount <= max)
                {
                    counter++;
                }
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

            var counter = 0;

            foreach (var line in lines)
            {
                ParseInput(line, out var min, out var max, out var letter, out var password);
                if (password[min - 1] == letter ^ password[max - 1] == letter)
                {
                    counter++;
                }
            }
            
            return counter;
        }

        private static void ParseInput(string input, out int min, out int max, out char letter, out string password)
        {
            var splits = input.Split(' ');
            var tmp = splits[0].Split('-');
            (min, max) = (int.Parse(tmp[0]), int.Parse(tmp[1]));
            letter = splits[1].TrimEnd(':').ToCharArray()[0];
            password = splits[2];
        }
    }
}