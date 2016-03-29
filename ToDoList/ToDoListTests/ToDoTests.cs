using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rest;
using System.Dynamic;
using static System.Net.HttpStatusCode;
using System.Diagnostics;

namespace UnitTestProject1
{
    /// <summary>
    /// Provides a way to start and stop the IIS web server from within the test
    /// cases.  If something prevents the test cases from stopping the web server,
    /// subsequent tests may not work properly until the stray process is killed
    /// manually.
    /// </summary>
    public static class IISAgent
    {
        // Reference to the running process
        private static Process process = null;

        /// <summary>
        /// Starts IIS
        /// </summary>
        public static void Start(string arguments)
        {
            if (process == null)
            {
                Console.WriteLine(Properties.Resources.IIS_EXECUTABLE);
                ProcessStartInfo info = new ProcessStartInfo(Properties.Resources.IIS_EXECUTABLE, arguments);
                info.WindowStyle = ProcessWindowStyle.Minimized;
                info.UseShellExecute = false;
                process = Process.Start(info);
            }
        }

        /// <summary>
        ///  Stops IIS
        /// </summary>
        public static void Stop()
        {
            if (process != null)
            {
                process.Kill();
            }
        }
    }
    [TestClass]
    public class ToDoTests
    {
        /// <summary>
        /// This is automatically run prior to all the tests to start the server
        /// </summary>
        [ClassInitialize()]
        public static void StartIIS(TestContext testContext)
        {
            IISAgent.Start(@"/site:""ToDoList"" /apppool:""Clr4IntegratedAppPool"" /config:""..\..\..\.vs\config\applicationhost.config""");
        }

        /// <summary>
        /// This is automatically run when all tests have completed to stop the server
        /// </summary>
        [ClassCleanup()]
        public static void StopIIS()
        {
            IISAgent.Stop();
        }

        private RestClient client = new RestClient("http://localhost:50000/");

        [TestMethod]
        public void TestMethod1()
        {
            dynamic user = new ExpandoObject();
            user.Name = "Joe";
            user.Email = "email";
            Response r = client.DoPostAsync(user, "RegisterUser").Result;
            Assert.AreEqual(OK, r.Status);
            Assert.AreEqual(36, r.Data.Length);
            Console.WriteLine(r.Data.ToString());
            Assert.AreEqual(Forbidden, client.DoDeleteAsync("DeleteItem/hello").Result.Status);
        }
    }
}
