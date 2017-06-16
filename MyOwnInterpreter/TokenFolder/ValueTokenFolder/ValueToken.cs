using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterNamespace.TokenFolder.ValueTokenFolder
{
    abstract class ValueToken<T> : ValueToken
    {
        
        public T GetValue
        {
            get
            {
                return (T)Val;
            }
        }
        public ValueToken(T Value)
        {            
            this.Val = Value;
        }
        public ValueToken()
        {            
        }
        public override string ToString()
        {
            return GetValue.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is ValueToken<T>)
                return GetValue.GetHashCode() == (obj as ValueToken<T>).GetValue.GetHashCode();
            else
                return false;
        }
        public override int GetHashCode()
        {
            return GetValue.GetHashCode();
        }
    }
    public abstract class ValueToken : Token
    {
        protected object Value;
        public object Val
        {
            get
            {
                return Value;
            }
            set
            {
                Value = value;
            }
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
