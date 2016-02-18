using System;
using System.Windows.Forms;

namespace FileAnalyzer
{
    static class Launch
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Get the application context and run one form inside it
            var context = FileAnalysisApplicationContext.GetContext();
            context.RunNew();
            Application.Run(context);
        }
    }  
}

