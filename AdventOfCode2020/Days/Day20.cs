using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day20
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static readonly HashSet<Tile> _tiles = new HashSet<Tile>();
        private const int NTiles = 12;

        private static string[] _monster = new[]
        {
            "                  # ",
            "#    ##    ##    ###",
            " #  #  #  #  #  #   "
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

            CreateTiles(lines);

            foreach (var tile in _tiles)
            {
                var possibility = new HashSet<string>();

                for (int i = 0; i < 4; i++)
                {
                    possibility.UnionWith(tile.RotateRight().Sides);
                }

                tile.FlipVertically();
                
                for (int i = 0; i < 4; i++)
                {
                    possibility.UnionWith(tile.RotateRight().Sides);
                }
                
                tile.FlipHorizontally();
                
                for (int i = 0; i < 4; i++)
                {
                    possibility.UnionWith(tile.RotateRight().Sides);
                }
                
                tile.possibleSides = possibility;
                
            }

            return _tiles.Where(tile => tile.MatchesAnyhow(_tiles) == 2).Aggregate(1L, (total, tile) => total * tile.id);
        }
        
        public static object Part2()
        {
            var puzzle = new Tile[NTiles, NTiles];

            for (int i = 0; i < NTiles; i++)
            {
                for (int j = 0; j < NTiles; j++)
                {
                    Tile curTile;
                    if (i == 0 && j == 0)
                    {
                        curTile = _tiles.First(tile => tile.MatchesAnyhow(_tiles) == 2);
                        while (!curTile.MatchesSide(curTile.rightBorder, _tiles) 
                               || !curTile.MatchesSide(curTile.bottomBorder, _tiles))
                        {
                            curTile.RotateRight();
                        }
                    }
                    else if (j == 0)
                    {
                        var prevRightSide = puzzle[j, i - 1].rightBorder;
                        curTile = _tiles.First(tile => !tile.used && tile.possibleSides.Contains(prevRightSide));

                        while (curTile.leftBorder != prevRightSide && curTile.leftBorder.Reversed() != prevRightSide)
                        {
                            curTile.RotateRight();
                        }

                        if (curTile.leftBorder != prevRightSide)
                        {
                            curTile.FlipVertically();
                        }
                    }
                    else
                    {
                        var prevBottomSide = puzzle[j - 1, i].bottomBorder;
                        curTile = _tiles.First(tile => !tile.used && tile.possibleSides.Contains(prevBottomSide));
                        
                        while (curTile.topBorder != prevBottomSide && curTile.topBorder.Reversed() != prevBottomSide)
                        {
                            curTile.RotateRight();
                        }

                        if (curTile.topBorder != prevBottomSide)
                        {
                            curTile.FlipHorizontally();
                        }
                    }

                    puzzle[j, i] = curTile;
                    curTile.used = true;
                }
            }

            var totalBody = new string[8 * NTiles];

            for (int i = 0; i < NTiles; i++)
            {
                for (int j = 0; j < NTiles; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        totalBody[i * 8 + k] += puzzle[i, j].body[k];
                    }
                }
            }

            var image = new Tile { body = totalBody };
            
            // File.WriteAllLines("test.txt", image.RotateRight().FlipHorizontally().body);
            // Console.WriteLine(string.Join("\n", puzzle[0, 0].body) + $"{puzzle[0, 0].bottomBorder}\t{puzzle[0, 1].topBorder}");
            // Console.WriteLine("----------------------------");
            // Console.WriteLine(string.Join("\n", puzzle[0, 1].body));

            var totalWater = image.body.Sum(item => item.Count(c => c == '#'));
            var totalWaterInMonster = _monster.Sum(item => item.Count(c => c == '#'));

            for (int i = 0; i < 8; i++)
            {
                var nMonsters = CountMonsters(image.body);
                if (nMonsters > 0)
                {
                    return totalWater - totalWaterInMonster * nMonsters;
                }

                if (i == 4)
                {
                    image.FlipHorizontally();
                }
                else
                {
                    image.RotateRight();
                }
            }
            
            return totalWater;
        }

        private static int CountMonsters(string[] image)
        {
            var counter = 0;
            for (int i = 0; i < image.Length - _monster.Length; i++)
            {
                for (int j = 0; j < image[i].Length - _monster[0].Length; j++)
                {
                    if (CheckMonster(image, i, j))
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }

        private static bool CheckMonster(string[] image, int x, int y)
        {
            for (int i = 0; i < _monster.Length; i++)
            {
                for (int j = 0; j < _monster[0].Length; j++)
                {
                    if (_monster[i][j] == '#' && image[x + i][y + j] != '#')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        private static void CreateTiles(string[] lines)
        {
            for (int i = 0; i < lines.Length; i += 12)
            {
                var id = int.Parse(lines[i].Split(' ')[1].Trim(':'));
                var topBorder = lines[i + 1];
                var bottomBorder = lines[i + 10];
                var leftBorder = new StringBuilder();
                var rightBorder = new StringBuilder();
                var body = new string[8];

                for (int j = 0; j < 8; j++)
                {
                    body[j] = lines[i + j + 2].Substring(1, 8);
                }

                for (int j = i + 1; j < i + 11; j++)
                {
                    leftBorder.Append(lines[j][0]);
                    rightBorder.Append(lines[j][9]);
                }

                var tile = new Tile
                {
                    id = id,
                    topBorder = topBorder,
                    rightBorder = rightBorder.ToString(),
                    bottomBorder = bottomBorder,
                    leftBorder = leftBorder.ToString(),
                    body = body,
                };

                _tiles.Add(tile);
            }
        }
    }

    public class Tile
    {
        public int id;
        /// <summary>
        /// Left to right
        /// </summary>
        public string topBorder;
        /// <summary>
        /// Top to right
        /// </summary>
        public string rightBorder;
        /// <summary>
        /// Left to right
        /// </summary>
        public string bottomBorder;
        /// <summary>
        /// Top to bottom
        /// </summary>
        public string leftBorder;

        public HashSet<string> possibleSides;
        public string[] body;
        public bool used = false;

        public HashSet<string> Sides => new HashSet<string> { topBorder, leftBorder, rightBorder, bottomBorder }; 

        public Tile RotateRight()
        {
            try
            {
                (topBorder, rightBorder, bottomBorder, leftBorder) =
                    (leftBorder.Reversed(), topBorder, rightBorder.Reversed(), bottomBorder);
            } catch {}

            body = body.Rotated();
            return this;
        }
        
        public Tile FlipVertically()
        {
            try
            {
                (topBorder, bottomBorder) = (bottomBorder, topBorder);
                leftBorder = leftBorder.Reversed();
                rightBorder = rightBorder.Reversed();
            } catch {}

            Array.Reverse(body);
            return this;
        }
        
        public Tile FlipHorizontally()
        {
            try
            {
                (leftBorder, rightBorder) = (rightBorder, leftBorder);
                topBorder = topBorder.Reversed();
                bottomBorder = bottomBorder.Reversed();
            } catch {}

            for (int i = 0; i < body.Length; i++)
            {
                body[i] = body[i].Reversed();
            }
            return this;
        }

        public int MatchesAnyhow(IEnumerable<Tile> tiles)
        {
            return tiles.Count(tile => tile.id != id && tile.possibleSides.Any(side => possibleSides.Contains(side)));
        }
        
        public bool MatchesSide(string curSide, IEnumerable<Tile> tiles)
        {
            return tiles.Any(tile => tile.id != id && tile.possibleSides.Any(side => side == curSide));
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($"{id}\n");
            result.Append($"{topBorder}\n");
            for (int i = 1; i < leftBorder.Length - 1; i++)
            {
                result.Append(string.Join("        ", leftBorder[i], rightBorder[i]));
                result.Append("\n");
            }
            
            result.Append($"{bottomBorder}\n");
            
            return result.ToString();
        }
    }
}

