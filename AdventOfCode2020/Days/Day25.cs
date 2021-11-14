using System.IO;
using System.Numerics;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day25
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static int _cardPublicKey;
        private static int _doorPublicKey;
        private const int SubjectNumber = 7;
        private const int Divider = 20201227;

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

            _cardPublicKey = int.Parse(lines[0]);
            _doorPublicKey = int.Parse(lines[1]);

            var doorLoopSize = GetLoopSize(_doorPublicKey);

            var encryptionKey = ApplyLoop(_cardPublicKey, doorLoopSize);

            return encryptionKey;
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

        private static int GetLoopSize(int publicKey)
        {
            var loopSize = 1;

            while (true)
            {
                
                if (ApplyLoop(SubjectNumber, loopSize) == publicKey)
                {
                    return loopSize;
                }

                loopSize += 1;
            }
        }
        
        private static int ApplyLoop(int subjectNumber, int loopSize)
        {
            return (int) BigInteger.ModPow(subjectNumber, loopSize, Divider);
        }
    }
}

