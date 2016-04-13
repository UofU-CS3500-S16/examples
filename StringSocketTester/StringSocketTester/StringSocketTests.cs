using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomNetworking;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace UnitTestProject1
{
    [TestClass]
    public class StringSocketTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            TcpListener server = null;
            TcpClient client = null;

            try
            {
                // Create and start a server and a client.
                server = new TcpListener(IPAddress.Any, 55555);
                server.Start();
                client = new TcpClient("localhost", 55555);

                // Obtain the sockets from the two ends of the connection.  We are using the blocking AcceptSocket()
                // method here, which is OK for a test case.
                Socket serverSocket = server.AcceptSocket();
                Socket clientSocket = client.Client;

                // Wrap the two ends of the connection into StringSockets
                StringSocket sendSocket = new StringSocket(serverSocket, new UTF8Encoding());
                StringSocket receiveSocket = new StringSocket(clientSocket, new UTF8Encoding());
            }
            finally
            {
                server.Stop();
                client.Close();
            }
        }
    }
}
