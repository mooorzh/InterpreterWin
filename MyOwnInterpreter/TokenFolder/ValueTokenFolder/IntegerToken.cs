using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterNamespace.TokenFolder.ValueTokenFolder
{
    class IntegerToken : ValueToken<int>
    {
        public IntegerToken(int Value) : base(Value)
        {
            this.tokenType = TokenType.INTEGER;
        }

        public static IntegerToken operator +(IntegerToken a, IntegerToken b)
        {
            return new IntegerToken(a.GetValue + b.GetValue);
        }
        public static DoubleToken operator +(IntegerToken a, DoubleToken b)
        {
            return new DoubleToken(a.GetValue + b.GetValue);
        }

        public static IntegerToken operator -(IntegerToken a, IntegerToken b)
        {
            return new IntegerToken(a.GetValue - b.GetValue);
        }
        public static DoubleToken operator -(IntegerToken a, DoubleToken b)
        {
            return new DoubleToken(a.GetValue - b.GetValue);
        }

        public static IntegerToken operator *(IntegerToken a, IntegerToken b)
        {
            return new IntegerToken(a.GetValue * b.GetValue);
        }
        public static DoubleToken operator *(IntegerToken a, DoubleToken b)
        {
            return new DoubleToken(a.GetValue * b.GetValue);
        }

        public static DoubleToken operator /(IntegerToken a, IntegerToken b)
        {
            return new DoubleToken(a.GetValue / b.GetValue);
        }
        public static DoubleToken operator /(IntegerToken a, DoubleToken b)
        {
            return new DoubleToken(a.GetValue / b.GetValue);
        }

        public static IntegerToken operator ^(IntegerToken a, IntegerToken b)
        {
            return new IntegerToken((int)System.Math.Pow(a.GetValue, b.GetValue));
        }
        public static DoubleToken operator ^(IntegerToken a, DoubleToken b)
        {
            return new DoubleToken(System.Math.Pow(a.GetValue, b.GetValue));
        }

        public static bool operator <(IntegerToken a, DoubleToken b)
        {
            return a.GetValue < b.GetValue;
        }
        public static bool operator >(IntegerToken a, DoubleToken b)
        {
            return a.GetValue > b.GetValue;
        }
        public static bool operator ==(IntegerToken a, DoubleToken b)
        {
            return a.GetValue == b.GetValue;
        }
        public static bool operator !=(IntegerToken a, DoubleToken b)
        {
            return a.GetValue != b.GetValue;
        }

        public static bool operator <(IntegerToken a, CharToken b)
        {
            return a.GetValue < b.GetValue;
        }
        public static bool operator >(IntegerToken a, CharToken b)
        {
            return a.GetValue > b.GetValue;
        }
        public static bool operator ==(IntegerToken a, CharToken b)
        {
            return a.GetValue == b.GetValue;
        }
        public static bool operator !=(IntegerToken a, CharToken b)
        {
            return a.GetValue != b.GetValue;
        }

        public static bool operator <(IntegerToken a, IntegerToken b)
        {
            return a.GetValue < b.GetValue;
        }
        public static bool operator >(IntegerToken a, IntegerToken b)
        {
            return a.GetValue > b.GetValue;
        }
        public static bool operator ==(IntegerToken a, IntegerToken b)
        {
            return a.GetValue == b.GetValue;
        }
        public static bool operator !=(IntegerToken a, IntegerToken b)
        {
            return a.GetValue != b.GetValue;
        }

        public static explicit operator IntegerToken(DoubleToken a)
        {
            return new IntegerToken((int)a.GetValue);
        }
        public static explicit operator IntegerToken(CharToken a)
        {
            return new IntegerToken(a.GetValue);
        }
    }
}
