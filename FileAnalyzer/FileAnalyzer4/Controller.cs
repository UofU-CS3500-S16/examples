using System;
using System.IO;
using System.Text.RegularExpressions;

namespace FileAnalyzer
{
    /// <summary>
    /// Controls the operation of an IAnalysisView.
    /// </summary>
    public class Controller
    {
        // The window being controlled
        private IAnalysisView window;

        // The contents of the open file in the AnalysisWindow, or the
        // empty string if no file is open.
        private string fileContents = "";

        /// <summary>
        /// Begins controlling window.
        /// </summary>
        public Controller(IAnalysisView window)
        {
            this.window = window;
            window.FileChosenEvent += HandleFileChosen;
            window.CloseEvent += HandleClose;
            window.NewEvent += HandleNew;
            window.CountEvent += HandleCount;
        }

        /// <summary>
        /// Handles a request to open a file.
        /// </summary>
        private void HandleFileChosen(String filename)
        {
            try
            {
                fileContents = File.ReadAllText(filename);
                window.CharCount = fileContents.Length;
                window.WordCount = CountWords(fileContents);
                window.LineCount = CountLines(fileContents);
                window.SubstringCount = 0;
                window.SearchString = "";
                window.Title = filename;
            }
            catch (Exception ex)
            {
                window.Message = "Unable to open file\n" + ex.Message;
            }
        }

        /// <summary>
        /// Handles a request to close the window
        /// </summary>
        private void HandleClose()
        {
            window.DoClose();
        }

        /// <summary>
        /// Handles a request to open a new window.
        /// </summary>
        private void HandleNew()
        {
            window.OpenNew();
        }

        /// <summary>
        /// Handles a request to count occurrences of the search string.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        private void HandleCount(string searchString)
        {
            window.SubstringCount = CountSubstrings(searchString, fileContents);
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
