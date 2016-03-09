using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interrupt
{
    public partial class DemoWindow : Form
    {
        public DemoWindow()
        {
            InitializeComponent();
        }

        // We set this to true when we want to cancel a computation
        private bool Cancel { get; set; }

        /// <summary>
        /// Handles the Click event of the StartButton control.
        /// </summary>
        private void StartButton_Click(object sender, EventArgs e)
        {
            // Configure the UI prior to the calculation 
            int highRange = Int32.Parse(TopOfRange.Text);
            StartButton.Enabled = false;
            StopButton.Enabled = true;
            HailStart.Text = "";
            HailLength.Text = "";

            // Compute the longest sequence.  We are counting on this method returning
            // without blocking.
            Cancel = false;
            ComputeLongestSequence(1, highRange, 1);
        }

        /// <summary>
        /// Handles the Click event of the StopButton control.
        /// </summary>
        private void StopButton_Click(object sender, EventArgs e)
        {
            // Both the GUI event thread and the computation thread have access to the Form object.  Thus, they have
            // shared access to the Cancel property.  By setting this to true, we are asking the computation thread
            // to cancel itself.
            Cancel = true;
        }

        /// <summary>
        /// Computes the longest hailstone sequence in the specified interal and displays
        /// it in the GUI.
        /// </summary>
        public async void ComputeLongestSequence(int start, int stop, int delta)
        {
            // Start a task to compute the sequence and then await the result
            Task<Pair> pair = new Task<Pair>(() => LongestSequence(start, stop, delta));
            pair.Start();

            // An async method (see the method header) can use the await keyword.  When the await is executed, there
            // are two possibilities:
            // (1) The task has already finished.  The method continues along as possible.
            // (2) The task has not finished.  A Task is created that will wait for the computation Task to complete.
            //     When it does, this new Task will execute the remainder of the method body.  The new Task is returned
            //     immediately.  Even better, since the await happened on the GUI event thread, the new Task will be
            //     executed there as well.
            Pair result = await pair;

            if (result != null)
            {
                HailStart.Text = result.Start.ToString();
                HailLength.Text = result.Length.ToString();
            }

            // Renable the GUI
            StopButton.Enabled = false;
            StartButton.Enabled = true;
            Cancel = false;
        }

        /// <summary>
        /// Return the starting point and length of the longest hailstone sequence that starts
        /// in the specified interval.
        /// </summary>
        public Pair LongestSequence(int start, int stop, int delta)
        {
            int longestLength = 0;
            int longestStart = 0;

            for (int n = start; n <= stop; n += delta)
            {
                // Each time through the loop we consider the whether or not to cancel
                if (Cancel)
                {
                    return null;
                }
                int length = HailstoneLength(n);
                if (length > longestLength)
                {
                    longestLength = length;
                    longestStart = n;
                }
            }

            return new Pair(longestLength, longestStart);
        }

        /// <summary>
        /// Returns the length of the hailstone sequence starting with n.
        /// </summary>
        public static int HailstoneLength(long n)
        {
            int length = 1;
            while (n > 1)
            {
                length++;
                if (n % 2 == 0)
                {
                    n = n / 2;
                }
                else
                {
                    n = 3 * n + 1;
                }
            }
            return length;
        }

        /// <summary>
        /// A Length/Start pair
        /// </summary>
        public class Pair
        {
            public Pair(int length, int start)
            {
                Length = length;
                Start = start;
            }
            public int Length { get; set; }
            public int Start { get; set; }
        }
    }
}

