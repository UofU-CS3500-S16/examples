// Written by Joe Zachary for CS 3500, January 2015

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LectureExamples
{
    /// <summary>
    /// Defines a class with two public properties.
    /// When an object is created, it is allocated on the
    /// heap and a reference is stored.
    /// A class is defines a "reference type"
    /// </summary>
    public class Person1
    {
        public Person1 (String name, int age)
        {
            Name = name;
            Age = age;
        }
        public String Name { get; set; }
        public int Age { get; set; }
    }

    /// <summary>
    /// Defines a struct with two public properties.
    /// When a struct is created, it is allocated in place.
    /// There is no reference.
    /// A struct defines a "value type"
    /// </summary>
    public struct Person2
    {
        public Person2 (String name, int age)
        {
            Name = name;
            Age = age;
        }
        public String Name { get; set; }
        public int Age { get; set; }
    }

    /// <summary>
    /// Demonstrates the differences between classes and structs
    /// </summary>
    public class StructDemo
    {
        public static void Main(string[] args)
        {
            // Create an object
            Person1 p1 = new Person1("Jack", 21);

            // Create a struct
            Person2 p2 = new Person2("Jack", 21);

            // There's always a zero-argument constructor for structs
            Person2 x = new Person2();

            // Make both persons one year older
            Older1(p1);
            Older2(p2);

            // Note the difference in the two ages
            Console.WriteLine("Both persons one year older");
            Console.WriteLine("p1.Age = " + p1.Age);
            Console.WriteLine("p2.Age = " + p2.Age);
            Console.ReadLine();

            // Pass 2 by reference and it works out
            Older3(ref p2);
            Console.WriteLine("Successfully aging p2");
            Console.WriteLine("p2.Age = " + p2.Age);
            Console.ReadLine();

            // Create two new persons
            Person1 p3 = new Person1(p1.Name, p1.Age);
            Person2 p4 = new Person2(p2.Name, p2.Age);

            // Structs don't have == operators
            Console.WriteLine("Using the == operator");
            Console.WriteLine("p1 == p3 ? " + (p1 == p3));
            //Console.WriteLine("p2 == p4 ? " + (p2 == p4));
            Console.ReadLine();

            // Equals defaults differently for classes and structs
            Console.WriteLine("Default equality of physically different objects/structs");
            Console.WriteLine("p1.Equals(p3)) ? " + p1.Equals(p3));
            Console.WriteLine("p2.Equals(p4)) ? " + p2.Equals(p4));
            Console.ReadLine();

            // ReferenceEquals is always false for structs
            Console.WriteLine("ReferenceEquals is different for objects/structs");
            Console.WriteLine("ReferenceEquals(p1,p3)) ? " + ReferenceEquals(p1, p3));
            Console.WriteLine("ReferenceEquals(p1,p1)) ? " + ReferenceEquals(p1, p1));
            Console.WriteLine("ReferenceEquals(p2,p4)) ? " + ReferenceEquals(p2, p4));
            Console.WriteLine("ReferenceEquals(p2,p2)) ? " + ReferenceEquals(p2, p2));
        }

        /// <summary>
        /// Increments the age
        /// </summary>
        public static void Older1(Person1 p)
        {
            p.Age++;
        }

        /// <summary>
        /// Increments the page
        /// </summary>
        public static void Older2(Person2 p)
        {
            p.Age++;
        }

        /// <summary>
        /// Increments the age
        /// </summary>
        public static void Older3(ref Person2 p)
        {
            p.Age++;
        }
    }
}
