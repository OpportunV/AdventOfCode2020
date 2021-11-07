using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public class Day19
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static readonly Dictionary<string, (HashSet<string>, HashSet<string>)> _rules =
            new Dictionary<string, (HashSet<string>, HashSet<string>)>();
        private static readonly Dictionary<string, string> _letters = new Dictionary<string, string>();
        
        private static readonly Dictionary<(int, int, string), bool> _cache = new Dictionary<(int, int, string), bool>();
        private const string TargetRule = "0";

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

            var messages = new HashSet<string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                if (char.IsDigit(line[0]))
                {
                    var split = line.Split(':');
                    var ruleId = split[0].Trim();
                    
                    var dependencies = split[1].Trim().Split('|');
                    var left = new HashSet<string>();
                    if (char.IsDigit(dependencies[0][0]))
                    {
                        left = new HashSet<string>(dependencies[0].Trim().Split(' '));
                    }
                    else
                    {
                        _letters[ruleId] = dependencies[0].Trim().Trim('"');
                    }

                    var right = new HashSet<string>();
                    if (dependencies.Length > 1)
                    {
                        right = new HashSet<string>(dependencies[1].Trim().Split(' '));
                    }
                    
                    _rules[ruleId] = (left, right);
                }
                else
                {
                    messages.Add(line);
                }
            }

            var ans = 0;

            foreach (var message in messages)
            {
                _cache.Clear();
                ans += Match(message, 0, message.Length, TargetRule)
                    ? 1
                    : 0;
            }

            return ans;
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
            
            return -1;
        }

        private static bool MatchRules(string message, int pos, int len, HashSet<string> rules)
        {
            if (pos == len && rules.Count == 0)
            {
                return true;
            }

            if (pos == len)
            {
                return false;
            }

            if (rules.Count == 0)
            {
                return false;
            }

            for (int i = pos + 1; i <= len; i++)
            {
                if (i == len && rules.Count == 1)
                {
                    continue;
                }

                var tmp = new List<string>(rules);
                var firstRule = tmp[0];
                tmp.RemoveAt(0);

                if (Match(message, pos, i, firstRule)
                    && MatchRules(message, i, len, new HashSet<string>(tmp)))
                {
                    return true;
                }
            }
            
            return false;
        }

        private static bool Match(string message, int pos, int len, string rule)
        {
            var key = (pos, len, rule);
            if (_cache.ContainsKey(key))
            {
                // Console.WriteLine($"{pos} {len} {rule} {_cache[key]}");
                return _cache[key];
            }

            var firstOption = _rules[rule].Item1;
            var flag = false;
            if (_letters.ContainsKey(rule))
            {
                // Console.WriteLine(_letters[rule][0]);
                flag = _letters[rule][0] == message[pos];
            }
            else
            {
                var secondOption = _rules[rule].Item2;
                if (MatchRules(message, pos, len, firstOption) || MatchRules(message, pos, len, secondOption))
                {
                    _cache[key] = true;
                    return true;
                }
            }
            
            _cache[key] = flag;
            return false;
        }
    }
}

