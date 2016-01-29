using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestDemo;

namespace LabTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ConstructorTest1()
        {
            ArrayList list = new ArrayList();
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ConstructorTest2()
        {
            ArrayList list = new ArrayList();
            list.Get(0);
        }

        [TestMethod]
        public void ConstructorTest3()
        {
            ArrayList list = new ArrayList();
            list.AddLast("hello");
            Assert.AreEqual("hello", list.Get(0));
        }
    }
}
