﻿// Written by Joe Zachary for CS 3500, January 2015

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace LectureExamples
{
    /// <summary>
    /// Demonstrates the rational number class
    /// </summary>
    public class RatDemo
    {
        public static void Main(String[] args)
        {
            Rat r1 = new Rat(3, 4);
            Rat r2 = new Rat(5, 6);
            Console.WriteLine(r1 + " + " + r2 + " = " + (r1 + r2));
            Console.WriteLine(r1 + " == " + r2 + " = " + (r1 == r2));
            Console.WriteLine(r1 + " != " + r2 + " = " + (r1 != r2));
            Console.WriteLine("Hash of " + r1 + " = " + r1.GetHashCode());
            Console.WriteLine(r1 + " + " + 7 + " = " + (r1 + 7));
        }
    }

    /// <summary>
    /// Provides rational numbers that can be expressed as ratios
    /// of 32-bit integers.  Rats are immutable.
    /// </summary>
    public struct Rat
    {
        // Abstraction function: 
        // A Rat represents the rational num/den [den != 0]
        // A Rat represents 0 [den == 0]

        // Representation invariant:
        //  Either:
        //   den > 0
        //   gcd(|num|, den) = 1
        //  Or:
        //   num == den == 0
        private int num;
        private int den;

        /// <summary>
        /// Creates n
        /// </summary>
        public Rat(int n)
            : this(n, 1)      // This invokes the 2-argument constructor
        {
        }


        // Note the use of the extension method Gcd below.  It works because
        // the project has a reference to the Extensions project.

        /// <summary>
        /// Creates n/d.
        /// </summary>
        /// <exception cref="System.ArgumentException">If d == 0</exception>
        public Rat(int n, int d)
        {
            if (d == 0)
            {
                throw new ArgumentException("Zero denominator not allowed");
            }
            int g = n.Gcd(d);
            if (d > 0)
            {
                num = n / g;
                den = d / g;
            }
            else
            {
                num = -n / g;
                den = -d / g;
            }
        }


        // Note how this method defines a new operator.  Also note
        // the use of the checked block.  Without it, the
        // arithmetic overflows within would be ignored.

        /// <summary>
        /// Returns the sum of r1 and r2.
        /// </summary>
        /// <exception cref="System.OverflowException">When arithmetic overflow</exception>
        public static Rat operator +(Rat r1, Rat r2)
        {
            checked
            {
                r1.normalize();
                r2.normalize();
                return new Rat(r1.num * r2.den + r1.den * r2.num,
                               r1.den * r2.den);
            }
        }

        private void normalize ()
        {
            if (den == 0)
            {
                den = 1;
            }
        }


        // Note the use of the override keyword, required if you want to
        // override an inherited method.

        /// <summary>
        /// Returns a standard string representation of a rational number
        /// </summary>
        public override string ToString()
        {
            normalize();
            if (den == 1)
            {
                return num.ToString();
            }
            else
            {
                return num + "/" + den;
            }
        }

        /// <summary>
        /// Reports whether this and o are the same rational number.
        /// </summary>
        public override bool Equals(object o)
        {
            if (o is Rat)
            {
                Rat r = (Rat)o;
                r.normalize();
                normalize();

                // Make sure r is non-null and its numerator and denominator
                // the same as those of this.
                return
                    !ReferenceEquals(r, null) &&
                    this.num == r.num &&
                    this.den == r.den;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Tests for equality
        /// </summary>
        public static bool operator ==(Rat r1, Rat r2)
        {
            if (ReferenceEquals(r1, null))
            {
                return ReferenceEquals(r2, null);
            }
            else
            {
                return r1.Equals(r2);
            }
        }

        /// <summary>
        /// Tests for inequality
        /// </summary>
        public static bool operator !=(Rat r1, Rat r2)
        {
            return !(r1 == r2);
        }

        /// <summary>
        /// Returns a hash code for this Rat.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return num ^ den;
        }

        /// <summary>
        /// Casts an int to a Rat
        /// </summary>
        public static implicit operator Rat(int n)
        {
            return new Rat(n);
        }
    }
}
