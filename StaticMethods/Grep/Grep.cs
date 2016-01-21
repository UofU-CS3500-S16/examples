// Written by Joe Zachary for CS 3500

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grep
{
    class Grep
    {
        /// <summary>
        /// Reads lines of text from the standard input stream.  Any line that
        /// contains the string provided as the first element of the args
        /// array is printed to standard output.  All other lines are ignored.
        /// If exactly one command line parameter is provided, a diagnostic
        /// is printed to the standard error stream.
        /// 
        /// To run this inside of Eclipse, you first need to set the command
        /// line parameters by right-clicking on the project and going
        /// Properties > Debug > Command line arguments.  
        /// 
        /// To run this from a DOS window, open the window and navigate to 
        /// the directory within the project directory that contains the
        /// Grep.exe executable.  Then try entering something like
        ///   type example.txt | grep the | sort > results.txt
        /// This will find every line in somefile.txt that contains the
        /// word "the", sort the resulting lines, and write everything
        /// to results.txt.
        /// </summary>
        static void Main(string[] args)
        {
            // Make sure we have exactly one parameter.
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Provide one string to search for");
                System.Environment.Exit(1);
            }

            // Finds and prints the lines that contain the pattern
            String pattern = args[0];
            String line;
            while ((line = Console.ReadLine()) != null)
            {
                if (line.Contains(pattern))
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
