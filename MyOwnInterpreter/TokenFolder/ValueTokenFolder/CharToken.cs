using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterNamespace.TokenFolder.ValueTokenFolder
{
    class CharToken : ValueToken<char>
    {
        public CharToken(char Value) : base(Value)
        {
            this.tokenType = TokenType.CHAR;
        }

        public static StringToken operator +(CharToken a, CharToken b)
        {
            return new StringToken(a.GetValue.ToString() + b.GetValue.ToString());
        }
        public static StringToken operator +(CharToken a, StringToken b)
        {
            return new StringToken(a.GetValue.ToString() + b.GetValue);
        }

        public static bool operator >(CharToken a, CharToken b)
        {
            return a.GetValue > b.GetValue;
        }        
        public static bool operator <(CharToken a, CharToken b)
        {
            return a.GetValue < b.GetValue;
        } 
        public static bool operator ==(CharToken a, CharToken b)
        {
            return a.GetValue > b.GetValue;
        }
        public static bool operator !=(CharToken a, CharToken b)
        {
            return a.GetValue > b.GetValue;
        }            

        public static bool operator >(CharToken a, IntegerToken b)
        {
            return a.GetValue > b.GetValue;
        }
        public static bool operator <(CharToken a, IntegerToken b)
        {
            return a.GetValue < b.GetValue;
        }
        public static bool operator ==(CharToken a, IntegerToken b)
        {
            return a.GetValue > b.GetValue;
        }
        public static bool operator !=(CharToken a, IntegerToken b)
        {
            return a.GetValue > b.GetValue;
        }

    }
}
