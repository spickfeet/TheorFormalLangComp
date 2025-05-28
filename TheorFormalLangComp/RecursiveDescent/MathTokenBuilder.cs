using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TheorFormalLangComp.Tokens;

namespace TheorFormalLangComp.RecursiveDescent
{
    public class MathTokenBuilder
    {
        public static List<TokenData<TokenTypesMath>> CreateTokens(string text)
        {
            Regex regex = new(@"\d+|[a-zA-Zа-яА-ЯёЁ\d]+|\*|\/|\+|\-|\(|\)|[^a-zA-Z0-9а-яА-ЯёЁ]");
            Regex regexArg = new(@"[a-zA-Zа-яА-ЯёЁ\d]+");
            List<TokenData<TokenTypesMath>> tokens = new();
            int tempLineNumber = 1;
            int tempLineOffset = 0;

            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0)
                foreach (Match match in matches)
                {
                    if (int.TryParse(match.Value, out int number))
                    {
                        tokens.Add(new(TokenTypesMath.Num, match.Value, tempLineNumber, tempLineOffset));
                        tempLineOffset += match.Value.Length;
                    }
                    else if (Regex.IsMatch(match.Value, @"[a-zA-Zа-яА-ЯёЁ\d]+"))
                    {
                        tokens.Add(new(TokenTypesMath.Id, match.Value, tempLineNumber, tempLineOffset));
                        tempLineOffset += match.Value.Length;
                    }
                    else if (match.Value == "*")
                    {
                        tokens.Add(new(TokenTypesMath.Multiply, match.Value, tempLineNumber, tempLineOffset));
                        tempLineOffset += match.Value.Length;
                    }
                    else if (match.Value == "/")
                    {
                        tokens.Add(new(TokenTypesMath.Div, match.Value, tempLineNumber, tempLineOffset));
                        tempLineOffset += match.Value.Length;
                    }
                    else if (match.Value == "+")
                    {
                        tokens.Add(new(TokenTypesMath.Plus, match.Value, tempLineNumber, tempLineOffset));
                        tempLineOffset += match.Value.Length;
                    }
                    else if (match.Value == "-")
                    {
                        tokens.Add(new(TokenTypesMath.Minus, match.Value, tempLineNumber, tempLineOffset));
                        tempLineOffset += match.Value.Length;
                    }
                    else if (match.Value == "(")
                    {
                        tokens.Add(new(TokenTypesMath.OB, match.Value, tempLineNumber, tempLineOffset));
                        tempLineOffset += match.Value.Length;
                    }
                    else if (match.Value == ")")
                    {
                        tokens.Add(new(TokenTypesMath.CB, match.Value, tempLineNumber, tempLineOffset));
                        tempLineOffset += match.Value.Length;
                    }
                    else if (match.Value == "\n")
                    {
                        tempLineNumber++;
                        tempLineOffset = 0;
                    }
                    else if (match.Value == "\r")
                    {
                        continue;
                    }
                    else if (string.IsNullOrWhiteSpace(match.Value))
                    {
                        tempLineOffset += match.Value.Length;
                    }
                    else
                    {
                        tokens.Add(new(TokenTypesMath.NonV, match.Value, tempLineNumber, tempLineOffset));
                        tempLineOffset += match.Value.Length;
                    }
                }
            return tokens;

        }
    }
}
