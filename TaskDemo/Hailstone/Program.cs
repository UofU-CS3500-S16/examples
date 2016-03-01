using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hailstone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int MAX = 5000000;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var sequence = LongestSequence(1, MAX, 1);
            sw.Stop();
            Console.WriteLine("Starts at " + sequence[0] + "; has length " + sequence.Count);
            Console.WriteLine("ELAPSED TIME: " + sw.ElapsedMilliseconds);

            sw.Reset();
            sw.Start();
            var odds = new Task<List<long>>(() => LongestSequence(1, MAX, 2));
            var evens = new Task<List<long>>(() => LongestSequence(2, MAX, 2));

            odds.Start();
            evens.Start();

            odds.Wait();
            evens.Wait();

            var oddSequence = odds.Result;
            var evenSequence = evens.Result;

            sw.Stop();

            if (oddSequence.Count > evenSequence.Count)
            {
                Console.WriteLine("Starts at " + oddSequence[0] + "; has length " + oddSequence.Count);
            }
            else
            {
                Console.WriteLine("Starts at " + evenSequence[0] + "; has length " + evenSequence.Count);
            }


            Console.WriteLine("ELAPSED TIME: " + sw.ElapsedMilliseconds);
        }

        public static List<long> LongestSequence (int start, int stop, int delta)
        {
            var longest = new List<long>();
            for (int n = start; n <= stop; n += delta)
            {
                var sequence = HailstoneSequence(n);
                if (sequence.Count > longest.Count)
                {
                    longest = sequence;
                }
            }
            return longest;
        }

        public static List<long> HailstoneSequence (long n)
        {
            var sequence = new List<long>();
            while (n != 1)
            {
                sequence.Add(n);
                if (n % 2 == 0)
                {
                    n = n / 2;
                }
                else
                {
                    n = 3 * n + 1;
                }
            }
            sequence.Add(1);
            return sequence;
        }
    }
}
