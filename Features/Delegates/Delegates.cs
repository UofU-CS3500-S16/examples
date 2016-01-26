// Written by Joe Zachary for CS 3500, January 2015
// Revised by Joe Zachary, January 2016

using System;
using System.Collections.Generic;

namespace LectureExamples
{
    /// <summary>
    /// Examples of how delegates and lambdas work
    /// </summary>
    public class Delegates
    {
        public static void Main(string[] args)
        {
            SortingExamples();
            //FilteringExamples();
        }

        /// <summary>
        /// Shows how to pass delegates to the List Sort method.
        /// </summary>
        public static void SortingExamples()
        {
            // Make a list for demonstration purposes
            List<String> list = new List<String>();
            list.Add("hello");
            list.Add("long string");
            list.Add("a");
            list.Add("another long string");

            // The Sort method can take a delegate as a parameter.  This delegate should
            // take two list elements x and y and return negative (if x is smaller than y),
            // positive (if x is larger than y), and zero (if x and y are equal).

            // This sorts in the usual fashion.  We pass in an existing method.
            list.Sort(String.Compare);
            Console.WriteLine(String.Join(", ", list));

            // This sorts in reverse order.  We pass in a lambda.
            list.Sort((x, y) => -String.Compare(x, y));
            Console.WriteLine(String.Join(", ", list));

            // Another way of doing the same way.
            list.Sort(OppCompare);
            Console.WriteLine(String.Join(", ", list));

            // This sorts by length.  We pass in a lambda.
            list.Sort((x, y) => x.Length - y.Length);
            Console.WriteLine(String.Join(", ", list));
        }

        /// <summary>
        /// Returns the opposite of Compare
        /// </summary>
        static int OppCompare (string x, string y)
        {
            return -String.Compare(x, y);
        }

        /// <summary>
        /// Declaration of a delegate type
        /// </summary>
        public delegate bool Finder(String s);

        /// <summary>
        /// Reports whether s contains more than 5 characters
        /// </summary>
        public static bool LongerThan5(String s)
        {
            return s.Length > 5;
        }

        /// <summary>
        /// Enumerates the elements of the list that satisfy f
        /// </summary>
        public static IEnumerable<String> FilterList(List<String> list, Finder f)
        {
            foreach (String s in list)
            {
                if (f(s))
                {
                    yield return s;
                }
            }
        }

        /// <summary>
        /// This demonstrates more about delegates.
        /// </summary>
        public static void FilteringExamples()
        {
            // Make a list for demonstration purposes
            List<String> list = new List<String>();
            list.Add("hello");
            list.Add("long string");
            list.Add("a");
            list.Add("another long string");

            // We pass in a method that we defined above
            Console.WriteLine(String.Join(", ", FilterList(list, LongerThan5)));

            // We use a method that returns a method
            FilterList(list, MakeLongerThanX(10));

            // We pass in a lambda that requires its parameter to be of length 5
            Console.WriteLine(String.Join(", ", FilterList(list, s => s.Length == 5)));

            // Methods can be stored in variables
            Finder f1 = LongerThan5;
            Console.WriteLine(f1("joe"));

            // Here's another approach to declaring the delegate
            Func<string, bool> f2 = LongerThan5;
            Console.WriteLine(f2("joe"));

            // Lambda expressions (anonymous methods) can be stored in variables.
            Finder f3 = s => s.Length == 3;
            Console.WriteLine(f3("joe"));
        }


        /// <summary>
        /// Returns a method that reports whether a string
        /// has a longer length than x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Finder MakeLongerThanX (int x)
        {
            return s => s.Length > x;
        }



        /// <summary>
        /// Another way to declare the type of a delegate
        /// </summary>
        public static IEnumerable<String> FilterList2(List<String> list, Func<String, bool> f)
        {
            foreach (String s in list)
            {
                if (f(s))
                {
                    yield return s;
                }
            }
        }
    }
}
