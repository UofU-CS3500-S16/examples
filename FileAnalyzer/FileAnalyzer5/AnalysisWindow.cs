using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FileAnalyzer
{
    /// <summary>
    /// Top-level window of file analyzer
    /// </summary>
    public partial class AnalysisWindow : Form, IAnalysisView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisWindow"/> class.
        /// </summary>
        public AnalysisWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fired when a file is chosen with a file dialog.
        /// The parameter is the chosen filename.
        /// </summary>
        public event Action<string> FileChosenEvent;

         /// <summary>
        /// Fired when a close action is requested.
        /// </summary>
        public event Action CloseEvent;

        /// <summary>
        /// Fired when a new action is requested.
        /// </summary>
        public event Action NewEvent;

        /// <summary>
        /// Fired when a request is made to count occurrences of a string.
        /// The parameter is the search string.
        /// </summary>
        public event Action<string> CountEvent;

        /// <summary>
        /// Sets the line count in the UI.
        /// </summary>
        public int LineCount
        {
            set  { lineCount.Text = value.ToString(); }
        }

        /// <summary>
        /// Sets the word count in the UI
        /// </summary>
        public int WordCount
        {
            set { wordCount.Text = value.ToString(); }
        }

        /// <summary>
        /// Sets the character count in the UI.
        /// </summary>
        public int CharCount
        {
            set { charCount.Text = value.ToString(); }
        }

        /// <summary>
        /// Sets the search string in the UI.
        /// </summary>
        public String SearchString
        {
            set { substringBox.Text = value; }
        }

        /// <summary>
        /// Sets the substring count in the UI.
        /// </summary>
        public int SubstringCount
        {
            set { substringCount.Text = value.ToString(); }
        }

        /// <summary>
        /// Sets the title in the UI
        /// </summary>
        public string Title
        {
            set { Text = value; }
        }

        /// <summary>
        /// Shows the message in the UI.
        /// </summary>
        public string Message
        {
            set { MessageBox.Show(value); }
        }

        /// <summary>
        /// Closes this window
        /// </summary>
        public void DoClose()
        {
            Close();
        }

        /// <summary>
        /// Opens a new Analysis window.
        /// </summary>
        public void OpenNew()
        {
            FileAnalysisApplicationContext.GetContext().RunNew();
        }

        /// <summary>
        /// Handles the Click event of the openItem control.
        /// </summary>
        private void OpenItem_Click(object sender, EventArgs e)
        {
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                if (FileChosenEvent != null)
                {
                    FileChosenEvent(fileDialog.FileName);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the closeItem control.
        /// </summary>
        private void CloseItem_Click(object sender, EventArgs e)
        {
            if (CloseEvent != null)
            {
                CloseEvent();
            }
        }

        /// <summary>
        /// Handles the Click event of the newItem control.
        /// </summary>
        private void NewItem_Click(object sender, EventArgs e)
        {
            if (NewEvent != null)
            {
                NewEvent();
            }
        }

        /// <summary>
        /// Handles the Click event of the CountButton control.
        /// </summary>
        private void CountButton_Click(object sender, EventArgs e)
        {
            if (CountEvent != null)
            {
                CountEvent(substringBox.Text);
            }
        }
    }
}
