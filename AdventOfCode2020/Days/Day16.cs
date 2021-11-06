using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public class Day16
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");
        private static readonly List<(int, int, int, int)> _restrictions = new List<(int, int, int, int)>();
        private static int _restrictionsStopIndex;
        private static int _myTicketIndex;
        private static int _nearbyTicketIndex;
        
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

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim() == "your ticket:")
                {
                    _restrictionsStopIndex = i - 1;
                    _myTicketIndex = i + 1;
                }
                if (lines[i].Trim() == "nearby tickets:")
                {
                    _nearbyTicketIndex = i + 1;
                }
            }
            
            var tickets = new List<int[]>();

            for (int i = 0; i < _restrictionsStopIndex; i++)
            {
                var tmp = lines[i].Split(':')[1].Split(new[] { "or" }, StringSplitOptions.None);
                var left = tmp[0].Split('-');
                var right = tmp[1].Split('-');
                _restrictions.Add((int.Parse(left[0]), int.Parse(left[1]), int.Parse(right[0]), int.Parse(right[1])));
            }

            for (int i = _nearbyTicketIndex; i < lines.Length; i++)
            {
                tickets.Add(lines[i].Split(',').Select(int.Parse).ToArray());
            }

            return tickets.Sum(ticket => ticket.Where(InvalidateValueByRestrictions).FirstOrDefault());
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

            var tickets = new List<int[]>();
            
            for (int i = _nearbyTicketIndex; i < lines.Length; i++)
            {
                tickets.Add(lines[i].Split(',').Select(int.Parse).ToArray());
            }

            var validTickets = tickets.Where(ticket => !ticket.Where(InvalidateValueByRestrictions).Any()).ToList();
            var myTicket = lines[_myTicketIndex].Split(',').Select(int.Parse).ToArray();
            validTickets.Add(myTicket);

            var restrictionIndexes = new Dictionary<int, HashSet<int>>();

            for (int i = 0; i < _restrictions.Count; i++)
            {
                restrictionIndexes[i] = new HashSet<int>();
                var restriction = _restrictions[i];
                for (int j = 0; j < validTickets[0].Length; j++)
                {
                    var curInd = j;
                    if (ValidateValuesByRestriction(validTickets.Select(item => item[curInd]), restriction))
                    {
                        restrictionIndexes[i].Add(j);
                    }
                }
            }

            var departureIndexes = new int[6];
            var usedIndexes = new HashSet<int>();

            while (usedIndexes.Count < validTickets[0].Length)
            {
                var current = restrictionIndexes.First(pair => pair.Value.Count == usedIndexes.Count + 1);
                var curInd = current.Value.Except(usedIndexes).First();
                usedIndexes.Add(curInd);
                if (current.Key < 6)
                {
                    departureIndexes[current.Key] = curInd;
                }
            }

            return departureIndexes.Aggregate(1L, (current, departureIndex) => current * myTicket[departureIndex]);
        }

        private static bool InvalidateValueByRestrictions(int value)
        {
            return !_restrictions.Any(item =>
            {
                var (a, b, c, d) = item;
                return value >= a && value <= b || value >= c && value <= d;
            });
        }
        
        private static bool ValidateValuesByRestriction(IEnumerable<int> values, (int, int, int, int) restriction)
        {
            var (a, b, c, d) = restriction;
            return values.All(value => value >= a && value <= b || value >= c && value <= d);
        }
    }
}

