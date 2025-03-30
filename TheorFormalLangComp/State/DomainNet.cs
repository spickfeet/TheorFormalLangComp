using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheorFormalLangComp.State
{
    internal class DomainNet : IMailFinderState
    {
        private string forbiddenChars = " .-!@#$%^&*()=+{}[]|\\:;\"'<>,?/\t\n\r";
        private int _countEnter = 0;
        public string GermanNameState
        {
            get
            {
                _countEnter--;
                if (_countEnter == 0) return "NE";
                if (_countEnter == -1) return "NET";
                return "ERROR_NET";
            }
        }
        public void Enter(MailFinder mailFinder)
        {
            if (_countEnter == 0 && mailFinder.Text[mailFinder.CurrentIndex] == 'e')
            {
                _countEnter++;
                return;
            }
            if (mailFinder.Text[mailFinder.CurrentIndex] == 't' && _countEnter == 1 &&
                (mailFinder.CurrentIndex == mailFinder.Text.Length - 1 || forbiddenChars.Contains(mailFinder.Text[mailFinder.CurrentIndex + 1])))
            {
                mailFinder.State = new End();
                return;
            }

            mailFinder.State = new DomainPart();
        }
    }
}
