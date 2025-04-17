using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheorFormalLangComp.RecursiveDescent
{
    public class RecursiveDescent
    {
        public List<> Tokens { get; set; }
        public List<string> Errors { get; set; }
        public List<string> StateHist {  get; set; }
        public int Index { get; set; }
        public RecursiveDescent(List<string> tokens) 
        {
            Tokens = tokens;
            Errors = new List<string>();
            StateHist = new List<string>();
        }
        public void Start()
        {
            Index = 0;
            E();
        }
        private void E()
        {
            T();
            A();
        }
        private void T()
        {
            O();
            B();
        }
        private void A()
        {
            T();
            A();
        }
        private void O()
        {
            if (Tokens[Index] == "(")
            {
                Index++;
                E();
                if (Index < Tokens.Count - 1  && Tokens[Index] != ")" || Index == Tokens.Count - 1)
                {
                    Errors.Add("Нет <)>");
                }
                else
                {
                    Index++;
                }
            }

            Index++;
        }
        private void B()
        {
            O();
            B();
        }
    }
}
