using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheorFormalLangComp.State
{
    public class LocalPart : IMailFinderState
    {
        private string forbiddenChars = " +-!№#$%^&?*()<>[]:;@\\,\"\t\n\r";
        public void Enter(MailFinder mailFinder)
        {
            if (mailFinder.Text[mailFinder.CurrentIndex] == '@')
            {
                mailFinder.State = new DomainPart();
                mailFinder.DomainStartIndex = mailFinder.CurrentIndex + 1;
                mailFinder.CurrentDomainStartIndex = mailFinder.CurrentIndex + 1;
                return;
            }
            if (forbiddenChars.Contains(mailFinder.Text[mailFinder.CurrentIndex]) || mailFinder.CurrentIndex - mailFinder.StartIndex >=64)
            {
                mailFinder.State = new FirstEnter();
                return;
            }
            return;
        }
    }
}
