using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterNamespace.TokenFolder.ValueTokenFolder
{
    class StringToken : ValueToken<string>
    {
        public StringToken(string Value) : base(Value)
        {
            this.tokenType = TokenType.STRING;
        }

        public static StringToken operator +(StringToken a, StringToken b)
        {
            return new StringToken(a.GetValue + b.GetValue);
        }
        public static StringToken operator +(StringToken a, CharToken b)
        {
            return new StringToken(a.GetValue + b.GetValue.ToString());
        }

        public static bool operator ==(StringToken a, StringToken b)
        {
            return a.GetValue == b.GetValue;
        }
        public static bool operator !=(StringToken a, StringToken b)
        {
            return a.GetValue != b.GetValue;
        }

        public CharToken this[int index]
        {
            get
            {
                return new CharToken(GetValue[index]);
            }
        }
    }
}
