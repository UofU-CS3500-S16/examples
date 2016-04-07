using CustomNetworking;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleWebServer
{
    public class WebServer
    {
        public static void Main()
        {
            new WebServer();
            Console.Read();
        }

        private TcpListener server;

        public WebServer()
        {
            server = new TcpListener(IPAddress.Any, 54321);
            server.Start();
            server.BeginAcceptSocket(ConnectionRequested, null);
        }

        private void ConnectionRequested(IAsyncResult ar)
        {
            Socket s = server.EndAcceptSocket(ar);
            new HttpRequest(new StringSocket(s, new UTF8Encoding()));
            server.BeginAcceptSocket(ConnectionRequested, null);
        }
    }

    public class HttpRequest
    {
        private StringSocket socket;
        private int contentLength;

        public HttpRequest(StringSocket socket)
        {
            this.socket = socket;
            this.contentLength = 0;
            socket.BeginReceive(LineReceived, socket);
        }

        private void LineReceived(string s, Exception e, object o)
        {
            Console.WriteLine(s);
            if (s != null)
            {
                if (s.StartsWith("Content-Length:"))
                {
                    contentLength = Int32.Parse(s.Substring(16).Trim());
                }
                if (s == "\r")
                {
                    socket.BeginReceive(ContentReceived, socket, contentLength);
                }
                else
                {
                    socket.BeginReceive(LineReceived, socket);
                }
            }
        }

        private void ContentReceived(string s, Exception e, object o)
        {
            if (s != null)
            {
                Person p = JsonConvert.DeserializeObject<Person>(s);
                Console.WriteLine(p.Name + " " + p.Eyes);

                string result =
                    JsonConvert.SerializeObject(
                            new Person { Name = "June", Eyes = "Blue" },
                            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                socket.BeginSend("HTTP/1.1 200 OK\n", Ignore, null);
                socket.BeginSend("Content-Type: application/json\n", Ignore, null);
                socket.BeginSend("Content-Length: " + result.Length + "\n", Ignore, null);
                socket.BeginSend("\r\n", Ignore, null);
                socket.BeginSend(result, (ex, py) => { socket.Shutdown(); }, null);
            }
        }

        private void Ignore(Exception e, object payload)
        {
        }
    }

    public class Person
    {
        public String Name { get; set; }
        public String Eyes { get; set; }
    }
}
