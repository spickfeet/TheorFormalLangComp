using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheorFormalLangComp.Model; 

namespace TheorFormalLangComp.ViewModel
{
    public class RegularWindowVM : BasicVM
    {

        public string RegularText { get; set; }
        

        public RegularWindowVM(int menuNumb)
        {
            RegularTextClass reg = new(); 

            switch(menuNumb)
            {
                case 1:
                    RegularText = reg.Formule;
                    break;
                case 2:
                    RegularText = reg.Language;
                    break;
                case 3:
                    RegularText = reg.Example;
                    break;
                case 4:
                    RegularText = reg.StateMachine;
                    break;
            }
        }

    }
}
