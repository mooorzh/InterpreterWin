using InterpreterNamespace.TokenFolder;
using InterpreterNamespace.TokenFolder.IDTokenFolder;
using InterpreterNamespace.TokenFolder.ReservedSymbTokenFolder;
using InterpreterNamespace.TokenFolder.ValueTokenFolder;
using System;
using System.Collections.Generic;

namespace InterpreterNamespace.TokenizerFolder
{
    public class Tokenizer
    {
        private const char EndSymbol = '$';
        private string Text;
        private int CurrentPosition;
        private char CurrentChar
        {
            get
            {
                if (CurrentPosition < Text.Length)
                    return Text[CurrentPosition];
                else
                    return EndSymbol;
            }
        }
        public Tokenizer(string text)
        {
            Text = text;
            CurrentPosition = 0;
        }
        private void Advance()
        {
            CurrentPosition++;
        }
        private char peek()
        {
            if (CurrentPosition + 1 < Text.Length)
                return Text[CurrentPosition + 1];
            else
                return EndSymbol;
        }
        private void SkipSpace()
        {
            while (((CurrentChar == ' ') || (CurrentChar == '\n') || (CurrentChar == '\r') || (CurrentChar == '\t')) && (CurrentChar != EndSymbol))
                Advance();
        }
        private Token Number()
        {
            string res = "";
            while (char.IsDigit(CurrentChar))
            {
                res += CurrentChar;
                Advance();
            }
            if (CurrentChar == '.')
            {
                res += ',';
                Advance();
                if (char.IsDigit(CurrentChar))
                    while (char.IsDigit(CurrentChar))
                    {
                        res += CurrentChar;
                        Advance();
                    }
                else
                    Error();
                return new DoubleToken(double.Parse(res));
            }
            else
                return new IntegerToken(int.Parse(res));
        }
        private StringToken StringToken()
        {
            string res = "";
            Advance();
            while ((CurrentChar != '"') && (CurrentChar != EndSymbol))
            {
                res += CurrentChar;
                Advance();
            }
            if (CurrentChar == '"')
                Advance();
            else
                Error();
            return new StringToken(res);
        }
        private CharToken CharToken()
        {
            if (CurrentChar == '\'')
                Advance();
            else
                Error();
            char result = CurrentChar;
            Advance();
            if (CurrentChar == '\'')
                Advance();
            else
                Error();
            return new CharToken(result);
        }
        private IDToken IDToken()
        {
            string res = "";
            if (CurrentChar == '_')
            {
                res += CurrentChar;
                Advance();
            }
            if (char.IsLetter(CurrentChar))
            {
                while (char.IsLetterOrDigit(CurrentChar))
                {
                    res += CurrentChar;
                    Advance();
                }
            }
            else
                Error();
            return new IDToken(res);


        }
        public Token GetNextToken()
        {
            while (((CurrentChar == ' ') || (CurrentChar == '\n') || (CurrentChar == '\r') || (CurrentChar == '\t')) && (CurrentChar != EndSymbol))
                SkipSpace();
            Token res = null;
            if (char.IsDigit(CurrentChar))
                res = Number();
            else if (char.IsLetter(CurrentChar) || (CurrentChar == '_'))
                res = IDToken();
            else if (CurrentChar == '\"')
                res = StringToken();
            else if (CurrentChar == '\'')
                res = CharToken();
            else if ((CurrentChar.ToString() + peek().ToString()) == "==")
            {
                Advance();
                Advance();
                res = new ReservedSymbToken("==");
            }
            else if ((CurrentChar.ToString() + peek().ToString()) == "!=")
            {
                Advance();
                Advance();
                res = new ReservedSymbToken("!=");
            }
            else
            {
                res = new ReservedSymbToken(CurrentChar);
                Advance();
            }
            return res;
        }
        public Token[] MakeTokenArray()
        {
            List<Token> result = new List<Token>();
            Token tmp;
            do
            {
                tmp = GetNextToken();
                result.Add(tmp);
            }
            while (!(tmp.GetTokenType == TokenType.EOF));
            return result.ToArray();
        }
        private void Error()
        {
            throw new Exception("Error in Toeknizer:" + CurrentChar);
        }
    }
}
