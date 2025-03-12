using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheorFormalLangComp.State
{
    internal class DomainRu : IMailFinderState
    {
        private string forbiddenChars = " .-!@#$%^&*()=+{}[]|\\:;\"'<>,?/\t\n\r";
        public void Enter(MailFinder mailFinder)
        {
            if (mailFinder.Text[mailFinder.CurrentIndex] == 'u' && 
                (mailFinder.CurrentIndex == mailFinder.Text.Length - 1 || forbiddenChars.Contains(mailFinder.Text[mailFinder.CurrentIndex + 1])))
            {
                mailFinder.State = new End();
                return;
            }
            mailFinder.State = new DomainPart();
        }
    }
}
