using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Helpers;

namespace AdventOfCode2020.Days
{
    public static class Day22
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static readonly Queue<int> _firstPlayerDeck = new Queue<int>(); 
        private static readonly Queue<int> _secondPlayerDeck = new Queue<int>();

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

            var player1 = true;
            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    player1 = false;
                    i++;
                    continue;
                }

                if (player1)
                {
                    _firstPlayerDeck.Enqueue(int.Parse(line));
                }
                else
                {
                    _secondPlayerDeck.Enqueue(int.Parse(line));
                }
            }

            while (_firstPlayerDeck.Count > 0 && _secondPlayerDeck.Count > 0)
            {
                var first = _firstPlayerDeck.Dequeue();
                var second = _secondPlayerDeck.Dequeue();

                if (first > second)
                {
                    _firstPlayerDeck.Enqueue(first);
                    _firstPlayerDeck.Enqueue(second);
                }
                else
                {
                    _secondPlayerDeck.Enqueue(second);
                    _secondPlayerDeck.Enqueue(first);
                }
            }

            return GetResult();
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
            
            _firstPlayerDeck.Clear();
            _secondPlayerDeck.Clear();

            var player1 = true;
            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    player1 = false;
                    i++;
                    continue;
                }

                if (player1)
                {
                    _firstPlayerDeck.Enqueue(int.Parse(line));
                }
                else
                {
                    _secondPlayerDeck.Enqueue(int.Parse(line));
                }
            }

            PlayRecursiveGame(_firstPlayerDeck, _secondPlayerDeck);
            
            return GetResult();
        }

        private static int PlayRecursiveGame(Queue<int> deckOne, Queue<int> deckTwo)
        {
            var cache = new HashSet<(string, string)>();
            while (deckOne.Count > 0 && deckTwo.Count > 0)
            {
                var item = (string.Join("", deckOne), string.Join("", deckTwo));
                if (cache.Contains(item))
                {
                    return 1;
                }
                
                cache.Add(item);
                
                var first = deckOne.Dequeue();
                var second = deckTwo.Dequeue();

                if (deckOne.Count >= first && deckTwo.Count >= second)
                {
                    if (PlayRecursiveGame(new Queue<int>(deckOne.Take(first)),
                        new Queue<int>(deckTwo.Take(second))) == 1)
                    {
                        deckOne.Enqueue(first);
                        deckOne.Enqueue(second);
                    }
                    else
                    {
                        deckTwo.Enqueue(second);
                        deckTwo.Enqueue(first);
                    }
                }
                else if (first > second)
                {
                    deckOne.Enqueue(first);
                    deckOne.Enqueue(second);
                }
                else
                {
                    deckTwo.Enqueue(second);
                    deckTwo.Enqueue(first);
                }
            }

            return deckOne.Count > 0
                ? 1
                : 2;
        }
        
        private static int GetResult()
        {
            var ans = 0;

            if (_firstPlayerDeck.Count > 0)
            {
                var i = _firstPlayerDeck.Count;
                foreach (var card in _firstPlayerDeck)
                {
                    ans += card * i;
                    i--;
                }
            }
            else
            {
                var i = _secondPlayerDeck.Count;
                foreach (var card in _secondPlayerDeck)
                {
                    ans += card * i;
                    i--;
                }
            }

            return ans;
        }
    }
}

