using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiling
{
    static class ProfileDemo
    {
        static void Main()
        {
            int size = 10000; 
            Square(size);
            Linear(size);
            Log(size);
        }

        static long Square (int n)
        {
            long sum = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sum += i + j;
                }
            }
            return sum;
        }

        static long Linear(int n)
        {
            long sum = 0;
            for (int i = 0; i < n; i++)
            {
                    sum += i;
            }
            return sum;
        }

        static long Log(int n)
        {
            long sum = 0;
            for (int i = 1; i < n; i*= 2)
            {
                sum += i;
            }
            return sum;
        }
    }
}
