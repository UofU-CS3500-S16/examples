// Written by Joe Zachary for CS 3500, January 2015
// Modified by Joe Zachary, January 2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LectureExamples
{
    /// <summary>
    /// Illustrates extension methods
    /// </summary>
    public static class ExtensionDemo
    {
        public static void Main(string[] args)
        {
            // Use an int extension method
            Console.WriteLine("gcd(15,9) = " + 15.Gcd(9));

            // Use a list extension method
            List<int> list = new List<int>();
            list.Add(5);
            list.Add(7);
            Console.WriteLine("A list: " + list.Render());

            // Use another list extension method
            Console.WriteLine(list.ScaleSum(10));
        }


        // An extension method must be static, and the first parameter
        // must be annotated with the "this" keyword.  In the case below,
        // the effect is that GCD appears as a method of the Int32 (int) class.

        /// <summary>
        /// Returns the GCD of this and b. 
        /// </summary>
        public static int Gcd(this int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (b > 0)
            {
                int temp = a % b;
                a = b;
                b = temp;
            }
            return a;
        }

        // This is another extension method, this time to
        // a generic list.  Note the use of type parameters.

        /// <summary>
        /// Renders this as a string.
        /// </summary>
        public static String Render<T>(this List<T> list)
        {
            return "[ " + String.Join(" ", list) + " ]";
        }

        /// <summary>
        /// Returns the product of scale and the sum of all the elements in the list
        /// </summary>
        /// <param name="list"></param>
        public static int ScaleSum (this List<int> list, int scale)
        {
            int sum = 0;
            foreach (int n in list)
            {
                sum += n;
            }
            return scale * sum;
        }
    }
}
