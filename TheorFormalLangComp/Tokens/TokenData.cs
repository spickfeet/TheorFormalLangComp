using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheorFormalLangComp.Tokens
{
    public class TokenData<T>
    {
        public string TokenValue { get; set; }
        public T Token { get; set; }
        public int Line { get; set; }
        public int LocalIndex { get; set; }
        public TokenData(T token, string tokenValue, int line, int localIndex)
        {
            TokenValue = tokenValue;
            Token = token;
            Line = line;
            LocalIndex = localIndex;
        }
    }
}
