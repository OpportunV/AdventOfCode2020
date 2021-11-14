using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day24
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");
        
        private static Dictionary<(int, int), bool> _tiles = new Dictionary<(int, int), bool>();
        private const int PartTwoDaysAmount = 100;

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
                var tile = new HexTile();
                tile.Move(line);
                if (!_tiles.ContainsKey(tile.Pos))
                {
                    _tiles[tile.Pos] = false;
                }
                _tiles[tile.Pos] = !_tiles[tile.Pos];
            }

            return _tiles.Count(item => item.Value);
        }
        
        public static object Part2()
        {
            for (int turn = 0; turn < PartTwoDaysAmount; turn++)
            {
                var newTiles = new Dictionary<(int, int), bool>(_tiles);
                var tilesToCheck = new HashSet<(int, int)>(_tiles.Keys);

                foreach (var pos in _tiles.Where(pair => pair.Value).Select(pair => pair.Key))
                {
                    tilesToCheck.UnionWith(HexTile.Adjacent(pos));
                }
                
                foreach (var curPos in tilesToCheck)
                {
                    var blackCounter = HexTile.Adjacent(curPos)
                        .Count(pos => _tiles.ContainsKey(pos) && _tiles[pos]);

                    if (_tiles.ContainsKey(curPos) && _tiles[curPos])
                    {
                        if (blackCounter == 0 || blackCounter > 2)
                        {
                            newTiles[curPos] = false;
                        }
                    }
                    else if (blackCounter == 2)
                    {
                        newTiles[curPos] = true;
                    }
                    else
                    {
                        newTiles[curPos] = false;
                    }
                }

                _tiles = newTiles;
            }
            
            return _tiles.Count(item => item.Value);
        }
    }

    internal class HexTile
    {
        private int X { get; set; }
        private int Y { get; set; }
        public (int, int) Pos => (X, Y);
        
        public HexTile()
        {
            X = 0;
            Y = 0;
        }

        public static IEnumerable<(int, int)> Adjacent((int, int) pos)
        {
            var (x, y) = pos;
            yield return (x + 2, y);
            yield return (x + 1, y - 1);
            yield return (x - 1, y - 1);
            yield return (x - 2, y);
            yield return (x - 1, y + 1);
            yield return (x + 1, y + 1);
        }

        public void Move(string directions)
        {
            while (!string.IsNullOrEmpty(directions))
            {
                if (directions.StartsWith("e"))
                {
                    X += 2;
                    directions = directions.Substring(1);
                }
                else if (directions.StartsWith("se"))
                {
                    X++;
                    Y--;
                    directions = directions.Substring(2);
                }
                else if (directions.StartsWith("sw"))
                {
                    X--;
                    Y--;
                    directions = directions.Substring(2);
                }
                else if (directions.StartsWith("w"))
                {
                    X -= 2;
                    directions = directions.Substring(1);
                }
                else if (directions.StartsWith("nw"))
                {
                    X--;
                    Y++;
                    directions = directions.Substring(2);
                }
                else if (directions.StartsWith("ne"))
                {
                    X++;
                    Y++;
                    directions = directions.Substring(2);
                }
            }
        }
    }
}

