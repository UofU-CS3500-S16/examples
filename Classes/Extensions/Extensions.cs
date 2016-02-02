using System;

namespace ExtensionDemo
{
    /// <summary>
    /// Provides a GCD extension.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns the GCD of this and b. 
        /// </summary>
        public static int Gcd(this int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (b > 0)
            {
                int temp = a % b;
                a = b;
                b = temp;
            }
            return a;
        }
    }
}
