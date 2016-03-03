using System;
using System.Windows.Forms;

namespace Interrupt
{
    static class Interrupt
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DemoWindow());
        }
    }
}
