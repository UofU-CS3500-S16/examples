using CustomNetworking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

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
            server.BeginAcceptSocket(ConnectionRequested, null);
            new HttpRequest(new StringSocket(s, new UTF8Encoding()));
        }
    }

    class HttpRequest
    {
        private StringSocket ss;
        private int lineCount;
        private int contentLength;

        public HttpRequest(StringSocket stringSocket)
        {
            this.ss = stringSocket;
            ss.BeginReceive(LineReceived, null);
        }

        private void LineReceived(string s, Exception e, object payload)
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
                    ss.BeginReceive(ContentReceived, null, contentLength);
                }
                else
                {
                    ss.BeginReceive(LineReceived, null);
                }
            }
        }

        private void ContentReceived(string s, Exception e, object payload)
        {
            if (s != null)
            {
                Person p = JsonConvert.DeserializeObject<Person>(s);
                Console.WriteLine(p.Name + " " + p.Eyes);

                // Call service method

                string result =
                    JsonConvert.SerializeObject(
                            new Person { Name = "June", Eyes = "Blue" },
                            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                ss.BeginSend("HTTP/1.1 200 OK\n", Ignore, null);
                ss.BeginSend("Content-Type: application/json\n", Ignore, null);
                ss.BeginSend("Content-Length: " + result.Length + "\n", Ignore, null);
                ss.BeginSend("\r\n", Ignore, null);
                ss.BeginSend(result, (ex, py) => { ss.Shutdown(); }, null);
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