using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileAnalyzer
{
    public class Model
    {
        // The contents of the open file in the AnalysisWindow, or the
        // empty string if no file is open.
        private string contents;

        /// <summary>
        /// Constructs a Model with an empty contents
        /// </summary>
        public Model ()
        {
            contents = "";
        }

        /// <summary>
        /// Makes the contents of the named file the new value of contents
        /// </summary>
        public void ReadFile (string filename)
        {
            contents = File.ReadAllText(filename);
        }

        /// <summary>
        /// Returns the number of chars in contents.
        /// </summary>
        public int CountChars ()
        {
            return contents.Length;
        }

        /// <summary>
        /// Returns the number of words in contents.
        /// </summary>
        public int CountWords()
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
        public int CountLines()
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
        public int CountSubstrings(string substring)
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
