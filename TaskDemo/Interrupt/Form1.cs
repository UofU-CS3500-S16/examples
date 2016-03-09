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

            // Solve the problem on a separate task, blocking until it is finished.
            // Unfortunately, the UI will be unresponsive while we wait for the task
            // to complete.
            Task<Pair> task = new Task<Pair>(() => LongestSequence(1, highRange, 1));
            task.Start();
            task.Wait();

            // Display the result
            HailStart.Text = task.Result.Start.ToString();
            HailLength.Text = task.Result.Length.ToString();
            StopButton.Enabled = false;
            StartButton.Enabled = true;
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
        public struct Pair
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

