using System.Windows.Forms;

namespace FileAnalyzer
{
    /// <summary>
    /// Keeps track of how many top-level forms are running, shuts down
    /// the application when there are no more.
    /// </summary>
    class FileAnalysisApplicationContext : ApplicationContext
    {
        // Number of open forms
        private int windowCount = 0;

        // Singleton ApplicationContext
        private static FileAnalysisApplicationContext context;

        /// <summary>
        /// Private constructor for singleton pattern
        /// </summary>
        private FileAnalysisApplicationContext()
        {
        }

        /// <summary>
        /// Returns the one DemoApplicationContext.
        /// </summary>
        public static FileAnalysisApplicationContext GetContext()
        {
            if (context == null)
            {
                context = new FileAnalysisApplicationContext();
            }
            return context;
        }

        /// <summary>
        /// Runs a form in this application context
        /// </summary>
        public void RunNew()
        {
            // Create the window
            AnalysisWindow window = new AnalysisWindow();

            // One more form is running
            windowCount++;

            // When this form closes, we want to find out
            window.FormClosed += (o, e) => { if (--windowCount <= 0) ExitThread(); };

            // Run the form
            window.Show();
        }
    }
}