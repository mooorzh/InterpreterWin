using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterNamespace.TokenFolder
{
    public enum TokenType
    {
        INTEGER,
        DOUBLE,
        STRING,
        CHAR,
        BOOL,
        ID,
        EOF,
        SEMI,
        PLUS,
        ASSIGN,
        EQUAL_BOOL,        
        TYPE_BOOL,
        TYPE_DBL,
        TYPE_INT,
        TYPE_STRING,
        TYPE_CHAR,
        WRITE_FUNCTION,
        READ_FUNCTION,
        IF,
        WHILE,        
        COMA,
        EQUAL,
        DIZ,
        CON,
        L_PAR,
        R_PAR,
        LESS,
        MORE,
        MINUS,
        MUL,
        DIV,
        POW,
        COLON,
        DOT,
        L_FIGURE,
        QUOTE,
        R_FIGURE,
        D_QUOTE,
        ELSE,
        NOT,
        VALUE,
        NOT_EQUAL,
        BOOL_FALSE,
        BOOL_TRUE,
        VARIABLE,
    }
    public class Token
    {
        protected TokenType tokenType;        
        public TokenType GetTokenType
        {
            get
            {
                return tokenType;
            }
        }
        public override string ToString()
        {
            return tokenType.ToString();
        }
    }
}
