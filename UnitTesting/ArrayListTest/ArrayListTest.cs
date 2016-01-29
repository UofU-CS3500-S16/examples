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

        /// <summary>
        /// Tests a one-element array
        /// </summary>
        [TestMethod]
        public void SmallTest()
        {
            ArrayList list = new ArrayList();
            list.AddLast("10");
            Assert.AreEqual("10", list.Get(0));
        }

        [TestMethod]
        public void PrivateTest()
        {
            ArrayList list = new ArrayList();
            list.AddLast("10");
            PrivateObject listAccessor = new PrivateObject(list);
            object[] parameters = { 0, "Joe" };
            listAccessor.Invoke("Set", parameters);
            Assert.AreEqual("Joe", list.Get(0));
        }
    }
}
