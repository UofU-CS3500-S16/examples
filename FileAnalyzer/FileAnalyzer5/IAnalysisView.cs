using System;

namespace FileAnalyzer
{
    /// <summary>
    /// Controllable interface of AnalysisWindow
    /// </summary>
    public interface IAnalysisView
    {
        event Action<string> FileChosenEvent;

        event Action<string> CountEvent;

        event Action CloseEvent;

        event Action NewEvent;

        int CharCount { set; }

        int LineCount { set; }

        string SearchString { set; }

        int SubstringCount { set; }

        int WordCount { set; }

        string Title { set; }

        string Message { set; }

        void DoClose();

        void OpenNew();
    }
}
