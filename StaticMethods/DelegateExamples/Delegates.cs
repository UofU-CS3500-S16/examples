using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegateExamples
{
    /// <summary>
    /// Illustrates how delegates (method parameters) work in C#.
    /// </summary>
    static class Delegates
    {
        static void Main()
        {
            // We create a list of strings to experiment with
            List<String> list = new List<String>();
            list.Add("17");
            list.Add("22");
            list.Add("47");
            list.Add("3");
            list.Add("15");
            list.Add("28");
            list.Add("19");
            list.Add("231");

            // Alphabetical order
            list.Sort();
            Console.WriteLine("Alphabetical order");
            Print(list);

            // Numerical order
            list.Sort(Compare);
            Console.WriteLine("Numerical order");
            Print(list);

            // Numerical order using lambda expression
            list.Sort((x, y) => Convert.ToInt32(x).CompareTo(Convert.ToInt32(y)));
            Console.WriteLine("Numerical order using lambda");
            Print(list);

            // Order by length of strings
            list.Sort((x, y) => x.Length.CompareTo(y.Length));
            Console.WriteLine("String length order");
            Print(list);

            // Create a list of numbers
            List<int> numbers = new List<int>();
            numbers.Add(3);
            numbers.Add(4);
            numbers.Add(1);
            numbers.Add(2);

            // Square them all
            Map(numbers, n => n * n);
            Console.WriteLine(numbers);

            // Create a list of strings.
            List<string> strings = new List<string>(new string[] { "this", "is", "a", "test" });
            List<int> lengths = MapStoI(strings, s => s.Length);

            // Add up all the numbers
            Console.WriteLine(Fold(numbers, Math.Max, int.MinValue));
        }

        /// <summary>
        /// Compares two strings according to the integers they represent
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        static int Compare(string x, string y)
        {
            return Convert.ToInt32(x).CompareTo(Convert.ToInt32(y));
        }

        /// <summary>
        /// Replaces each element of list with the result of applying
        /// the method to it.
        /// </summary>
        static void Map (List<int> list, Func<int,int> method)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = method(list[i]);
            }
        }

        /// <summary>
        /// Applies the method to each element of list and returns a list
        /// of the results.
        /// </summary>
        static List<int> MapStoI (List<string> list, Func<string,int> method)
        {
            List<int> result = new List<int>();
            foreach (string s in list)
            {
                result.Add(method(s));
            }
            return result;
        }

        /// <summary>
        /// Returns the result of combining two ints.
        /// This is another way of specifying the type of method
        /// that is needed as a parameter.  See Fold below for
        /// its use.
        /// </summary>
        delegate int Combine(int n, int m);

        /// <summary>
        /// Combines the elements of a list by applying a binary function f to the elements
        /// from left to right, using the provided identity element.  For example, if he
        /// list is [1,2,3,4], the method computes f(f(f(f(identity,1),2),3),4).
        /// </summary>
        static int Fold (List<int> list, Combine f, int identity)
        {
            int result = identity;
            foreach (int n in list)
            {
                result = f(result, n);
            }
            return result;
        }

        /// <summary>
        /// Prints out a list for debbugging purposes
        /// </summary>
        static void Print (List<String> list)
        {
            foreach (String s in list)
            {
                Console.Write(s + " ");
            }
            Console.WriteLine("\n");
        }
    }
}
