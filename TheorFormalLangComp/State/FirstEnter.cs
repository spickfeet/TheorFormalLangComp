using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheorFormalLangComp.State
{
    public class FirstEnter : IMailFinderState
    {
        private string forbiddenChars = " .-!@#$%^&*()=+{}[]|\\:;\"'<>,?/";
        public string GermanNameState => "FE";
        public void Enter(MailFinder mailFinder)
        {
            if (forbiddenChars.Contains(mailFinder.Text[mailFinder.CurrentIndex])) return;
            mailFinder.StartIndex = mailFinder.CurrentIndex;
            mailFinder.State = new LocalPart();
        }
    }
}
