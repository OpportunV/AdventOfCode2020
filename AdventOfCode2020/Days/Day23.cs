using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day23
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static readonly Dictionary<int, Cup> _cupMap = new Dictionary<int, Cup>();
        private static int[] _inputCups;
        private const int PartTwoCupAmount = 1_000_000;
        private const int PartOneMovesAmount = 100;
        private const int PartTwoMovesAmount = 10_000_000;

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

            _inputCups = lines[0].Select(item => int.Parse(item.ToString())).ToArray();

            var currentCup = new Cup { value = _inputCups[0] };
            _cupMap.Add(currentCup.value, currentCup);

            for (int i = 1; i < _inputCups.Length; i++)
            {
                var newCup = new Cup { value = _inputCups[i] };
                _cupMap.Add(newCup.value, newCup);
                currentCup.next = newCup;
                currentCup = newCup;
            }

            currentCup.next = _cupMap[_inputCups[0]];
            
            Simulate(_cupMap[_inputCups[0]], PartOneMovesAmount);

            var ans = new StringBuilder();

            var startCup = _cupMap[1];
            var current = startCup.next;

            while (current != startCup)
            {
                ans.Append(current.value);
                current = current.next;
            }
            
            return ans.ToString();
        }
        
        public static object Part2()
        {
            _cupMap.Clear();
            
            var currentCup = new Cup { value = _inputCups[0] };
            _cupMap.Add(currentCup.value, currentCup);

            for (int i = 1; i < _inputCups.Length; i++)
            {
                var newCup = new Cup { value = _inputCups[i] };
                _cupMap.Add(newCup.value, newCup);
                currentCup.next = newCup;
                currentCup = newCup;
            }

            for (int i = 10; i <= PartTwoCupAmount; i++)
            {
                var newCup = new Cup { value = i };
                _cupMap.Add(newCup.value, newCup);
                currentCup.next = newCup;
                currentCup = newCup;
            }

            currentCup.next = _cupMap[_inputCups[0]];
            
            Simulate(_cupMap[_inputCups[0]], PartTwoMovesAmount);
            
            var startCup = _cupMap[1];

            
            return (long)startCup.next.value * startCup.next.next.value;
        }

        private static void Simulate(Cup current, int movesAmount)
        {
            var maxValue = _cupMap.Keys.Max();
            for (int i = 0; i < movesAmount; i++)
            {
                var firstPickedUp = current.next;
                var pickedValues = new HashSet<int>
                    { firstPickedUp.value, firstPickedUp.next.value, firstPickedUp.next.next.value };
                current.next = firstPickedUp.next.next.next;

                var nextValue = current.value - 1;
                if (nextValue < 1)
                {
                    nextValue = maxValue;
                }
                while (pickedValues.Contains(nextValue))
                {
                    nextValue--;
                    if (nextValue < 1)
                    {
                        nextValue = maxValue;
                    }
                }

                var destination = _cupMap[nextValue];
                (destination.next, firstPickedUp.next.next.next) = (firstPickedUp, destination.next);
                current = current.next;
            }
        }
    }

    internal class Cup
    {
        public int value;
        public Cup next;
    }
}

