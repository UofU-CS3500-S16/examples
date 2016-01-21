// Written by Joe Zachary for CS 3500

// Just as you import packages in Java, you use namespaces in C#.
using System;
using System.Collections.Generic;

// A namespace organizes classes hierarchically, much as Java packages do.  The key difference
// is that packaged classes are organized into like-named folders.  Namespaces have nothing
// to do with folders.
namespace Hailstone
{
    /// <summary>
    /// Provides hailstone generator.  This project is a class library, and
    /// when it is built it generates a DLL that can be used in other
    /// programs.  To ask Visual Studio to generate an XML file containing
    /// the documentation provided by comments such as this, right click
    /// on the project and go
    ///   Properties > Build > Xml documentation file
    /// </summary>
    public static class Hail
    {
        /// <summary>
        /// Returns a list containing the hailstone sequence beginning at start.
        /// IEnumerable is an interface that is implemented by most of the container
        /// classes in the .NET library.  
        /// </summary>
        public static IEnumerable<int> HailstoneLister(int start)
        {
            // Note that if the type of a variable can be inferred from its
            // initializer, you can declare the variable this way.  The variable
            // still has a type (hover the mouse to see it).  I'm kind of leery
            // about this, but I'm beginning to come around.
            var numbers = new List<int>();
            while (start > 1)
            {
                numbers.Add(start);
                if (start % 2 == 0)
                {
                    start = start / 2;
                }
                else
                {
                    start = 3 * start + 1;
                }
            }

            numbers.Add(1);
            return numbers;
        }

        /// <summary>
        /// Prints the hailstone sequence beginning at start.
        /// </summary>
        public static void HailstonePrinter(int start)
        {
            while (start > 1)
            {
                Console.WriteLine(start);
                if (start % 2 == 0)
                {
                    start = start / 2;
                }
                else
                {
                    start = 3 * start + 1;
                }
            }

            Console.WriteLine(start);
        }

        /// <summary>
        /// Returns an IEnumerable that can generate the hailstone
        /// sequence beginning at n.  The numbers in the sequence
        /// are not generated all at once.  Instead, they are generated
        /// as they are requested by the consumer.
        /// </summary>
        /// <param name="start">Starting point</param>
        public static IEnumerable<int> Hailstone(int start)
        {
            while (start > 1)
            {
                yield return start;
                if (start == 27)
                {
                    break;
                }
                else if (start % 2 == 0)
                {
                    start = start / 2;
                }
                else
                {
                    start = 3 * start + 1;
                }
            }

            yield return start;
        }
    }
}
