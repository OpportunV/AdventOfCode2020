using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day21
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static Dictionary<string, int> _ingredientCounter = new Dictionary<string, int>();
        private static Dictionary<string, HashSet<string>> _allergens = new Dictionary<string, HashSet<string>>();

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
            
            foreach (var line in lines)
            {
                var tmp = line.Split(new[] { "(contains " }, StringSplitOptions.None);
                var ingredients = tmp[0].Trim().Split(' ');
                var tmpAllergens = tmp[1].Replace(")", "").Split(new[] { ", " }, StringSplitOptions.None);

                foreach (var ingredient in ingredients)
                {
                    if (!_ingredientCounter.ContainsKey(ingredient))
                    {
                        _ingredientCounter[ingredient] = 0;
                    }

                    _ingredientCounter[ingredient] += 1;
                }

                foreach (var allergen in tmpAllergens)
                {
                    if (_allergens.ContainsKey(allergen))
                    {
                        _allergens[allergen].IntersectWith(ingredients);
                    }
                    else
                    {
                        _allergens[allergen] = new HashSet<string>(ingredients);
                    }
                }
            }

            var allergens = _allergens.Values.SelectMany(item => item).ToHashSet();

            return _ingredientCounter.Where(pair => !allergens.Contains(pair.Key)).Sum(pair => pair.Value);
        }
        
        public static object Part2()
        {

            while (_allergens.Values.Any(item => item.Count != 1))
            {
                foreach (var allergen in _allergens.Keys.ToList())
                {
                    var possibleIng = _allergens[allergen];
                    if (possibleIng.Count != 1)
                    {
                        continue;
                    }

                    foreach (var curAllergen in _allergens.Keys.ToList())
                    {
                        if (curAllergen == allergen)
                        {
                            continue;
                        }
                        
                        var curIngredients = _allergens[curAllergen];

                        _allergens[curAllergen] =
                            curIngredients.Where(item => item != possibleIng.Single()).ToHashSet();
                    }
                }
            }
            
            return string.Join(",", _allergens.OrderBy(item => item.Key).Select(item => item.Value.Single()));
        }
    }
}

