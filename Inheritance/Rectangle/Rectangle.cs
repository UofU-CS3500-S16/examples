// Written by Joe Zachary for CS 3500, September 2012.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Rectangles
{
    /// <summary>
    /// Represents a rectangle.
    /// </summary>
    public class Rectangle
    {
        private int _width;    // Width of rectangle, must be non-negative
        private int _height;   // Height of rectangle, must be non-negative

        /// <summary>
        /// Creates a rectangle with the specified width and height
        /// </summary>
        public Rectangle(int w, int h)
        {
            Width = w;
            Height = h;
        }

        /// <summary>
        /// Gets/sets the rectangle's width without changing its height
        /// </summary>
        public virtual int Width
        {
            get { return _width; }
            set { if (value < 0) throw new ArgumentException(); else _width = value; }
        }

        /// <summary>
        /// Gets/sets the rectangle's height without changing its width
        /// </summary>
        public virtual int Height
        {
            get { return _height; }
            set { if (value < 0) throw new ArgumentException(); else _height = value; }
        }

        /// <summary>
        /// Returns the area of the rectangle
        /// </summary>
        public int Area()
        {
            return Width * Height;
        }

    }


    /// <summary>
    /// Represents Squares, which are a kind of rectangle
    /// </summary>
    public class Square : Rectangle
    {
        /// <summary>
        /// Constructs a Square with the specified side length
        /// </summary>
        public Square(int s)
            : base(s, s)
        {
        }

        /// <summary>
        /// Changes the side length of Square.
        /// </summary>
        public override int Width
        {
            set { base.Width = value; base.Height = value; }
        }

        /// <summary>
        /// Changes the side length of Square
        /// </summary>
        public override int Height
        {
            set { base.Height = value; base.Width = value; }
        }

    }




    public class Program
    {
        static void Main(string[] args)
        {
            Rectangle r = new Rectangle(2, 5);
            Square s = new Square(5);

            PrintDimensions(r);
            DoubleWidth(r);
            PrintDimensions(r);
            Console.ReadLine();

            PrintDimensions(s);
            DoubleWidth(s);
            PrintDimensions(s);
            Console.ReadLine();
        }

        /// <summary>
        /// Displays the dimensions of the rectangle
        /// </summary>
        private static void PrintDimensions(Rectangle r)
        {
            Console.WriteLine("Width = " + r.Width + ", Height = " + r.Height);
        }

        /// <summary>
        /// Doubles the width of r without changing its height.  According to the
        /// specs of Rectangle, this should work.  But when passed a Square, it
        /// doesn't.  This is because Square violates the Liskov Substitution
        /// Principle in the way it overrides Width and Height.
        /// </summary>
        static void DoubleWidth(Rectangle r)
        {
            r.Width = 2 * r.Width;
        }
    }

}
