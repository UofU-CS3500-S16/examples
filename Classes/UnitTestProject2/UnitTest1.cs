using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LectureExamples;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Rat r = new Rat();
            Assert.AreEqual("1", r.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RatConstructorTest2()
        {
            Rat r = new Rat(5, 0);
            PrivateObject rat_accessor = new PrivateObject(r);
            Assert.AreEqual(1, rat_accessor.GetField("num"));
        }
    }
}
