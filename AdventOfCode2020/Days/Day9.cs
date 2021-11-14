using System.IO;
using System.Linq;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day9
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

            bool ValidateNumber(int index, long[] array)
            {
                var curNumber = array[index];
                for (int i = index - 25; i < index; i++)
                {
                    var iNumber = array[i];
                    for (int j = index - 25; j < index; j++)
                    {
                        if (curNumber == iNumber + array[j])
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            var numbers = lines.Select(long.Parse).ToArray();

            for (int i = 0; i < numbers.Length; i++)
            {
                if (i < 25)
                {
                    continue;
                }

                if (!ValidateNumber(i, numbers))
                {
                    return numbers[i];
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
            
            var numbers = lines.Select(long.Parse).ToArray();
            var target = (long) Part1();

            for (int i = 0; i < numbers.Length; i++)
            {
                var sum = numbers[i];
                var min = numbers[i];
                var max = numbers[i];
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    var current = numbers[j];
                    sum += current;
                    min = current < min
                        ? current
                        : min;
                    max = current > max
                        ? current
                        : max;
                    if (sum == target)
                    {
                        return min + max;
                    }
                }
                
            }

            return -1;
        }
    }
}

