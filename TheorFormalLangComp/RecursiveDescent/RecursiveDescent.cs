using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheorFormalLangComp.Tokens;

namespace TheorFormalLangComp.RecursiveDescent
{
    public class RecursiveDescent
    {
        public List<TokenData<TokenTypesMath>> Tokens { get; set; }
        public List<string> Errors { get; set; }
        public List<string> StateHist {  get; set; }
        public int Index { get; set; }
        public RecursiveDescent(List<TokenData<TokenTypesMath>> tokens) 
        {
            Tokens = tokens;
            Errors = new List<string>();
            StateHist = new List<string>();
        }
        public void Start()
        {
            E();
        }
        private void E()
        {
            StateHist.Add("E");
            T();
            A();
        }
        private void T()
        {
            StateHist.Add("T");
            O();
            B();
        }
        private void A()
        {
            StateHist.Add("A");
            if (Index < Tokens.Count)
            {
                if ((Tokens[Index].Token == TokenTypesMath.Plus || Tokens[Index].Token == TokenTypesMath.Minus))
                {
                    StateHist.Add("+|-");
                    Index++;
                    T();
                    A();
                }
            }
        }
        private void O()
        {
            StateHist.Add("O");
            if (Index >= Tokens.Count)
            {
                StateHist.Add("ERROR");
                Errors.Add($"Не хватает переменной или числа в конце\n");
                return;
            }
            if (Tokens[Index].Token == TokenTypesMath.OB)
            {
                StateHist.Add("(");
                Index++;
                E();
                if (Index >= Tokens.Count)
                {
                    StateHist.Add("ERROR");
                    Errors.Add($"Строка: {Tokens[Index - 1].Line} Индекс: {Tokens[Index - 1].LocalIndex + Tokens[Index - 1].TokenValue.Length}. Не хватает ) в конце\n\n");
                    return;
                }
                if (Tokens[Index].Token != TokenTypesMath.CB)
                {
                    StateHist.Add("ERROR");
                    Errors.Add($"Строка: {Tokens[Index].Line} Индекс: {Tokens[Index].LocalIndex}. Ожидается <)> вместо {Tokens[Index].TokenValue}\n");
                }
                else
                {
                    StateHist.Add(")");
                    Index++;
                    return;
                }
            }
            if (Index < Tokens.Count && (Tokens[Index].Token != TokenTypesMath.Num && Tokens[Index].Token != TokenTypesMath.Id))
            {
                StateHist.Add("ERROR");
                Errors.Add($"Строка: {Tokens[Index].Line} Индекс: {Tokens[Index].LocalIndex}. <{Tokens[Index].TokenValue}> Нужен id или num\n");
            }
            Index++;
        }
        private void B()
        {
            StateHist.Add("B");
            if (Index < Tokens.Count && (Tokens[Index].Token == TokenTypesMath.Multiply || Tokens[Index].Token == TokenTypesMath.Div))
            {
                StateHist.Add("*|/");
                Index++;
                O();
                B();
            }
        }
    }
}
