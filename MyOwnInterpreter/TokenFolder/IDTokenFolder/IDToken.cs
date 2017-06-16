using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterNamespace.TokenFolder.IDTokenFolder
{
    public class IDToken:Token
    {        
        protected string Name;
        public string GetName
        {
            get
            {
                return Name;
            }
        }
        private Dictionary<string, TokenType> Words = new Dictionary<string, TokenType>
        {
            ["IF"] =TokenType.IF,
            ["ELSE"] = TokenType.ELSE,
            ["WHILE"] = TokenType.WHILE,
            ["WRITE"] = TokenType.WRITE_FUNCTION,
            ["READ"] = TokenType.READ_FUNCTION,            
            ["TRUE"] = TokenType.BOOL_TRUE,
            ["FALSE"] = TokenType.BOOL_FALSE,
            ["VAR"] = TokenType.VARIABLE,
        };
        public IDToken(string Name)
        {            
            TokenType type = TokenType.ID;
            this.Name = Name;
            if(Words.ContainsKey(Name.ToUpper()))
                type = Words[Name.ToUpper()];
            tokenType = type;
        }
    }
}
