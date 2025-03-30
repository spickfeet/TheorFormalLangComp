using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheorFormalLangComp.State
{
    internal class End : IMailFinderState
    {
        public string GetNameState
        {
            get
            {
                return "END";
            }
        }
        public void Enter(MailFinder mailFinder)
        {
            mailFinder.SavePos();
            mailFinder.State = new FirstEnter();
        }
    }
}
