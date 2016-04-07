using System;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;
using System.Text;

public class PrimeCalculator
{
    /// <summary>
    /// Find the Nth prime number
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int FindPrimeN(int n)
    {

        int prime = 2;
        int candidate = 3;
        while (n > 1)
        {
            if (Faster_IsPrime(candidate))
            {
                prime = candidate;
                n--;
            }
            candidate += 2;
        }
        return prime;
    }

    /// <summary>
    ///   Returns true if the given number is prime.   p must be positive.
    ///   Warning: this routine is not efficient if you know that p cannot be even
    /// </summary>
    /// <param name="p"> number to test for primeness</param>
    /// <returns>true or false based on primeness</returns>
    public static bool Generic_IsPrime(int p)
    {
        if (p == 2) return true;       // 2 is prime by definition
        if (p % 2 == 0) return false;  // immediately disqualify every even number

        for (int i = 3; i <= Math.Sqrt(p); i+=2)
        {
            if (p % i == 0) return false;
        }
        return true;
    }

    /// <summary>
    ///   Returns true if the given number is prime.   p must be positive and non-even
    /// </summary>
    /// <param name="p"> number to test for primeness. Must be positive and non-even</param>
    /// <returns>true or false based on primeness</returns>
    public static bool Faster_IsPrime(int p)
    {
        for (int i = 3; i <= Math.Sqrt(p); i+=2)
        {
            if (p % i == 0) return false;
        }
        return true;
    }

        
}