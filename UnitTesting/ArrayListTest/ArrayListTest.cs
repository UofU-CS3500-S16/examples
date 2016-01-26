// Written by Joe Zachary for CS 3500, January 2016

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestDemo;

namespace ArrayListTest
{
    /// <summary>
    /// Tests for the ArrayList class
    /// </summary>
    [TestClass]
    public class ArrayListTest
    {
        /// <summary>
        /// Tries to index into an empty ArrayList.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void EmptyTest()
        {
            ArrayList list = new ArrayList();
            list.Get(-1);
        }
    }
}
