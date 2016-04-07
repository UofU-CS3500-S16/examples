using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

public class Threads3
{
    public static void Mainx()
    {
        // Fire off the calculation in a separate thread
        Task<int> task = new Task<int>(
            () => PrimeCalculator.FindPrimeN(200000));
        task.Start();

        // Display what the task is up to
        while (true)
        {
            TaskStatus status = task.Status;
            Console.WriteLine(status);
            if (task.IsCompleted)
            {
                break;
            }
            Thread.Sleep(1000);
        }
         
        // Blocks until task completes
        Console.WriteLine("Result: " + task.Result);

        // Verify that task completed
        System.Diagnostics.Trace.Assert(
            task.IsCompleted);

        Console.Read();
    }

}



