using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Helpers
{
    public static class Helper
    {
        private static readonly List<(int, int)> _directions = new List<(int, int)>
        {
            (-1, -1),
            (-1,  0),
            (-1,  1),
            ( 0, -1),
            ( 0,  1),
            ( 1, -1),
            ( 1,  0),
            ( 1,  1),
        };
        public static string[] GetInput(string path)
        {
            var tmp = File.ReadLines(Path.Combine(path));
            return tmp as string[] ?? tmp.ToArray();
        }
        
        public static IEnumerable<(int, int)> Adjacent((int, int) pos, int width, int height)
        {
            var (x, y) = pos;
            foreach (var (i, j) in _directions)
            {
                
                var curX = x + i;
                if (curX < 0 || height <= curX)
                {
                    continue;
                }
                
                var curY = y + j;
                if (curY < 0 || width <= curY)
                {
                    continue;
                }

                yield return (curX, curY);
            }
        }
        
        public static IEnumerable<(int, int, int)> Adjacent3D((int, int, int) pos)
        {
            var (x, y, z) = pos;
            for (int dx = -1; dx <= 1; dx++)
            {
                var curX = x + dx;
                for (int dy = -1; dy <= 1; dy++)
                {
                    var curY = y + dy;
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        var curZ = z + dz;
                        if (curX == x && curY == y && curZ == z)
                        {
                            continue;
                        }

                        yield return (curX, curY, curZ);
                    }
                }
            }
        }
        
        public static IEnumerable<(int, int, int, int)> Adjacent4D((int, int, int, int) pos)
        {
            var (x, y, z, w) = pos;
            for (int dx = -1; dx <= 1; dx++)
            {
                var curX = x + dx;
                for (int dy = -1; dy <= 1; dy++)
                {
                    var curY = y + dy;
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        var curZ = z + dz;
                        for (int dw = -1; dw <= 1; dw++)
                        {
                            var curW = w + dw;
                            if (curX == x && curY == y && curZ == z && curW == w)
                            {
                                continue;
                            }

                            yield return (curX, curY, curZ, curW);
                        }
                    }
                }
            }
        }
        
        public static int CountOccupiedAdjacentWithDistance((int, int) pos, int width, int height, string[] lines)
        {
            var (x, y) = pos;
            var ans = 0;
            foreach (var (i, j) in _directions)
            {
                var curX = x + i;
                var curY = y + j;
                var xInBounds = curX >= 0 && height > curX;
                var yInBounds = curY >= 0 && width > curY;
                while (xInBounds && yInBounds && lines[curX][curY] == '.')
                {
                    curX += i;
                    curY += j;
                    xInBounds = curX >= 0 && height > curX;
                    yInBounds = curY >= 0 && width > curY;
                }

                if (xInBounds && yInBounds)
                {
                    ans += lines[curX][curY] == '#'
                        ? 1
                        : 0;
                }
            }

            return ans;
        }
    }
    
}