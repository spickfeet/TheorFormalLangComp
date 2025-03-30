using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheorFormalLangComp.State
{
    public class NextDomain : IMailFinderState
    {
        private string forbiddenChars = " ()<>[]:;@\\,\"\t\n\r";
        public string GetNameState => "ND";
        public void Enter(MailFinder mailFinder)
        {
            if (mailFinder.Text[mailFinder.CurrentIndex] == 'r')
            {
                mailFinder.State = new DomainRu();
                return;
            }
            if (mailFinder.Text[mailFinder.CurrentIndex] == 'c')
            {
                mailFinder.State = new DomainCom();
                return;
            }
            if (mailFinder.Text[mailFinder.CurrentIndex] == 'n')
            {
                mailFinder.State = new DomainNet();
                return;
            }
            mailFinder.State = new DomainPart();
        }
    }
}
