// Written by Joe Zachary for CS 3500, February 2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugging
{
    /// <summary>
    /// To help illustrate debugging tools.
    /// </summary>
    static class Debug
    {
        /// <summary>
        /// You should adjust this to invoke the method and constructors in which you are
        /// interested.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Factorial(20);
            Demo d = new Demo();
            d.F(10, d);
            Stepping.G();
            CallStack.F1(42, "Forty two");
        }

        /// <summary>
        /// Returns n!.  If n is negative, throws an ArgumentException.
        /// If n! can't be represented as a long, throws an OverflowException.
        /// </summary>
        static long Factorial(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException();
            }
            checked
            {
                long fact = 1;
                for (int i = 1; i < n; i++)
                {
                    fact = fact * i;
                }
                return fact;
            }
        }
    }


    /// <summary>
    /// Provides a lot of different kinds of values to pay around with.
    /// </summary>
    class Demo
    {
        // A variety of instance variables
        private int height;
        public double Weight { get; private set; }
        private String s;
        private int[] numbers;

        public Demo ()
        {
            height = 72;
            Weight = 150.5;
            s = "Hello world";
            numbers = new int[]{ 1, 2, 3, 4, 5};
        }

        public int F (int x, Demo other)
        {
            int a = 7;
            int b = 8;
            int c = 7;
            return a + b + c + x;
        }
    }

    static class Stepping
    {
        public static void G ()
        {
            int x = 2;
            int y = 3;
            H(F(x* y), Math.Cos(x - y));
        }

        static void H (double a, double b)
        {
            double x = a * a;
            double y = b * b;
        }

        static double F (double x)
        {
            return 3.14159;
        }
    }

    static class CallStack
    {
        public static void F1 (int n1, string s1)
        {
            double d1 = 7;
            F2("hello", d1);
        }

        public static void F2 (string s2, double d2)
        {
            int n2 = s2.Length;
            F3(n2);
        }

        public static void F3 (int n3)
        {
            int[] numbers = { 1, 3, 5, 7, 9 };
        }
    }
}
