// Written by Joe Zachary for CS 3500, January 2015
// Revised by Joe Zachary, January 28, 2016

using System;
using ExtensionDemo;

namespace LectureExamples
{
    /// <summary>
    /// Demonstrates the Rat class
    /// </summary>
    public class RatDemo
    {
        /// <summary>
        /// Demonstrate the Rat class
        /// </summary>
        /// <param name="args"></param>
        public static void Main(String[] args)
        {
            Rat r1 = new Rat(3, 4);
            Rat r2 = new Rat(5, 6);
            Console.WriteLine(r1 + " + " + r2 + " = " + (r1 + r2));
            Console.WriteLine(r1 + " == " + r2 + " = " + (r1 == r2));
            Console.WriteLine(r1 + " != " + r2 + " = " + (r1 != r2));
            Console.WriteLine("Hash of " + r1 + " = " + r1.GetHashCode());
            Console.WriteLine(r1 + " + " + 7 + " = " + (r1 + 7));
            //ReferenceEquals(r1, r2);
        }
    }

    /// <summary>
    /// Provides rational numbers that can be expressed as ratios
    /// of machine integers.  Rats are immutable.
    /// </summary>
    public struct Rat
    {
        // den > 0, gcd(den, |num|) = 1
        // unless constructed by the default constructor, in which
        //   case num = den = 0

        /// <summary>
        /// The numerator of this rational number
        /// </summary>
        private int num;

        /// <summary>
        /// The denominator of this rational number
        /// </summary>
        private int den;

        /// <summary>
        /// Creates n.  Note how "this(0)" invokes the
        /// 2-argument constructor.
        /// </summary>
        public Rat(int n)
            : this(n, 1)      // "this" the 2-argument constructor
        {
        }

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

        /// <summary>
        /// Returns the sum of r1 and r2.  Note how this method defines 
        /// a new operator.  Also note the use of the checked block.  
        /// Without it, the arithmetic overflows within would be ignored.
        /// </summary>
        /// <exception cref="System.OverflowException">When arithmetic overflow</exception>
        public static Rat operator +(Rat r1, Rat r2)
        {
            r1.normalize();
            r2.normalize();
            checked
            {
                return new Rat(r1.num * r2.den + r1.den * r2.num,
                               r1.den * r2.den);
            }
        }

        private void normalize()
        {
            if (den == 0)
            {
                den = 1;
            }
        }

        // TODO: This is a test
        // Note the use of the override keyword, required if you want to
        // override an inherited method.

        /// <summary>
        /// Returns a standard string representation of a rational number.
        /// Note the use of the override keyword, which is required if you
        /// want to override an inherited method.
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
                return this.num == r.num && this.den == r.den;
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
        /// Returns a hash code for this Rat.  Note that
        /// if you override Equals, you should also override
        /// GetHashCode.  Otherwise hash tables containing
        /// Rats won't work properly.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            normalize();
            return num ^ den;
        }

        /// <summary>
        /// Provides a cast from an int to a Rat
        /// </summary>
        public static implicit operator Rat(int n)
        {
            return new Rat(n);
        }
    }
}
