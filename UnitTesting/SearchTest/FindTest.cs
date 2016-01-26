// Written by Joe Zachary for CS 3500, January 2015.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Search;
using System.Collections.Generic;

namespace SearchTest
{
    /// <summary>
    /// Class for testing the SearchDemo.Find method.
    /// </summary>
    [TestClass]
    public class FindTest
    {
        /// <summary>
        /// An ordered list shared among tests
        /// </summary>
        private List<int> numbers;

        /// <summary>
        /// Sets up for the test by initializing numbers to be
        /// an ordered array.
        /// </summary>
        public FindTest ()
        {
            numbers = new List<int>();
            numbers.Add(1);
            numbers.Add(3);
            numbers.Add(5);
            numbers.Add(7);
            numbers.Add(9);
        }

        /// <summary>
        /// Tests the case where an element is in the list.
        /// </summary>
        [TestMethod]
        public void PresentTest()
        {
            Assert.AreEqual(0, SearchDemo.Find(numbers, 1));
        }

        /// <summary>
        /// Tests the case where an element is not in the list.
        /// </summary>
        [TestMethod]
        public void AbsentTest()
        {
            Assert.AreEqual(-1, SearchDemo.Find(numbers, 0));
        }
    }
}
