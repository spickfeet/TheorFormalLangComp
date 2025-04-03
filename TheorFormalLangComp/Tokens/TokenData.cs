using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheorFormalLangComp.Tokens
{
    public class TokenData
    {
        public string TokenValue { get; set; }
        public TokenEnum Token { get; set; }
        public int Line { get; set; }
        public int LocalIndex { get; set; }
        public TokenData(TokenEnum token, string tokenValue, int line, int localIndex)
        {
            TokenValue = tokenValue;
            Token = token;
            Line = line;
            LocalIndex = localIndex;
        }
    }
}
