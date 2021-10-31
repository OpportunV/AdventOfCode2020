using System.IO;
using System.Linq;

namespace AdventOfCode2020.Helpers
{
    public static class Helper
    {
        public static string[] GetInput(string path)
        {
            var tmp = File.ReadLines(Path.Combine(path));
            return tmp as string[] ?? tmp.ToArray();
        }
    }
    
}