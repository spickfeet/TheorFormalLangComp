using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheorFormalLangComp.Tokens
{
    public static class TokenConverter
    {
        //public static List<TokenData> CreateTokens(string text)
        //{
        //    Regex regex = new(@"Console|ReadLine|\s|((?!(ReadLine|Console))[^\.();])+|\.|\(|\)|;");
        //    List<TokenData> tokens = new();
        //    int tempLineNumber = 1;
        //    int tempLineOffset = 0;

        //    MatchCollection matches = regex.Matches(text);
        //    if (matches.Count > 0)
        //        foreach (Match match in matches)
        //        {
        //            if (match.Value == "Console")
        //            {
        //                tokens.Add(new(TokenEnum.KeywordConsole, match.Value, tempLineNumber, tempLineOffset));
        //                tempLineOffset += match.Value.Length;
        //            }
        //            else if (match.Value == ".")
        //            {
        //                tokens.Add(new(TokenEnum.Point, match.Value, tempLineNumber, tempLineOffset));
        //                tempLineOffset += match.Value.Length;
        //            }
        //            else if (match.Value == "ReadLine")
        //            {
        //                tokens.Add(new(TokenEnum.KeywordReadLine, match.Value, tempLineNumber, tempLineOffset));
        //                tempLineOffset += match.Value.Length;
        //            }
        //            else if (match.Value == "(")
        //            {
        //                tokens.Add(new(TokenEnum.OpenBracket, match.Value, tempLineNumber, tempLineOffset));
        //                tempLineOffset += match.Value.Length;
        //            }
        //            else if (match.Value == ")")
        //            {
        //                tokens.Add(new(TokenEnum.CloseBracket, match.Value, tempLineNumber, tempLineOffset));
        //                tempLineOffset += match.Value.Length;
        //            }
        //            else if (match.Value == ";")
        //            {
        //                tokens.Add(new(TokenEnum.EndLine, match.Value, tempLineNumber, tempLineOffset));
        //                tempLineOffset += match.Value.Length;
        //            }
        //            else if (match.Value == "\n")
        //            {
        //                tempLineNumber++;
        //                tempLineOffset = 0;
        //            }
        //            else if (match.Value == "\r")
        //            {
        //                continue;
        //            }
        //            else if (string.IsNullOrWhiteSpace(match.Value))
        //            {
        //                tempLineOffset += match.Value.Length;
        //            }
        //            else 
        //            {
        //                tokens.Add(new(TokenEnum.NonValidToken, match.Value.Replace(" ",""), tempLineNumber, tempLineOffset));
        //                tempLineOffset += match.Value.Length;
        //            }
        //        }
        //    return tokens;

        //}
    }
}
