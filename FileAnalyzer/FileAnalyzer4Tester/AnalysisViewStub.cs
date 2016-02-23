using System;
using FileAnalyzer;

namespace FileAnalyzer4Tester
{
    class AnalysisViewStub : IAnalysisView
    {
        // These two properties record whether a method has been called
        public bool CalledDoClose
        {
            get; private set;
        }

        public bool CalledOpenNew
        {
            get; private set;
        }

        // These four methods cause events to be fired
        public void FireCloseEvent()
        {
            if (CloseEvent != null)
            {
                CloseEvent();
            }
        }

        public void FireCountEvent(string substring)
        {
            if (CountEvent != null)
            {
                CountEvent(substring);
            }
        }

        public void FireFileChosenEvent(string filename)
        {
            if (FileChosenEvent != null)
            {
                FileChosenEvent(filename);
            }
        }

        public void FireNewEvent()
        {
            if (NewEvent != null)
            {
                NewEvent();
            }
        }

        // These six properties implement the interface
        public int CharCount
        {
            set; get;
        }

        public int LineCount
        {
            set; get;
        }

        public string SearchString
        {
            set; get;
        }

        public int SubstringCount
        {
            set; get;
        }

        public int WordCount
        {
            set; get;
        }

        public string Title
        {
            set; get;
        }

        public string Message
        {
            set; get;
        }

        // These four events implement the interface
        public event Action CloseEvent;

        public event Action<string> CountEvent;

        public event Action<string> FileChosenEvent;

        public event Action NewEvent;

        // These two methods implement the interface
        public void DoClose()
        {
            CalledDoClose = true;
        }

        public void OpenNew()
        {
            CalledOpenNew = true;
        }
    }
}
