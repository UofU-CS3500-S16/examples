using System;
using System.IO;

namespace IntelliTrace
{
    class IntelliTrace
    {
        static void Main(string[] args)
        {

            FileStream fs = Create();
            ReadByte(fs);
            Close(fs);
            Delete("WordSearchInputs.txt");

            Console.WriteLine("Done");
        }

        static FileStream Create()
        {
            int n = 15;
            return File.Create("WordSearchInputs.txt");
        }

        static void ReadByte (FileStream fs)
        {
            double pi = 3.14159;
            fs.ReadByte();
        }

        static void Close (FileStream fs)
        {
            string s = "hello";
            fs.Close();
        }

        static void Delete (string filename)
        {
            bool b = true;
            File.Delete(filename);
        }
    }
}
