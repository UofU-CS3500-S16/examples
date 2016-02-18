using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Events
{
    /// <summary>
    /// Illustration of events
    /// </summary>
    public class Events
    {
        public static void Main(string[] args)
        {
            // Create an EventProvider
            EventProvider provider = new EventProvider();

            // Register three CommunicationEvent handlers.  Each takes a string
            // as a parameter and returns nothing.
            provider.CommunicationEvent += Console.WriteLine;
            provider.CommunicationEvent += capitalize;
            provider.CommunicationEvent += s => Console.WriteLine(s.ToLower());
            provider.CommunicationEvent += capitalize;
            provider.CommunicationEvent -= capitalize;

            // Ask that a CommunicationEvent be fired
            provider.FireCommunicationEvent("HeLlO tHeRe");

            // Delay so we can see the results
            Console.ReadLine();
        }

        private static void capitalize(string s)
        {
            Console.WriteLine(s.ToUpper());
        }
    }

    /// <summary>
    /// A class that allows event listeners to register for a CommunicationEvent
    /// </summary>
    public class EventProvider
    {
        /// <summary>
        /// The type of a CommunicationEvent handler
        /// </summary>
        public delegate void Communicator(string s);

        /// <summary>
        /// Used to register handles for CommunicationEvents
        /// </summary>
        public event Communicator CommunicationEvent;

        /// <summary>
        /// Fires a communication event.  By "calling" CommunicationEvent, all
        /// the registered handlers are called.
        /// </summary>
        public void FireCommunicationEvent(string s)
        {
            if (CommunicationEvent != null)
            {
                CommunicationEvent(s);
            }
        }
    }
}
