using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterNamespace.TokenFolder.ReservedSymbTokenFolder
{
    class ReservedSymbToken : Token
    {

        public ReservedSymbToken(char res)
        {
            Dictionary<char, TokenType> SingleToken = new Dictionary<char, TokenType>
            {
                ['+'] = TokenType.PLUS,
                ['-'] = TokenType.MINUS,
                ['*'] = TokenType.MUL,
                ['/'] = TokenType.DIV,
                ['('] = TokenType.L_PAR,
                [')'] = TokenType.R_PAR,
                [':'] = TokenType.COLON,
                [';'] = TokenType.SEMI,
                ['.'] = TokenType.DOT,
                [','] = TokenType.COMA,
                ['='] = TokenType.EQUAL,
                ['^'] = TokenType.POW,
                ['|'] = TokenType.DIZ,
                ['&'] = TokenType.CON,
                ['>'] = TokenType.MORE,
                ['<'] = TokenType.LESS,
                ['{'] = TokenType.L_FIGURE,
                ['}'] = TokenType.R_FIGURE,
                ['!'] = TokenType.NOT,
                ['$'] = TokenType.EOF,
            };
            tokenType = SingleToken[res];
        }
        public ReservedSymbToken(string res)
        {
            if (res == "==")
            {
                tokenType = TokenType.EQUAL_BOOL;
            }
            else if(res == "!=")
            {
                tokenType = TokenType.NOT_EQUAL;
            }
            else
                throw new Exception();
        }

    }
}
