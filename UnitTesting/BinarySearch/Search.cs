// Written by Joe Zachary for CS 3500, January 2016.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace UnitTestDemo
{
    /// <summary>
    /// Provides a method for searching sorted arrays
    /// </summary>
    public static class SearchDemo
    {
        /// <summary>
        /// Exercises the Find method.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public static void Main(string[] args)
        {
            var numbers = new List<int>();
            numbers.Add(1);
            numbers.Add(3);
            numbers.Add(5);
            numbers.Add(7);
            numbers.Add(9);
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine(Find(numbers, i));
            }
        }

        /// <summary>
        /// Returns the index of an item within a list, so long as the list is arranged
        /// into ascending order.  (If the list is not ordered, the behavior of this
        /// method is undefined.)  If the item is not in the list, returns -idx-1, 
        /// where idx is the index within the list such that list.Insert(idx, item)
        /// leaves list arranged in ascending order.
        /// </summary>
        public static int Find(List<int> list, int item)
        {
            int lo = 0;
            int hi = list.Count - 1;
            while (lo <= hi)
            {
                int mid = (lo + hi) / 2;
                if (list[mid] == item)
                {
                    return mid;
                }
                else if (list[mid] < item)
                {
                    lo = mid + 1;
                }
                else
                {
                    hi = mid - 1;
                }
            }
            return -(lo + 1);
        }
    }
}
