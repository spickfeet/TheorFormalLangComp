using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheorFormalLangComp.State
{
    public class DomainCom : IMailFinderState
    {
        private string forbiddenChars = " .-!@#$%^&*()=+{}[]|\\:;\"'<>,?/\t\n\r";
        private int _countEnter = 0;
        public void Enter(MailFinder mailFinder)
        {
            if (_countEnter == 0 && mailFinder.Text[mailFinder.CurrentIndex] == 'o')
            {
                _countEnter++;
                return;
            }
            if (mailFinder.Text[mailFinder.CurrentIndex] == 'm' && _countEnter==1 &&
                (mailFinder.CurrentIndex == mailFinder.Text.Length - 1 || forbiddenChars.Contains(mailFinder.Text[mailFinder.CurrentIndex + 1])))
            {
                mailFinder.State = new End();
                return;
            }
            
            mailFinder.State = new DomainPart();
        }
    }
}
