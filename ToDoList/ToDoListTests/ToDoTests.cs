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
            Assert.AreEqual(Created, r.Status);
            Assert.AreEqual(36, r.Data.Length);

            user.Name = "";
            r = client.DoPostAsync(user, "RegisterUser").Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void TestMethod2()
        {
            dynamic user = new ExpandoObject();
            user.Name = "Joe";
            user.Email = "email";
            Response r = client.DoPostAsync(user, "RegisterUser").Result;
            Assert.AreEqual(Created, r.Status);
            string userID = r.Data.ToString();

            dynamic item = new ExpandoObject();
            item.UserID = userID;
            item.Description = "Description 1";
            item.Completed = false;
            r = client.DoPostAsync(item, "AddItem").Result;
            Assert.AreEqual(Created, r.Status);

            item.UserID = null;
            r = client.DoPostAsync(item, "AddItem").Result;
            Assert.AreEqual(Forbidden, r.Status);

            item.UserID = "missing";
            r = client.DoPostAsync(item, "AddItem").Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void TestMethod3()
        {
            dynamic user = new ExpandoObject();
            user.Name = "Joe";
            user.Email = "email";
            Response r = client.DoPostAsync(user, "RegisterUser").Result;
            Assert.AreEqual(Created, r.Status);
            string userID = r.Data.ToString();

            dynamic item = new ExpandoObject();
            item.UserID = userID;
            item.Description = "Description 1";
            item.Completed = false;
            r = client.DoPostAsync(item, "AddItem").Result;
            Assert.AreEqual(Created, r.Status);
            string itemID = r.Data.ToString();

            r = client.DoPutAsync(item, "MarkCompleted/" + itemID).Result;
            Assert.AreEqual(OK, r.Status);

            r = client.DoPutAsync(item, "MarkCompleted/" + "missing").Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void TestMethod4()
        {
            dynamic user = new ExpandoObject();
            user.Name = "Joe";
            user.Email = "email";
            Response r = client.DoPostAsync(user, "RegisterUser").Result;
            Assert.AreEqual(Created, r.Status);
            string userID = r.Data.ToString();

            dynamic item = new ExpandoObject();
            item.UserID = userID;
            item.Description = "Description 1";
            item.Completed = false;
            r = client.DoPostAsync(item, "AddItem").Result;
            Assert.AreEqual(Created, r.Status);
            string itemID = r.Data.ToString();

            r = client.DoDeleteAsync("DeleteItem/" + itemID).Result;
            Assert.AreEqual(OK, r.Status);

            r = client.DoDeleteAsync("DeleteItem/" + itemID).Result;
            Assert.AreEqual(Forbidden, r.Status);

            r = client.DoDeleteAsync("DeleteItem/" + "missing").Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void TestMethod5()
        {
            dynamic user = new ExpandoObject();
            user.Name = "Joe";
            user.Email = "email";
            Response r = client.DoPostAsync(user, "RegisterUser").Result;
            Assert.AreEqual(Created, r.Status);
            string userID = r.Data.ToString();

            dynamic item = new ExpandoObject();
            item.UserID = userID;
            item.Description = "Description 1";
            item.Completed = false;
            r = client.DoPostAsync(item, "AddItem").Result;
            Assert.AreEqual(Created, r.Status);
            string itemID1 = r.Data.ToString();

            item = new ExpandoObject();
            item.UserID = userID;
            item.Description = "Description 2";
            item.Completed = false;
            r = client.DoPostAsync(item, "AddItem").Result;
            Assert.AreEqual(Created, r.Status);
            string itemID2 = r.Data.ToString();

            item = new ExpandoObject();
            item.UserID = userID;
            item.Description = "Description 3";
            item.Completed = false;
            r = client.DoPostAsync(item, "AddItem").Result;
            Assert.AreEqual(Created, r.Status);
            string itemID3 = r.Data.ToString();

            r = client.DoPutAsync(item, "MarkCompleted/" + itemID1).Result;
            Assert.AreEqual(OK, r.Status);

            r = client.DoDeleteAsync("DeleteItem/" + itemID3).Result;
            Assert.AreEqual(OK, r.Status);

            r = client.DoGetAsync("GetAllItems?user=missing").Result;
            Assert.AreEqual(Forbidden, r.Status);

            r = client.DoGetAsync("GetAllItems?user={0}", userID).Result;
            Assert.AreEqual(OK, r.Status);
            Assert.AreEqual(2, r.Data.Count);

            r = client.DoGetAsync("GetAllItems?user={0}&completed=true", userID).Result;
            Assert.AreEqual(OK, r.Status);
            Assert.AreEqual(1, r.Data.Count);
        }
    }
}

