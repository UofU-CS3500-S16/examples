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
            int highRange = Int32.Parse(TopOfRange.Text);
            StartButton.Enabled = false;
            StopButton.Enabled = true;
            HailStart.Text = "";
            HailLength.Text = "";
            Cancel = false;
            Task task = new Task(() => ComputeLongestSequence(1, highRange, 1));
            task.Start();
        }

        /// <summary>
        /// Handles the Click event of the StopButton control.
        /// </summary>
        private void StopButton_Click(object sender, EventArgs e)
        {
            Cancel = true;
        }

        /// <summary>
        /// Computes the longest hailstone sequence in the specified interal and displays
        /// it in the GUI.
        /// </summary>
        public void ComputeLongestSequence(int start, int stop, int delta)
        {
            try
            {
                Pair result = LongestSequence(start, stop, delta);
                HailStart.Text = result.Start.ToString();
                HailLength.Text = result.Length.ToString();
            }
            catch (Exception)
            {
            }

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
                if (Cancel)
                {
                    throw new Exception("Cancelled");
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

