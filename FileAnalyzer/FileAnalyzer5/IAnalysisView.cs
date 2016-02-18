using System;

namespace FileAnalyzer
{
    /// <summary>
    /// Delegate for FileChosenEvents,
    /// </summary>
    public delegate void FileChosenHandler(string filename);

    /// <summary>
    /// Delegate for CountEvents.
    /// </summary>
    public delegate void CountHandler(string searchString);

    /// <summary>
    /// Controllable interface of AnalysisWindow
    /// </summary>
    public interface IAnalysisView
    {
        event FileChosenHandler FileChosenEvent;

        event CountHandler CountEvent;

        event Action CloseEvent;

        event Action NewEvent;

        int CharCount { set; }

        int LineCount { set; }

        string SearchString { set; }

        int SubstringCount { set; }

        int WordCount { set; }

        void SetTitle(string filename);

        void ShowMessage(string message);

        void DoClose();

        void OpenNew();
    }
}
