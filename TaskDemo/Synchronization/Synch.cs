using System;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronization
{
    public class Synch
    {
        public static void Main()
        {
            new Synch().Compute(100000000);
        }

        private int Total { get; set; }

        public void Compute (long n)
        {
            Task t1 = new Task(() => Add(n));
            Task t2 = new Task(() => Subtract(n));
            t1.Start();
            t2.Start();

            while (!t1.IsCompleted || !t2.IsCompleted)
            {
                Console.WriteLine(Total);
                Thread.Sleep(500);
            }

            Console.WriteLine("Finished: " + Total);
            Console.ReadLine();
        }

        public void Add (long n)
        {
            while (n > 0)
            {
                Total++;
                n--;
            }
            Console.WriteLine("Add done");
        }

        public void Subtract (long n)
        {
            while (n > 0)
            {
                Total--;
                n--;
            }
            Console.WriteLine("Sub done");
        }
    }
}
