using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheorFormalLangComp.State
{
    public interface IMailFinderState
    {
        void Enter(MailFinder mailFinder);
        string GermanNameState { get; }
    }
}
