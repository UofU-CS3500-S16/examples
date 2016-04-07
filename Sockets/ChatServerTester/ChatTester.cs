using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Sockets;
using Chat;
using System.Threading.Tasks;
using System.Text;

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
    }
}
