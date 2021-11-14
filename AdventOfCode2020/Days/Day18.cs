using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day18
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static readonly Regex _bracesRegEx = new Regex(@"\([\d+* ]+?\)");
        private static readonly Regex _additionRegEx = new Regex(@"(\d+) \+ (\d+)");
        
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
            
            return lines.Select(item => ReduceBraces(item, EvaluateExpression))
                .Sum(EvaluateExpression);
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
            
            return lines.Select(item => ReduceBraces(item, EvaluateExpressionAddPriority))
                .Sum(EvaluateExpressionAddPriority);
        }

        private static string ReduceBraces(string expression, Func<string, long> evaluator)
        {
            while (expression.Contains("("))
            {
                var matches = _bracesRegEx.Matches(expression);

                foreach (Match match in matches)
                {
                    var curExpression = match.Value;
                    expression = expression.Replace(curExpression, 
                        evaluator(curExpression.Trim('(', ')')).ToString());
                }
            }
            
            return expression;
        }

        private static long EvaluateExpression(string expression)
        {
            var curOperator = '+';
            long total = 0;

            foreach (var item in expression.Split(' '))
            {
                if (!long.TryParse(item, out var current))
                {
                    curOperator = item[0];
                }
                else
                {
                    total = curOperator == '+'
                        ? total + current
                        : total * current;
                }
            }
            
            return total;
        }
        
        private static long EvaluateExpressionAddPriority(string expression)
        {
            while (expression.Contains('+'))
            {
                var additionPair = _additionRegEx.Match(expression).Value;
                var split = additionPair.Split('+');
                var ans = (long.Parse(split[0]) + long.Parse(split[1])).ToString();
                var regex = new Regex(Regex.Escape(additionPair));
                expression = regex.Replace(expression, ans, 1);
            }
            
            return expression.Split('*')
                .Select(long.Parse)
                .Aggregate((total, item) => total * item);
        }
    }
}

