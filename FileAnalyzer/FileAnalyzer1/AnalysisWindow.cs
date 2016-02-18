using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FileAnalyzer
{
    /// <summary>
    /// Top-level window of file analyzer
    /// </summary>
    public partial class AnalysisWindow : Form
    {
        // Contents of the current file, or the empty string
        // if there's no current file
        private String fileContents;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisWindow"/> class.
        /// </summary>
        public AnalysisWindow()
        {
            InitializeComponent();
            fileContents = "";
        }

        /// <summary>
        /// Handles the Click event of the openItem control.
        /// </summary>
        private void OpenItem_Click(object sender, EventArgs e)
        {
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                try
                {
                    fileContents = File.ReadAllText(fileDialog.FileName);
                    charCount.Text = fileContents.Length.ToString();
                    wordCount.Text = CountWords(fileContents).ToString();
                    lineCount.Text = CountLines(fileContents).ToString();
                    substringCount.Text = "";
                    substringBox.Text = "";
                    Text = fileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to open file\n" + ex.Message);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the closeItem control.
        /// </summary>
        private void CloseItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the Click event of the CountButton control.
        /// </summary>
        private void CountButton_Click(object sender, EventArgs e)
        {
            substringCount.Text = CountSubstrings(substringBox.Text, fileContents).ToString();
        }

        /// <summary>
        /// Returns the number of words in contents.
        /// </summary>
        private int CountWords(string contents)
        {
            StringReader reader = new StringReader(contents);
            int count = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                count += Regex.Split(line, @"\s+").Length;
            }
            return count;
        }

        /// <summary>
        /// Returns the number of lines in contents.
        /// </summary>
        private int CountLines(string contents)
        {
            StringReader reader = new StringReader(contents);
            int count = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                count++;
            }
            return count;
        }

        /// <summary>
        /// Counts the number of times substring occurs in contents.
        /// </summary>
        private int CountSubstrings(string substring, string contents)
        {
            int count = 0;
            int index = -1;
            while (((index < contents.Length) && (index = contents.IndexOf(substring, index + 1)) >= 0))
            {
                count++;
            }
            return count;
        }
    }
}
