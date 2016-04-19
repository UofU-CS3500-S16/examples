using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Sockets;
using Chat;
using System.Threading.Tasks;
using System.Text;
using CustomNetworking;
using System.Threading;

namespace ChatServerTester
{
    [TestClass]
    public class ChatTester
    {
        private static System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

        [TestMethod]
        public void SimpleTest()
        {
            // Ceate a chat server listening on port 4000
            new SimpleChatServer(4000);

            // Send this string and make sure we get the right thing back
            BetterTestInstance("Test 1\r\nHello world\r\nThis is a test\r\n");

            // Run 10 tasks that test the server in parallel.

            Task[] tasks = new Task[10];
            for (int i = 0; i < tasks.Length; i++)
            {
                String testString = "Test " + i + "\r\n";
                tasks[i] = Task.Run(() => BetterTestInstance(testString));
            }
            Task.WaitAll(tasks);
        }

        private void SimpleTestInstance(string testString)
        {
            // Open a socket to the server
            TcpClient client = new TcpClient("localhost", 4000);
            Socket socket = client.Client;

            // This is the string we expect to get back
            String expectedString = "Welcome!\r\n" + testString.ToUpper();

            // Convert the string into an array of bytes and send them all out.
            // Note the use of the synchronous Send here.  We can use it because
            // we don't care if the testing thread is blocked for a while.
            byte[] outgoingBuffer = encoding.GetBytes(testString);
            socket.Send(outgoingBuffer);

            // Read bytes from the socket until we have the number we expect.
            // We are using a blocking synchronous Receive here.
            byte[] incomingBuffer = encoding.GetBytes(expectedString);
            Array.Clear(incomingBuffer, 0, incomingBuffer.Length);
            int index = 0;
            while (index < incomingBuffer.Length)
            {
                index += socket.Receive(incomingBuffer, index, incomingBuffer.Length - index, 0);
            }

            // Convert the buffer into a string and make sure it is what was expected
            String result = encoding.GetString(incomingBuffer);
            Assert.AreEqual(expectedString, result);
        }

        private void BetterTestInstance(string testString)
        {
            // Open a socket to the server
            TcpClient client = new TcpClient("localhost", 4000);
            Socket socket = client.Client;

            // This is the string we expect to get back
            String expectedString = "Welcome!\r\n" + testString.ToUpper();

            // Convert the string into an array of bytes and send them all out.
            // Note the use of the synchronous Send here.  We can use it because
            // we don't care if the testing thread is blocked for a while.
            Task t1 = Task.Run(() =>
            {
                byte[] outgoingBuffer = encoding.GetBytes(testString);
                socket.Send(outgoingBuffer);
            });

            // Read bytes from the socket until we have the number we expect.
            // We are using a blocking synchronous Receive here.
            byte[] incomingBuffer = null;
            Task t2 = Task.Run(() =>
            {
                incomingBuffer = encoding.GetBytes(expectedString);
                Array.Clear(incomingBuffer, 0, incomingBuffer.Length);
                int index = 0;
                while (index < incomingBuffer.Length)
                {
                    index += socket.Receive(incomingBuffer, index, incomingBuffer.Length - index, 0);
                }
            });

            // Convert the buffer into a string and make sure it is what was expected
            t1.Wait();
            t2.Wait();
            String result = encoding.GetString(incomingBuffer);
            Assert.AreEqual(expectedString, result);
        }

        [TestMethod]
        public void VolumeTest()
        {
            // Ceate a chat server listening on port 4000
            new SimpleChatServer(4000);

            // Open a socket to the server
            TcpClient client = new TcpClient("localhost", 4000);
            StringSocket ss = new StringSocket(client.Client, encoding);

            // Number of strings to use
            int LENGTH = 1000;

            // Generate LENGTH strings of random length.
            Random rand = new Random();
            string[] lines = new string[LENGTH];
            for (int i = 0; i < LENGTH; i++)
            {
                int length = rand.Next(1000);
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < length; j++)
                {
                    sb.Append(rand.Next(10));
                }
                lines[i] = sb.ToString();
            }

            // Send out the strings in a separate task
            Task t1 = Task.Run(() =>
            {
                foreach (string line in lines)
                {
                    ss.BeginSend(line + "\n", (e, p) => { }, null);
                }
            });

            // Receive the strings, store them in receivedLines
            string[] receivedLines = new string[LENGTH+1];

            // Number of lines we expect to receive
            int count = receivedLines.Length;

            // Used to signal when all lines have been received
            ManualResetEvent mre = new ManualResetEvent(false);

            // Begin receiving process
            int lineNumber = 0;
            ss.BeginReceive((s, e, p) => { receivedLines[0] = s; Dec(ref count, mre); }, null);
            foreach (string line in lines)
            {
                lineNumber++;
                int index = lineNumber;
                ss.BeginReceive((s, e, p) => { receivedLines[index] = s; Dec(ref count, mre); }, null);
            }

            // Wait until all responses are in, then do assertions on the testing thread
            // Doing them on the other threads won't work
            if (!mre.WaitOne(5000))
            {
                // The mre will return false after 5000 milliseconds if it isn't set first.
                // If this happens, not all of the lines were received.  The test should fail.
                Assert.Fail();
            }

            Assert.AreEqual("Welcome!\r", receivedLines[0]);
            for (int i = 0; i < lines.Length; i++)
            {
                Assert.AreEqual(lines[i], receivedLines[i + 1]);
            }
        }

        /// <summary>
        /// Atomically decrements the count, signals when the count reaches zero.
        /// </summary>
        private void Dec (ref int count, ManualResetEvent mre)
        {
            Interlocked.Decrement(ref count);
            if (count <= 0)
            {
                mre.Set();
            }
        }
    }
}
