using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterNamespace.TokenFolder.ValueTokenFolder
{
    class DoubleToken : ValueToken<double>
    {
        public DoubleToken(double Value) : base(Value)
        {
            this.tokenType = TokenType.DOUBLE;
        }

        public static DoubleToken operator +(DoubleToken a, IntegerToken b)
        {
            return new DoubleToken(a.GetValue + b.GetValue);
        }
        public static DoubleToken operator +(DoubleToken a, DoubleToken b)
        {
            return new DoubleToken(a.GetValue + b.GetValue);
        }

        public static DoubleToken operator -(DoubleToken a, IntegerToken b)
        {
            return new DoubleToken(a.GetValue - b.GetValue);
        }
        public static DoubleToken operator -(DoubleToken a, DoubleToken b)
        {
            return new DoubleToken(a.GetValue - b.GetValue);
        }

        public static DoubleToken operator *(DoubleToken a, IntegerToken b)
        {
            return new DoubleToken(a.GetValue * b.GetValue);
        }
        public static DoubleToken operator *(DoubleToken a, DoubleToken b)
        {
            return new DoubleToken(a.GetValue * b.GetValue);
        }

        public static DoubleToken operator /(DoubleToken a, IntegerToken b)
        {
            return new DoubleToken(a.GetValue / b.GetValue);
        }
        public static DoubleToken operator /(DoubleToken a, DoubleToken b)
        {
            return new DoubleToken(a.GetValue / b.GetValue);
        }

        public static DoubleToken operator ^(DoubleToken a, IntegerToken b)
        {
            return new DoubleToken(System.Math.Pow(a.GetValue, b.GetValue));
        }
        public static DoubleToken operator ^(DoubleToken a, DoubleToken b)
        {
            return new DoubleToken(System.Math.Pow(a.GetValue, b.GetValue));
        }

        public static bool operator <(DoubleToken a, DoubleToken b)
        {
            return a.GetValue < b.GetValue;
        }
        public static bool operator >(DoubleToken a, DoubleToken b)
        {
            return a.GetValue > b.GetValue;
        }
        public static bool operator ==(DoubleToken a, DoubleToken b)
        {
            return a.GetValue == b.GetValue;
        }
        public static bool operator !=(DoubleToken a, DoubleToken b)
        {
            return a.GetValue != b.GetValue;
        }

        public static bool operator <(DoubleToken a, IntegerToken b)
        {
            return a.GetValue < b.GetValue;
        }
        public static bool operator >(DoubleToken a, IntegerToken b)
        {
            return a.GetValue > b.GetValue;
        }
        public static bool operator ==(DoubleToken a, IntegerToken b)
        {
            return a.GetValue == b.GetValue;
        }
        public static bool operator !=(DoubleToken a, IntegerToken b)
        {
            return a.GetValue != b.GetValue;
        }

        public static explicit operator DoubleToken(IntegerToken a)
        {
            return new DoubleToken(a.GetValue);
        }

    }
}
