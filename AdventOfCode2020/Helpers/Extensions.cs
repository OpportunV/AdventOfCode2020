using System;
using System.Linq;

namespace AdventOfCode2020.Helpers
{
    public static class Extensions
    {
        public static string Reversed(this string str)
        {
            var chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        public static string[] Rotated(this string[] array)
        {
            var width = array.Length;
            var height = array[0].Length;
            var src = array.Select(item => item.ToArray()).ToArray();
            
            var dst = new char[height][];
            for (int i = 0; i < height; i++)
            {
                dst[i] = new char[width];
            }

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    var newRow = width - col - 1;
                    var newCol = row;

                    dst[newCol][newRow] = src[col][row];
                }
            }

            var result = new string[height];
            for (int i = 0; i < height; i++)
            {
                result[i] = new string(dst[i]);
            }

            return result;
        }
    }
}