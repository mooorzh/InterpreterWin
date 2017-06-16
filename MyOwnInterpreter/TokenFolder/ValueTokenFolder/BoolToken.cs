using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterNamespace.TokenFolder.ValueTokenFolder
{
    class BoolToken : ValueToken<bool>
    {
        public BoolToken(bool Value) : base(Value)
        {
            this.tokenType = TokenType.BOOL;
        }

        public static BoolToken operator |(BoolToken a, BoolToken b)
        {
            return new BoolToken(a.GetValue || b.GetValue);
        }
        public static BoolToken operator &(BoolToken a, BoolToken b)
        {
            return new BoolToken(a.GetValue && b.GetValue);
        }
        public static BoolToken operator !(BoolToken a)
        {
            return new BoolToken(!a.GetValue);
        }

        public static bool operator ==(BoolToken a, BoolToken b)
        {
            return a.GetValue == b.GetValue;
        }
        public static bool operator !=(BoolToken a, BoolToken b)
        {
            return a.GetValue != b.GetValue;
        }
    }
}
