using CustomNetworking;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleWebServerx
{
    public class WebServer
    {
        public static void Mainx()
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
        private int lineCount;

        public HttpRequest(StringSocket socket)
        {
            this.socket = socket;
            this.contentLength = 0;
            this.lineCount = 0;
            socket.BeginReceive(LineReceived, socket);
        }

        private void LineReceived(string s, Exception e, object o)
        {
            lineCount++;
            Console.WriteLine(s);
            if (s != null)
            {
                if (lineCount == 1)
                {
                    Regex r = new Regex(@"^(\S+)\s+(\S+)");
                    Match m = r.Match(s);
                    Console.WriteLine("Method: " + m.Groups[1].Value);
                    Console.WriteLine("URL: " + m.Groups[2].Value);
                }
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

                socket.BeginSend("HTTP/1.1 200 OK\r\n", Ignore, null);
                socket.BeginSend("Content-Type: application/json\r\n", Ignore, null);
                socket.BeginSend("Content-Length: " + result.Length + "\r\n", Ignore, null);
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
