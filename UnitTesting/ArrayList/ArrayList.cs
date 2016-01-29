// Written by Joe Zachary for CS 3500, January 2016

using System;

namespace UnitTestDemo
{
    /// <summary>
    /// A list of strings, indexed beginning at 0.
    /// </summary>
    public class ArrayList
    {
        /// <summary>
        /// The number of elements in this ArrayList
        /// </summary>
        private int size;

        /// <summary>
        /// values[0..size-1] contains the elements of this ArrayList.
        /// There can be up to size unused elements for future use.
        /// </summary>
        private string[] values;

        /// <summary>
        /// Creates an empty ArrayList.
        /// </summary>
        public ArrayList()
        {
            size = 0;
            values = new string[4];
        }

        /// <summary>
        /// Returns the size of this ArrayList.
        /// </summary>
        public int GetSize()
        {
            return size;
        }

        /// <summary>
        /// Adds value to the end of this ArrayList.
        /// </summary>
        public void AddLast(String value)
        {
            if (values.Length == size)
            {
                Array.Resize(ref values, size * 2);
            }
            values[size] = value;
            size++;
        }

        /// <summary>
        /// Returns the element stored at the given index.  If there
        /// is no such index, throws an IndexOutOfRangeException.
        /// </summary>
        public string Get(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException();
            }
            return values[index];
        }

        /// <summary>
        /// Stores value at the given index.  If there 
        /// is no such index, throws an IndexOutOfRangeException.
        /// </summary>
        private void Set(int index, string value)
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException();
            }
            values[index] = value;
        }
    }
}
