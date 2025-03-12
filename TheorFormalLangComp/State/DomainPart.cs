using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheorFormalLangComp.State
{
    public class DomainPart : IMailFinderState
    {
        private string forbiddenChars = " ()<>[]:;@\\,\"\t\n\r";
        public void Enter(MailFinder mailFinder)
        {
            if (mailFinder.Text[mailFinder.CurrentIndex] == '.' && mailFinder.CurrentIndex > mailFinder.CurrentDomainStartIndex) 
            {
                mailFinder.CurrentDomainStartIndex = mailFinder.CurrentIndex + 1;
                mailFinder.State = new NextDomain();
            }
            if (forbiddenChars.Contains(mailFinder.Text[mailFinder.CurrentIndex]) 
                || mailFinder.CurrentIndex - mailFinder.CurrentDomainStartIndex >= 64 ||
                mailFinder.CurrentIndex - mailFinder.DomainStartIndex >= 255) 
            {
                mailFinder.State = new FirstEnter();
            }
            return;
        }
    }
}
