// Written by Joe Zachary for CS 3500, January 2015

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LectureExamples
{
    /// <summary>
    /// Illustrates the use of out and ref parameters
    /// </summary>
    public static class ParameterDemo
    {
        public static void Main(string[] args)
        {
            // A library method that takes an out parameter.  The actual parameter
            // (value in this case) must be something into which a value can be
            // stored.  It is not required to be initialized, and if it is the
            // initial value is ignored.
            int value;
            if (Int32.TryParse("45", out value))
            {
                Console.WriteLine("Parsed value: " + value);
            }
            else
            {
                Console.WriteLine("Parse failed");
            }

            // A user-defined method with out parameters.  Note that the keyword
            // "out" is required at the point of call.
            double x, y;
            int roots = SolveQuadratic(4, -5, 1, out x, out y);
            if (roots == 0)
            {
                Console.WriteLine("No roots");
            }
            else if (roots == 1)
            {
                Console.WriteLine("One root: " + x);
            }
            else if (roots == 2)
            {
                Console.WriteLine("Two roots: " + x + " " + y);
            }

            // A user-defined method with ref parameters
            int[] A = { 2, 9 };
            Console.WriteLine("Before " + A[0] + " " + A[1]);
            swap(ref A[0], ref A[1]);
            Console.WriteLine(" After " + A[0] + " " + A[1]);
        }

        /// <summary>
        /// Returns the number of roots of the equation ax^2 + bx + c = 0.
        /// Stores the first root (if there is one) into x1, and the second
        /// root (if there is one) into x2.
        /// </summary>
        public static int SolveQuadratic(double a, double b, double c, 
                                            out double x1, out double x2)
        {

            // Out parameters must be given values
            x1 = 0;
            x2 = 0;

            // Solve the equation
            double disc = b * b - 4 * a * c;
            if (disc < 0)
            {
                return 0;
            }
            else if (disc == 0)
            {
                x1 = -b / (2 * a);
                return 1;
            }
            else
            {
                x1 = (-b + Math.Sqrt(disc)) / (2 * a);
                x2 = (-b - Math.Sqrt(disc)) / (2 * a);
                return 2;
            }
        }

        /// <summary>
        /// Swaps the values of n and m
        /// </summary>
        public static void swap(ref int n, ref int m)
        {
            int temp = n;
            n = m;
            m = temp;
        }
    }
}
