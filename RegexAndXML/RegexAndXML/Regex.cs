using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RegexAndXML
{
    public class RegexExamples
    {
        public static void Main(string[] args)
        {
            // Two ways to create an "identifier" regex
            Regex r1 = new Regex("[a-zA-Z][a-zA-Z0-9]*");
            Regex r2 = new Regex("[a-z][a-z0-9]*", RegexOptions.IgnoreCase);

            // A decimal number regex
            Regex r3 = new Regex(@"(\d+\.\d*) | (\d*\.\d+)", RegexOptions.IgnorePatternWhitespace);

            // A decimal number regex that matches an entire line
            Regex r4 = new Regex("^((\\d*\\.\\d+) | (\\d+\\.\\d*))$", RegexOptions.IgnorePatternWhitespace);

            // A decimal number regex that matches an entire string
            Regex r5 = new Regex("^((\\d*.\\d+) | (\\d+.\\d*))$", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);

            // Does a string contain a match?
            Console.WriteLine("Test 1: " + r3.IsMatch("One 22.5 two 36.7 three .777 four"));

            // Find the first match
            Console.WriteLine("Test 2: " + r3.Match("One 22.5 two 36.7 three .777 four"));

            // Does a string contain a match?
            Console.WriteLine("Test 3: " + r4.IsMatch("One 22.5 two 36.7 three .777 four"));

            // Does a string contain a match?
            Console.WriteLine("Test 4: " + r4.IsMatch("345.678"));

            // Find the first match
            Console.WriteLine("Test 5: " + r4.Match("345.678"));

            // Does a string contain a match?
            Console.WriteLine("Test 6: " + r4.IsMatch("345.678\n67.81"));

            // Does a string contain a match?
            Console.WriteLine("Test 7: " + r5.IsMatch("345.678\n67.81"));

            // Does a string contain a match?
            Console.WriteLine("Test 8: " + r5.Match("345.678\n67.81"));

            // Find all the matches
            Console.Write("Test 9: ");
            foreach (Match m in r3.Matches("One 22.5 two 36.7 three .777 four"))
            {
                Console.Write(m + " ");
            }
            Console.WriteLine();

            // Find all the matches
            Console.Write("Test 10: ");
            foreach (Match m in r5.Matches("345.678\n67.81"))
            {
                Console.Write(m + " ");
            }
            Console.WriteLine();

            // Split the string at the tokens
            Console.Write("Test 11: ");
            foreach (String s in r3.Split("One 22.5 two 36.7 three .777 four"))
            {
                Console.Write(s + " ");
            }
            Console.WriteLine();

            // Replace all the matches with #
            Console.WriteLine("Test 12: " + r3.Replace("One 22.5 two 36.7 three .777 four", "#"));

            // Capitalize the first letter of each match
            Console.WriteLine("Test 13: " + r1.Replace("x23 15 hello", m => m.ToString().ToUpper()));

            // Remove the first letter of each match
            Regex r6 = new Regex("[a-z]([a-z0-9]*)", RegexOptions.IgnoreCase);
            Console.WriteLine("Test 14: " + r6.Replace("x23 15 hello", "$1"));

            // Display all but the first letter of each match
            Console.Write("Test 15: ");
            foreach (Match m in r6.Matches("x23 15 hello"))
            {
                Console.Write(m.Groups[1] + " ");
            }
            Console.WriteLine();
        }
    }
}
