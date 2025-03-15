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
        public int globalStartIndex;
        public int globalEndIndex;
        public int line;
        public int startIndex;
        public int endIndex;
        public MailPosition(int line, int startIndex, int endIndex,int globalStartIndex, int globalEndIndex)
        {
            this.line = line;
            this.startIndex = startIndex;
            this.endIndex = endIndex;
            this.globalStartIndex = globalStartIndex;
            this.globalEndIndex = globalEndIndex;
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

        public MailFinder()
        {
            _currentLine = 1;
            _lines = new List<MailPosition>();
            State = new FirstEnter();
        }

        public void SavePos()
        {
            string[] lines = Text.Split('\n');
            int startLineIndex = 0;
            for(int i = 0; i < _currentLine- 1;i++)
            {
                startLineIndex += lines[i].Length + 1;
            }
           
            _lines.Add(new MailPosition(_currentLine, StartIndex - startLineIndex, CurrentIndex - startLineIndex, StartIndex,CurrentIndex));
        }
        public List<MailPosition> Find(string text)
        {
            Text = text;
            for (; CurrentIndex < text.Length; CurrentIndex++)
            {
                State.Enter(this);
                if (text[CurrentIndex] == '\n')
                {
                    _currentLine++;
                }
            }
            if(State is End)
            {
                CurrentIndex -= 1;
                State.Enter(this);
            }
                
            return _lines;
        }
    }
}
