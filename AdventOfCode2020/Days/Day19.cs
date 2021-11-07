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

        private static readonly Dictionary<int, Rule> _rules = new Dictionary<int, Rule>();
        
        private const int TargetRule = 0;

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
                    var ruleId = int.Parse(split[0].Trim());
                    
                    _rules[ruleId] = new Rule
                    {
                        source = split[1].Trim()
                    };
                }
                else
                {
                    messages.Add(line);
                }
            }

            foreach (var rule in _rules.Values)
            {
                Compile(rule);
            }
            
            var targetRule = _rules[TargetRule].compiled.ToHashSet();
            
            return messages.Where(targetRule.Contains).Count();
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
                    var ruleId = int.Parse(split[0].Trim());

                    _rules[ruleId] = new Rule()
                    {
                        source = split[1].Trim()
                    };
                }
                else
                {
                    messages.Add(line);
                }
            }

            foreach (var rule in _rules.Values)
            {
                Compile(rule);
            }
            
            var ans = 0;

            var rule42 = _rules[42].compiled.ToHashSet();
            var rule31 = _rules[31].compiled.ToHashSet();
            var rule42Len = rule42.First().Length;
            var rule31Len = rule31.First().Length;

            foreach (var message in messages)
            {
                var rule31Count = 0;
                var cur = message;
                while (cur.Length >= rule31Len && rule31.Contains(cur.Substring(cur.Length - rule31Len)))
                {
                    cur = cur.Substring(0, cur.Length - rule31Len);
                    rule31Count++;
                }
                if (rule31Count < 1)
                {
                    continue;
                }

                var rule42Count = 0;
                while (cur.Length >= rule42Len && rule42.Contains(cur.Substring(0, rule42Len)))
                {
                    cur = cur.Substring(rule42Len);
                    rule42Count++;
                }
                if (rule42Count < rule31Count + 1)
                {
                    continue;
                }

                if (cur.Length == 0)
                {
                    ans++;
                }
            }

            return ans;
        }

        private class Rule
        {
            public string source;
            public string[] compiled;
        }
        
        private static void Compile(Rule rule)
        {
            if (rule.compiled != null)
            {
                return;
            }

            if (rule.source[0] == '"')
            {
                rule.compiled = new[] { rule.source.Substring(1, rule.source.Length - 2) };
                return;
            }

            var compiled = new List<string>();

            void CombineValues(string prefix, Span<string[]> fragments)
            {
                if (fragments.Length == 0)
                {
                    compiled.Add(prefix);
                    return;
                }

                foreach (var s in fragments[0])
                {
                    CombineValues(prefix + s, fragments.Slice(1));
                }
            }

            foreach (var subRules in rule.source.Split('|'))
            {
                var fragments = new List<string[]>();
                foreach (var r in subRules.Trim().Split())
                {
                    var id = int.Parse(r.Trim());
                    var rr = _rules[id];
                    Compile(rr);
                    fragments.Add(rr.compiled);
                }

                CombineValues("", fragments.ToArray());
            }

            rule.compiled = compiled.ToArray();
        }
    }
}

