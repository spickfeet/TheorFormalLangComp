using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheorFormalLangComp.Tokens;

namespace CWCompil.State
{
    class CloseBracketState : IState
    {
        public void Enter(StateMachine sm)
        {
            if (sm.CurrentTokenIndex >= sm.TokensData.Count)
            {
                ErrorNeutralizer(sm);
                return;
            }
            if (sm.TokensData[sm.CurrentTokenIndex].Token == TokenEnum.EndLine)
            {
                sm.ValidLine += ";\n";
                sm.CountDel = 0;
                sm.State = new StartState();
            }
            else
            {
                ErrorNeutralizer(sm);
            }
        }
        private void NeutralizerAddOrChangeError(StateMachine sm, string[] tokens)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                if (sm.CountDel > tokens.Length)
                {
                    sm.CountDel = tokens.Length;
                }
                sm.ValidLine += tokens[i];
                if (sm.CountDel != 0)
                {
                    sm.ErrorsData[sm.ErrorsData.Count - sm.CountDel].Text = sm.ErrorsData[sm.ErrorsData.Count - sm.CountDel].Text.Replace("Отбрасывается", $"Заменяется на \"{tokens[i]}\"");
                    sm.CountDel--;
                }
                else
                {
                    sm.ErrorsData.Add(new(
                        sm.TokensData[(sm.CurrentTokenIndex < sm.TokensData.Count) ? sm.CurrentTokenIndex : sm.CurrentTokenIndex - 1].Line,
                        sm.TokensData[(sm.CurrentTokenIndex < sm.TokensData.Count) ? sm.CurrentTokenIndex : sm.CurrentTokenIndex - 1].LocalIndex,
                        sm.CurrentTokenIndex < sm.TokensData.Count ? $"Ожидается \"{tokens[i]}\" перед \"{sm.TokensData[sm.CurrentTokenIndex].TokenValue}\"" :
                         $"В конце ожидается \"{tokens[i]}\""));
                }
            }
        }
        private void ErrorNeutralizer(StateMachine sm)
        {
            if (sm.CurrentTokenIndex >= sm.TokensData.Count)
            {
                string[] tokens = { ";" };
                NeutralizerAddOrChangeError(sm, tokens);
                sm.State = new StartState();
                return;
            }
            if (sm.TokensData[sm.CurrentTokenIndex].Token == TokenEnum.KeywordConsole)
            {
                string[] tokens = { ";" };
                NeutralizerAddOrChangeError(sm, tokens);
                sm.State = new StartState();
                sm.State.Enter(sm);
                return;
            }
            if (sm.TokensData[sm.CurrentTokenIndex].Token == TokenEnum.Point)
            {
                string[] tokens = { ";" };
                NeutralizerAddOrChangeError(sm, tokens);
                sm.State = new StartState();
                sm.State.Enter(sm);
                return;
            }
            if (sm.TokensData[sm.CurrentTokenIndex].Token == TokenEnum.KeywordReadLine)
            {
                string[] tokens = { ";" };
                NeutralizerAddOrChangeError(sm, tokens);
                sm.State = new StartState();
                sm.State.Enter(sm);
                return;
            }

            sm.ErrorsData.Add(new(
                sm.TokensData[(sm.CurrentTokenIndex < sm.TokensData.Count) ? sm.CurrentTokenIndex : sm.CurrentTokenIndex - 1].Line,
                sm.TokensData[(sm.CurrentTokenIndex < sm.TokensData.Count) ? sm.CurrentTokenIndex : sm.CurrentTokenIndex - 1].LocalIndex,
                $"\"{sm.TokensData[sm.CurrentTokenIndex].TokenValue}\" не является ожидаемым. (Отбрасывается)"));
            sm.CountDel++;
        }
    }
}
