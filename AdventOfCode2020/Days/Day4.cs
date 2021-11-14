using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day4
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static readonly HashSet<string> _fields = new HashSet<string>
        {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid",
        };
        
        private static readonly HashSet<string> _eyeColors = new HashSet<string>
        {
            "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
        };

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
            
            var passports = GetPassports(lines);

            var counter = passports.Count(passport => _fields.All(passport.Contains));

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

            var passports = GetPassports(lines);

            var passWithFields = passports.Where(passport => _fields.All(passport.Contains)).ToArray();
            
            var counter = 0;
            
            foreach (var passWithField in passWithFields)
            {
                var tmp = passWithField.Trim(' ').Split(' ');
                var flag = true;
                foreach (var pair in tmp)
                {
                    var field = pair.Split(':')[0];
                    var value = pair.Split(':')[1];

                    switch (field)
                    {
                        case "byr":
                            var age = int.Parse(value);
                            if (age > 2002 || age < 1920)
                            {
                                flag = false;
                            }
                            break;
                        case "iyr":
                            var issueYear = int.Parse(value);
                            if (issueYear > 2020 || issueYear < 2010)
                            {
                                flag = false;
                            }
                            break;
                        case "eyr":
                            var expirationYear = int.Parse(value);
                            if (expirationYear > 2030 || expirationYear < 2020)
                            {
                                flag = false;
                            }
                            break;
                        case "hgt":
                            if (!int.TryParse(value.Substring(0, value.Length - 2), out var height))
                            {
                                flag = false;
                                break;
                            }
                            if (value.Contains("cm"))
                            {
                                if (height > 193 || height < 150)
                                {
                                    flag = false;
                                }
                            }
                            else
                            {
                                if (height > 76 || height < 59)
                                {
                                    flag = false;
                                }
                            }
                            break;
                        case "hcl":
                            if (value[0] != '#' || value.Length != 7)
                            {
                                flag = false;
                            }
                            break;
                        case "ecl":
                            if (!_eyeColors.Contains(value))
                            {
                                flag = false;
                            }
                            break;
                        case "pid":
                            if (value.Length != 9 || !int.TryParse(value, out _))
                            {
                                flag = false;
                            }
                            break;
                    }
                }
                
                if (flag)
                {
                    counter++;
                }
            }

            return counter;
        }

        private static List<string> GetPassports(string[] input)
        {
            var passports = new List<string>();

            var curPassport = string.Empty;
            foreach (var str in input)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    passports.Add(curPassport);
                    curPassport = string.Empty;
                    continue;
                }

                curPassport += $" {str}";
            }
            
            passports.Add(curPassport);

            return passports;
        }
    }
}

