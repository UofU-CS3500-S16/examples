using CustomNetworking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;
using System.Collections.Generic;

namespace GradingTester
{
    [TestClass]
    public class StringSocketTest
    {
        /// <summary>
        /// Opens and returns (with out parameters) a pair of communicating sockets.
        /// </summary>
        private static void OpenSockets(int port, out TcpListener server, out Socket s1, out Socket s2)
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            TcpClient client = new TcpClient("localhost", port);
            s1 = server.AcceptSocket();
            s2 = client.Client;
        }

        /// <summary>
        /// Closes stuff down
        /// </summary>
        private static void CloseSockets(TcpListener server, Socket s1, Socket s2)
        {
            try
            {
                s1.Shutdown(SocketShutdown.Both);
                s1.Close();
            }
            finally
            {
            }
            try
            {
                s2.Shutdown(SocketShutdown.Both);
                s2.Close();
            }
            finally
            {
            }
            try
            {
                server.Stop();
            }
            finally
            {
            }
        }

        /// <summary>
        /// Tests whether StringSocket can receive a line of text
        /// </summary>
        [TestMethod()]
        public void Test1()
        {
            new Test1Class().run(4001);
        }

        public class Test1Class
        {
            public void run(int port)
            {
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                String line = "";
                ManualResetEvent mre = new ManualResetEvent(false);

                try
                {
                    StringSocket sender = new StringSocket(s1, new UTF8Encoding());
                    StringSocket receiver = new StringSocket(s2, new UTF8Encoding());
                    sender.BeginSend("Hello\n", (e, p) => { }, null);
                    receiver.BeginReceive((s, e, p) => { line = s; mre.Set(); }, null);
                    mre.WaitOne();
                    Assert.AreEqual("Hello", line);
                }
                finally
                {
                    CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Like Test1, make sure payload comes through.
        /// </summary>
        [TestMethod()]
        public void Test3()
        {
            new Test3Class().run(4003);
        }

        public class Test3Class
        {
            public void run(int port)
            {
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                object payload = null;
                ManualResetEvent mre = new ManualResetEvent(false);

                try
                {
                    StringSocket sender = new StringSocket(s1, new UTF8Encoding());
                    StringSocket receiver = new StringSocket(s2, new UTF8Encoding());
                    sender.BeginSend("Hello\n", (e, p) => { payload = p; mre.Set(); }, "Payload");
                    mre.WaitOne();
                    Assert.AreEqual("Payload", payload);
                }
                finally
                {
                    CloseSockets(server, s1, s2);
                }
            }
        }

        /// Like Test1, but send one character at a time.
        /// </summary>
        [TestMethod()]
        public void Test5()
        {
            new Test5Class().run(4005);
        }

        public class Test5Class
        {
            public void run(int port)
            {
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                String line = "";
                ManualResetEvent mre = new ManualResetEvent(false);

                try
                {
                    StringSocket sender = new StringSocket(s1, new UTF8Encoding());
                    StringSocket receiver = new StringSocket(s2, new UTF8Encoding());
                    foreach (char c in "Hello\n")
                    {
                        sender.BeginSend(c.ToString(), (e, p) => { }, null);
                    }
                    receiver.BeginReceive((s, e, p) => { line = s; mre.Set(); }, null);
                    mre.WaitOne();
                    Assert.AreEqual("Hello", line);
                }
                finally
                {
                    CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Like Test1, but send a very long string.
        /// </summary>
        [TestMethod()]
        public void Test7()
        {
            new Test7Class().run(4007);
        }

        public class Test7Class
        {
            public void run(int port)
            {
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                String line = "";
                ManualResetEvent mre = new ManualResetEvent(false);

                try
                {
                    StringSocket sender = new StringSocket(s1, new UTF8Encoding());
                    StringSocket receiver = new StringSocket(s2, new UTF8Encoding());
                    StringBuilder text = new StringBuilder();
                    for (int i = 0; i < 100000; i++)
                    {
                        text.Append(i);
                    }
                    String str = text.ToString();
                    text.Append('\n');
                    sender.BeginSend(text.ToString(), (e, p) => { }, null);
                    receiver.BeginReceive((s, e, p) => { line = s; mre.Set(); }, null);
                    mre.WaitOne();
                    Assert.AreEqual(str, line);
                }
                finally
                {
                    CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Send multiple lines, make sure they're received in order.
        /// </summary>
        [TestMethod()]
        public void Test9()
        {
            new Test9Class().run(4009);
        }

        public class Test9Class
        {
            public void run(int port)
            {
                int LIMIT = 1000;
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                String[] lines = new String[LIMIT];
                ManualResetEvent mre = new ManualResetEvent(false);
                int count = 0;

                try
                {
                    StringSocket sender = new StringSocket(s1, new UTF8Encoding());
                    StringSocket receiver = new StringSocket(s2, new UTF8Encoding());
                    for (int i = 0; i < LIMIT; i++)
                    {
                        receiver.BeginReceive((s, e, p) => { lines[(int)p] = s; Interlocked.Increment(ref count); }, i);
                    }
                    for (int i = 0; i < LIMIT; i++)
                    {
                        sender.BeginSend(i.ToString() + "\n", (e, p) => { }, null);
                    }
                    SpinWait.SpinUntil(() => count == LIMIT);
                    for (int i = 0; i < LIMIT; i++)
                    {
                        Assert.AreEqual(i.ToString(), lines[i]);
                    }
                }
                finally
                {
                    CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Like Test9, except the receive calls are made on separate threads.
        /// </summary>
        [TestMethod()]
        public void Test11()
        {
            new Test11Class().run(4011);
        }

        public class Test11Class
        {
            public void run(int port)
            {
                int LIMIT = 1000;
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                List<int> lines = new List<int>();

                try
                {
                    StringSocket sender = new StringSocket(s1, new UTF8Encoding());
                    StringSocket receiver = new StringSocket(s2, new UTF8Encoding());
                    for (int i = 0; i < LIMIT; i++)
                    {
                        ThreadPool.QueueUserWorkItem(x =>
                            receiver.BeginReceive((s, e, p) => { lock (lines) { lines.Add(Int32.Parse(s)); } }, null)
                            );
                    }
                    for (int i = 0; i < LIMIT; i++)
                    {
                        sender.BeginSend(i.ToString() + "\n", (e, p) => { }, null);
                    }
                    SpinWait.SpinUntil(() => { lock (lines) { return lines.Count == LIMIT; } });
                    lines.Sort();
                    for (int i = 0; i < LIMIT; i++)
                    {
                        Assert.AreEqual(i, lines[i]);
                    }
                }
                finally
                {
                    CloseSockets(server, s1, s2);
                }
            }
        }

        /// <summary>
        /// Like Test10, except the send calls are made on separate threads.
        /// </summary>
        [TestMethod()]
        public void Test12()
        {
            new Test12Class().run(4012);
        }

        public class Test12Class
        {
            public void run(int port)
            {
                int LIMIT = 1000;
                Socket s1, s2;
                TcpListener server;
                OpenSockets(port, out server, out s1, out s2);
                List<int> lines = new List<int>();

                try
                {
                    StringSocket sender = new StringSocket(s1, new UTF8Encoding());
                    StringSocket receiver = new StringSocket(s2, new UTF8Encoding());
                    for (int i = 0; i < LIMIT; i++)
                    {
                        receiver.BeginReceive((s, e, p) => { lock (lines) { lines.Add(Int32.Parse(s)); } }, null);
                    }
                    for (int i = 0; i < LIMIT; i++)
                    {
                        String s = i.ToString();
                        ThreadPool.QueueUserWorkItem(x =>
                            sender.BeginSend(s + "\n", (e, p) => { }, null));
                    }
                    SpinWait.SpinUntil(() => { lock (lines) { return lines.Count == LIMIT; } });
                    lines.Sort();
                    for (int i = 0; i < LIMIT; i++)
                    {
                        Assert.AreEqual(i, lines[i]);
                    }
                }
                finally
                {
                    CloseSockets(server, s1, s2);
                }
            }
        }
    }
}
