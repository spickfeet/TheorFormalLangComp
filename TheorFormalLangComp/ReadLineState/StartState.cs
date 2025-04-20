using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheorFormalLangComp.Tokens;
using static System.Net.Mime.MediaTypeNames;

namespace CWCompil.State
{
    public class StartState : IState
    {
        public void Enter(StateMachine sm)
        {
            if(sm.CurrentTokenIndex >= sm.TokensData.Count)
            {
                ErrorNeutralizer(sm);
                return;
            }
            if (sm.TokensData[sm.CurrentTokenIndex].Token == TokenEnum.KeywordConsole)
            {
                sm.CountDel = 0;
                sm.State = new ConsoleState();
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
                            $"Ожидается \"{tokens[i]}\" перед \"{sm.TokensData[sm.CurrentTokenIndex].TokenValue}\""));
                }
            }
        }
        private void ErrorNeutralizer(StateMachine sm)
        {
            if (sm.CurrentTokenIndex >= sm.TokensData.Count)
            {
                sm.IsStopped = true;
                return;
            }
            if (sm.TokensData[sm.CurrentTokenIndex].Token == TokenEnum.Point)
            {
                string[] tokens = { "Console" };
                NeutralizerAddOrChangeError(sm, tokens);
                sm.State = new ConsoleState();
                sm.State.Enter(sm);
                return;
            }
            if (sm.TokensData[sm.CurrentTokenIndex].Token == TokenEnum.KeywordReadLine)
            {
                string[] tokens = { "Console", "." };
                NeutralizerAddOrChangeError(sm, tokens);
                sm.State = new PointState();
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
