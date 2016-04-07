using System;
using System.Threading.Tasks;

// Simple example of threads in action

public class Threads2
{

    public static void Mainx()
    {
        new Threads2().demo();
    }

    // Demo method
    public void demo()
    {

        // Create a new task
        Task task = new Task(f);
        symbol = '+';

        // Start the task in its own thread
        task.Start();

        // While the other thead is running, do some output 
        for (int count = 0; count < repetitions; count++)
        {
            if (count == 5000)
            {
                symbol = '@';
            }
            Console.Write('.');
        }

        // Wait until the Task completes
        task.Wait();
        Console.Read();
    }

    // Number of symbols to use
    private const int repetitions = 10000;

    // Symbol that second thread should use
    private char symbol;

    // Member function used to launch thread
    private void f()
    {
        for (int count = 0; count < 10000; count++)
        {
            Console.Write(symbol);
        }
    }

   
}



