using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TaskDemo
{
    class ComputeBound
    {
        /// <summary>
        /// Shows the impact of multi-threading.
        /// </summary>
        static void Main(string[] args)
        {
            int MAX = 5000000;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Pair solution1 = LongestSequence(1, MAX, 1);

            sw.Stop();

            Console.WriteLine("Starting at " + solution1.Start + " has length " + solution1.Length);
            Console.WriteLine(sw.ElapsedMilliseconds + " msec");
            Console.ReadLine();

            sw.Reset();
            sw.Start();

            // Create a pair of tasks to solve different pieces of the problem
            Task<Pair> odds = new Task<Pair>(() => LongestSequence(1, MAX, 2));
            Task<Pair> evens = new Task<Pair>(() => LongestSequence(2, MAX, 2));

            // Start both tasks
            odds.Start();
            evens.Start();

            // Wait for the tasks to be completed
            odds.Wait();
            evens.Wait();

            // Figure out the overall answer
            Pair pair1 = odds.Result;
            Pair pair2 = evens.Result;
            Pair solution2 = (pair1.Length > pair2.Length) ? pair1 : pair2;

            sw.Stop();

            Console.WriteLine("Starting at " + solution2.Start + " has length " + solution2.Length);
            Console.WriteLine(sw.ElapsedMilliseconds + " msec");
            Console.ReadLine();
        }

        /// <summary>
        /// Return the starting point and length of the longest hailstone sequence that starts
        /// in the specified interval.
        /// </summary>
        public static Pair LongestSequence(int start, int stop, int delta)
        {
            int longestLength = 0;
            int longestStart = 0;

            for (int n = start; n <= stop; n += delta)
            {
                int length = HailstoneLength(n);
                if (length > longestLength)
                {
                    longestLength = length;
                    longestStart = n;
                }
            }

            return new Pair(longestLength, longestStart);
        }

        /// <summary>
        /// Returns the length of the hailstone sequence starting with n.
        /// </summary>
        public static int HailstoneLength(long n)
        {
            int length = 1;
            while (n > 1)
            {
                length++;
                if (n % 2 == 0)
                {
                    n = n / 2;
                }
                else
                {
                    n = 3 * n + 1;
                }
            }
            return length;
        }

        /// <summary>
        /// A Length/Start pair
        /// </summary>
        public struct Pair
        {
            public Pair(int length, int start)
            {
                Length = length;
                Start = start;
            }
            public int Length { get; set; }
            public int Start { get; set; }
        }
    }
}
