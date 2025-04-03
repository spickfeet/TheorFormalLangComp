using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace TheorFormalLangComp.State
{
    public struct MailPosition
    {
        public int line;
        public int startIndex;
        public int endIndex;
        public string mail;
        public MailPosition(int line, int startIndex, int endIndex, string mail)
        {
            this.line = line;
            this.startIndex = startIndex;
            this.endIndex = endIndex;
            this.mail = mail;
        }
    }
    public class MailFinder
    {       
        private int _currentLine;
        private int _startIndex;
        public int StartIndex 
        {
            get {  return _startIndex; }
            set
            {
                _startIndex = value;
            } 
        }
        public IMailFinderState State { get; set; }
        public string Text { get; set; }
        public int CurrentIndex { get; set; }
        public int DomainStartIndex {  get; set; }
        public int CurrentDomainStartIndex { get; set; }
        private List<MailPosition> _lines;
        public List<IMailFinderState> MailFinderStates { get; set; }

        public MailFinder()
        {
            _currentLine = 1;
            _lines = new List<MailPosition>();
            MailFinderStates = new();
            State = new FirstEnter();
        }

        public void SavePos()
        {
            string[] lines = Text.Split('\n');
            int startLineIndex = 0;
            for(int i = 0; i < _currentLine - 1;i++)
            {
                startLineIndex += lines[i].Length + 1;
            }
           
            _lines.Add(new MailPosition(_currentLine, StartIndex - startLineIndex, CurrentIndex - 1 - startLineIndex, Text.Substring(StartIndex,CurrentIndex - StartIndex)));
        }
        public List<MailPosition> Find(string text)
        {
            MailFinderStates.Add(State);
            Text = text;
            for (; CurrentIndex < text.Length; CurrentIndex++)
            {
                State.Enter(this);
                MailFinderStates.Add(State);
                if (text[CurrentIndex] == '\n')
                {
                    _currentLine++;
                }
            }

            if(State is End)
            {
                State.Enter(this);
            }
                
            return _lines;
        }
    }
}
