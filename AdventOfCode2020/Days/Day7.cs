using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public class Day7
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private const string MyBag = "shiny gold";

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

            var bagsRules = new Dictionary<string, HashSet<string>>();
            var usefulBags = new HashSet<string> { MyBag };

            foreach (var line in lines)
            {
                var temp = line.Split(new[] {"contain"}, StringSplitOptions.None);
                var bag = temp[0].Substring(0, temp[0].IndexOf("bag", StringComparison.Ordinal)).Trim();
                var rules = temp[1].Split(',')
                    .Select(item => item.Substring(2, item.IndexOf("bag", StringComparison.Ordinal) - 2).Trim());
                var rulesSet = new HashSet<string>(rules);
                bagsRules.Add(bag, rulesSet);
                if (rulesSet.Contains(MyBag))
                {
                    usefulBags.Add(bag);
                }
            }

            var prevLenght = 0;
            while (prevLenght != usefulBags.Count)
            {
                prevLenght = usefulBags.Count;
                foreach (var pair in bagsRules)
                {
                    var bag = pair.Key;
                    var rules = pair.Value;
                    if (rules.Intersect(usefulBags).Any())
                    {
                        usefulBags.Add(bag);
                    }
                }
            }

            return bagsRules.Select(pair => pair.Value)
                .Count(rules => rules.Intersect(usefulBags).Any());
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
            
            var bagsRules = new Dictionary<string, HashSet<string>>();
            
            var usefulBags = new HashSet<string> { MyBag };

            foreach (var line in lines)
            {
                var temp = line.Split(new[] {"contain"}, StringSplitOptions.None);
                var bag = temp[0].Substring(0, temp[0].IndexOf("bag", StringComparison.Ordinal)).Trim();
                var rules = temp[1].Split(',')
                    .Select(item => item.Substring(0, item.IndexOf("bag", StringComparison.Ordinal)).Trim());
                var rulesSet = new HashSet<string>(rules);
                bagsRules.Add(bag, rulesSet);
            }

            var counter = 0;
            var queue = new Queue<(string, int)>(ParseBagString(bagsRules[MyBag]));
            while (queue.Count > 0)
            {
                var (bag, amount) = queue.Dequeue();
                counter += amount;
                if (bag == "no other" || !bagsRules.ContainsKey(bag))
                {
                    continue;
                }

                foreach (var valueTuple in ParseBagString(bagsRules[bag], amount))
                {
                    queue.Enqueue(valueTuple);
                }
            }

            return counter;
        }

        private static IEnumerable<(string, int)> ParseBagString(IEnumerable<string> bags, int multiplier = 1)
        {
            return bags.Select(item => item != "no other" 
                ? (item.Substring(2), multiplier * int.Parse(item.Substring(0, 1))) 
                : ("no other", 0));
        }
    }
}

