using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LectureExamples;
using ExtensionDemo;

namespace RatTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Rat r = new Rat(3, 6);
            Assert.AreEqual("1/2", r.ToString());
        }

        [TestMethod]
        public void TestMethod2()
        {
            try
            {
                Rat r = new Rat(1, 0);
                Assert.Fail();
            }
            catch (ArgumentException)
            { 
            }           
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod3()
        {
             Rat r = new Rat(1, 0);
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(3, 15.Gcd(6));
        }

        [TestMethod]
        public void TestMethod5()
        {
            Assert.AreEqual("0", new Rat().ToString());
        }
    }
}
