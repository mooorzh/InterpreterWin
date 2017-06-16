using InterpreterNamespace.TokenFolder;
using InterpreterNamespace.TokenFolder.IDTokenFolder;
using InterpreterNamespace.TokenFolder.ValueTokenFolder;
using InterpreterNamespace.TokenizerFolder;
using System;
using System.Collections.Generic;

namespace InterpreterNamespace.InterpreterFolder
{
    public abstract class Interpreter
    {
        protected Token[] TokenList;
        protected Dictionary<string, ValueToken> Variables = new Dictionary<string,ValueToken>();
        protected int CurrentPosition;
        protected Token CurrentToken
        {
            get
            {
                return TokenList[CurrentPosition];
            }
        }
        public Interpreter(string text)
        {
            TokenList = new Tokenizer(text).MakeTokenArray();
            CurrentPosition = 0;
        }        
        protected TokenType Eat(TokenType ExpType)
        {
            TokenType res = TokenType.EOF;
            if (ExpType == CurrentToken.GetTokenType)
            {
                CurrentPosition++;
                res = CurrentToken.GetTokenType;
            }
            else
                Error($"Error: Unexpected token(Expected:{ExpType},Currend:{CurrentToken})");
            return res;
        }//Done
        public virtual void Run()
        {
            while (CurrentToken.GetTokenType != TokenType.EOF)
            {
                Sentense();
            }
        }//Done
        protected void Sentense()
        {
            if (CurrentToken.GetTokenType == TokenType.VARIABLE)
            {
                DeclarationSentense();
                Eat(TokenType.SEMI);
            }
            else if (CurrentToken.GetTokenType == TokenType.ID)
            {
                EquationSentense();
                Eat(TokenType.SEMI);
            }
            else if ((CurrentToken.GetTokenType == TokenType.WRITE_FUNCTION))
            {
                ProcedureCallSentense();
                Eat(TokenType.SEMI);
            }
            else if (CurrentToken.GetTokenType == TokenType.WHILE)
            {
                WhileSentense();
            }
            else if (CurrentToken.GetTokenType == TokenType.IF)
            {
                IfSentense();
            }
            else if (CurrentToken.GetTokenType == TokenType.L_FIGURE)
                Block();
            else
                Error();
        }//Done
        protected void Block()
        {
            Eat(TokenType.L_FIGURE);
            while (!(CurrentToken.GetTokenType==TokenType.R_FIGURE))
            {
                Sentense();                
            }
            Eat(TokenType.R_FIGURE);
        }//Need Test
        protected void SkipBlock()
        {
            Eat(TokenType.L_FIGURE);
            while (!(CurrentToken.GetTokenType==TokenType.R_FIGURE))
            {
                if (CurrentToken.GetTokenType==TokenType.L_FIGURE)
                    SkipBlock();
                else
                    Eat(CurrentToken.GetTokenType);
            }
            Eat(TokenType.R_FIGURE);
        }//Need Test
        protected void SkipSentense()
        {
            while (!(CurrentToken.GetTokenType==TokenType.SEMI))
                Eat(CurrentToken.GetTokenType);
            Eat(TokenType.SEMI);
        }//Need Test
        protected void IfSentense()
        {
            Eat(TokenType.IF);
            Eat(TokenType.L_PAR);
            ValueToken result = Expression();
            Eat(TokenType.R_PAR);
            if (result is BoolToken)
            {
                if ((result as BoolToken).GetValue)
                    Sentense();
                else
                {
                    if (CurrentToken.GetTokenType == TokenType.L_FIGURE)
                        SkipBlock();
                    else
                        SkipSentense();
                }
                if (CurrentToken.GetTokenType == TokenType.ELSE)
                {
                    Eat(TokenType.ELSE);
                    if (!(result as BoolToken).GetValue)
                        Sentense();
                    else
                        if (CurrentToken.GetTokenType == TokenType.L_FIGURE)
                        SkipBlock();
                    else
                        SkipSentense();
                }
            }
                       
        }//Need Test
        protected void WhileSentense()
        {
            int begin = CurrentPosition;
            Eat(TokenType.WHILE);
            Eat(TokenType.L_PAR);
            ValueToken result = Expression();
            Eat(TokenType.R_PAR);
            if (result is BoolToken)
            {
                if ((result as BoolToken).GetValue)
                {                                
                    Sentense();                    
                    CurrentPosition = begin;
                    WhileSentense();
                }
                else
                {
                    if (CurrentToken.GetTokenType == TokenType.L_FIGURE)
                        SkipBlock();
                    else
                        SkipSentense();
                }
            }
            else
                Error();

        }//Need Test
        protected void ProcedureCallSentense()
        {
            if (CurrentToken.GetTokenType==TokenType.WRITE_FUNCTION)
                WriteFunction();            
        }//Done
        protected abstract void WriteFunction();
        protected abstract ValueToken ReadFunction();
        protected void EquationSentense()
        {
            string VariableName = (CurrentToken as IDToken).GetName;
            Eat(TokenType.ID);
            Eat(TokenType.EQUAL);
            if (Variables.ContainsKey(VariableName))
                Variables[VariableName] = Expression();
            else
                Error();
        }//Done,need TEst
        protected void DeclarationSentense()
        {
            Eat(TokenType.VARIABLE);
            do
            {
                if (CurrentToken.GetTokenType == TokenType.COMA)
                    Eat(TokenType.COMA);
                if (CurrentToken.GetTokenType == TokenType.ID)
                    Variables.Add((CurrentToken as IDToken).GetName, null);
                else
                    Error();
                if (TokenList[CurrentPosition + 1].GetTokenType == TokenType.EQUAL)
                    EquationSentense();
                else
                    Eat(CurrentToken.GetTokenType);
            }
            while (CurrentToken.GetTokenType==TokenType.COMA);
        }//Done,need TEst
        protected ValueToken Expression()
        {
            return DizOp();
        }//Done
        protected ValueToken DizOp()
        {
            ValueToken left = ConOp();
            while (CurrentToken.GetTokenType == TokenType.DIZ)
            {
                Eat(TokenType.DIZ);
                ValueToken right = ConOp();
                if ((left is BoolToken) && (right is BoolToken))
                    left = (left as BoolToken) | (right as BoolToken);
                else
                    Error("Error in DizOp: one of the values are not Bool type");
            }
            return left;
        }//Done
        protected ValueToken ConOp()
        {
            ValueToken left = CompareOps();
            while(CurrentToken.GetTokenType == TokenType.CON)
            {
                Eat(TokenType.CON);
                ValueToken right = CompareOps();
                if ((left is BoolToken) && (right is BoolToken))
                    left = (left as BoolToken) & (right as BoolToken);
                else
                    Error("Error in ConOp: one of the values are not Bool type");
            }
            return left;
        }//Done
        protected ValueToken CompareOps()
        {
            ValueToken left = PlusMinusOps();
            while ((CurrentToken.GetTokenType == TokenType.MORE) || (CurrentToken.GetTokenType == TokenType.LESS) || (CurrentToken.GetTokenType == TokenType.EQUAL_BOOL) || (CurrentToken.GetTokenType == TokenType.NOT_EQUAL))
            {
                if (CurrentToken.GetTokenType == TokenType.MORE)
                {
                    Eat(TokenType.MORE);
                    ValueToken right = PlusMinusOps();
                    if (left is IntegerToken)
                        if (right is IntegerToken)
                            left = new BoolToken((left as IntegerToken) > (right as IntegerToken));
                        else if (right is DoubleToken)
                            left = new BoolToken((left as IntegerToken) > (right as DoubleToken));
                        else if(right is CharToken)
                            left = new BoolToken((left as IntegerToken) > (right as CharToken));//!!!!!!!!!!!!!!!!!
                        else
                            Error("Error in CompareExpr: wrong value type");
                    else if (left is DoubleToken)
                        if (right is IntegerToken)
                            left = new BoolToken((left as DoubleToken) > (right as IntegerToken));
                        else if (right is DoubleToken)
                            left = new BoolToken((left as DoubleToken) > (right as DoubleToken));
                        else
                            Error("Error in CompareExpr: wrong value type");
                    else if (left is CharToken)
                        if (right is CharToken)
                            left = new BoolToken((left as CharToken) > (right as CharToken));                        
                        else if(right is IntegerToken)
                            left = new BoolToken((left as CharToken) > (right as IntegerToken));
                        else
                            Error("Error in CompareExpr: wrong value type");                    
                    else
                        Error("Error in CompareExpr: wrong value type");
                }
                else if (CurrentToken.GetTokenType == TokenType.LESS)
                {
                    Eat(TokenType.LESS);
                    ValueToken right = PlusMinusOps();
                    if (left is IntegerToken)
                        if (right is IntegerToken)
                            left = new BoolToken((left as IntegerToken) < (right as IntegerToken));
                        else if (right is DoubleToken)
                            left = new BoolToken((left as IntegerToken) < (right as DoubleToken));
                        else if (right is CharToken)
                            left = new BoolToken((left as IntegerToken) < (right as CharToken));
                        else
                            Error("Error in CompareExpr: wrong value type");
                    else if (left is DoubleToken)
                        if (right is IntegerToken)
                            left = new BoolToken((left as DoubleToken) < (right as IntegerToken));
                        else if (right is DoubleToken)
                            left = new BoolToken((left as DoubleToken) < (right as DoubleToken));
                        else
                            Error("Error in CompareExpr: wrong value type");
                    else if (left is CharToken)
                        if (right is CharToken)
                            left = new BoolToken((left as CharToken) > (right as CharToken));
                        else if (right is IntegerToken)
                            left = new BoolToken((left as CharToken) > (right as IntegerToken));
                        else
                            Error("Error in CompareExpr: wrong value type");
                    else
                        Error("Error in CompareExpr: wrong value type");
                }
                else if (CurrentToken.GetTokenType == TokenType.EQUAL_BOOL)
                {
                    Eat(TokenType.EQUAL_BOOL);
                    ValueToken right = PlusMinusOps();
                    if (left is IntegerToken)
                        if (right is IntegerToken)
                            left = new BoolToken((left as IntegerToken) == (right as IntegerToken));
                        else if (right is DoubleToken)
                            left = new BoolToken((left as IntegerToken) == (right as DoubleToken));
                        else if (right is CharToken)
                            left = new BoolToken((left as IntegerToken) == (right as CharToken));
                        else
                            Error("Error in CompareExpr: wrong value type");
                    else if (left is DoubleToken)
                        if (right is IntegerToken)
                            left = new BoolToken((left as DoubleToken) == (right as IntegerToken));
                        else if (right is DoubleToken)
                            left = new BoolToken((left as DoubleToken) == (right as DoubleToken));
                        else
                            Error("Error in CompareExpr: wrong value type");
                    else if (left is CharToken)
                        if (right is CharToken)
                            left = new BoolToken((left as CharToken) == (right as CharToken));
                        else if (right is IntegerToken)
                            left = new BoolToken((left as CharToken) == (right as IntegerToken));
                        else
                            Error("Error in CompareExpr: wrong value type");
                    else if(left is BoolToken)
                        if(right is BoolToken)
                            left = new BoolToken((left as BoolToken) == (right as BoolToken));
                        else
                            Error("Error in CompareExpr: wrong value type");
                    else if(left is StringToken)
                        if(right is StringToken)
                            left = new BoolToken((left as StringToken) == (right as StringToken));
                }
                else if (CurrentToken.GetTokenType == TokenType.NOT_EQUAL)
                {
                    Eat(TokenType.NOT_EQUAL);
                    ValueToken right = PlusMinusOps();
                    if (left is IntegerToken)
                        if (right is IntegerToken)
                            left = new BoolToken((left as IntegerToken) != (right as IntegerToken));
                        else if (right is DoubleToken)
                            left = new BoolToken((left as IntegerToken) != (right as DoubleToken));
                        else if (right is CharToken)
                            left = new BoolToken((left as IntegerToken) != (right as CharToken));
                        else
                            Error("Error in CompareExpr: wrong value type");
                    else if (left is DoubleToken)
                        if (right is IntegerToken)
                            left = new BoolToken((left as DoubleToken) != (right as IntegerToken));
                        else if (right is DoubleToken)
                            left = new BoolToken((left as DoubleToken) != (right as DoubleToken));
                        else
                            Error("Error in CompareExpr: wrong value type");
                    else if (left is CharToken)
                        if (right is CharToken)
                            left = new BoolToken((left as CharToken) != (right as CharToken));
                        else if (right is IntegerToken)
                            left = new BoolToken((left as CharToken) != (right as IntegerToken));
                        else
                            Error("Error in CompareExpr: wrong value type");
                    else if (left is BoolToken)
                        if (right is BoolToken)
                            left = new BoolToken((left as BoolToken) != (right as BoolToken));
                        else
                            Error("Error in CompareExpr: wrong value type");
                    else if (left is StringToken)
                        if (right is StringToken)
                            left = new BoolToken((left as StringToken) != (right as StringToken));
                }
                
            }
            return left;
        }//Done
        protected ValueToken PlusMinusOps()
        {
            ValueToken left = MulDivOps();
            while ((CurrentToken.GetTokenType == TokenType.PLUS) || (CurrentToken.GetTokenType == TokenType.MINUS))
            {
                if (CurrentToken.GetTokenType == TokenType.PLUS)
                {
                    Eat(TokenType.PLUS);
                    ValueToken right = MulDivOps();
                    if (left is IntegerToken)
                        if (right is IntegerToken)
                            left = (left as IntegerToken) + (right as IntegerToken);
                        else if (right is DoubleToken)
                            left = (left as IntegerToken) + (right as DoubleToken);
                        else
                            Error("Error in PlusMinus: wrong value type");
                    else if (left is DoubleToken)
                        if (right is IntegerToken)
                            left = (left as DoubleToken) + (right as IntegerToken);
                        else if (right is DoubleToken)
                            left = (left as DoubleToken) + (right as DoubleToken);
                        else
                            Error("Error in PlusMinus: wrong value type");
                    else if (left is CharToken)
                        if (right is CharToken)
                            left = (left as CharToken) + (right as CharToken);
                        else if (right is StringToken)
                            left = (left as CharToken) + (right as StringToken);
                        else
                            Error("Error in PlusMinus: wrong value type");
                    else if (left is StringToken)
                        if (right is CharToken)
                            left = (left as StringToken) + (right as CharToken);
                        else if (right is StringToken)
                            left = (left as StringToken) + (right as StringToken);
                        else
                            Error("Error in PlusMinus: wrong value type");
                    else
                        Error("Error in PlusMinus: wrong value type");
                }
                else if (CurrentToken.GetTokenType == TokenType.MINUS)
                {
                    Eat(TokenType.MINUS);
                    ValueToken right = MulDivOps();
                    if (left is IntegerToken)
                        if (right is IntegerToken)
                            left = (left as IntegerToken) - (right as IntegerToken);
                        else if (right is DoubleToken)
                            left = (left as IntegerToken) - (right as DoubleToken);
                        else
                            Error("Error in PlusMinus: wrong value type");
                    else if (left is DoubleToken)
                        if (right is IntegerToken)
                            left = (left as DoubleToken) - (right as IntegerToken);
                        else if (right is DoubleToken)
                            left = (left as DoubleToken) - (right as DoubleToken);
                        else
                            Error("Error in PlusMinus: wrong value type");
                    else
                        Error("Error in PlusMinus: wrong value type");
                }
            }
            return left;
        }//Done
        protected ValueToken MulDivOps()
        {
            ValueToken left = PowOps();
            while ((CurrentToken.GetTokenType == TokenType.DIV) || (CurrentToken.GetTokenType == TokenType.MUL))
            {
                if (CurrentToken.GetTokenType == TokenType.DIV)
                {
                    Eat(TokenType.DIV);
                    ValueToken right = PowOps();
                    if (left is IntegerToken)
                        if (right is IntegerToken)
                            left = (left as IntegerToken) / (right as IntegerToken);
                        else if (right is DoubleToken)
                            left = (left as IntegerToken) / (right as DoubleToken);
                        else
                            Error("Error in MulDiv: wrong value type");
                    else if (left is DoubleToken)
                        if (right is IntegerToken)
                            left = (left as DoubleToken) / (right as IntegerToken);
                        else if (right is DoubleToken)
                            left = (left as DoubleToken) / (right as DoubleToken);
                        else
                            Error("Error in MulDiv: wrong value type");
                    else
                        Error("Error in MulDiv: wrong value type");               
                }
                else if (CurrentToken.GetTokenType == TokenType.MUL)
                {
                    Eat(TokenType.MUL);
                    ValueToken right = PowOps();
                    if (left is IntegerToken)
                        if (right is IntegerToken)
                            left = (left as IntegerToken) * (right as IntegerToken);
                        else if (right is DoubleToken)
                            left = (left as IntegerToken) * (right as DoubleToken);
                        else
                            Error("Error in MulDiv: wrong value type");
                    else if (left is DoubleToken)
                        if (right is IntegerToken)
                            left = (left as DoubleToken) * (right as IntegerToken);
                        else if (right is DoubleToken)
                            left = (left as DoubleToken) * (right as DoubleToken);
                        else
                            Error("Error in MulDiv: wrong value type");
                    else
                        Error("Error in MulDiv: wrong value type");
                }
            }
            return left;
        }//Done
        protected ValueToken PowOps()
        {
            ValueToken left = Fact();
            while (CurrentToken.GetTokenType==TokenType.POW)
            {
                Eat(TokenType.POW);
                ValueToken right = Fact();
                if (left is IntegerToken)
                {
                    if (right is IntegerToken)            
                        left = left as IntegerToken ^ right as IntegerToken;                    
                    else if (right is DoubleToken)              
                        left = left as IntegerToken ^ right as DoubleToken;
                    else
                        Error("Error in Pow: wrong value type");
                }
                else if(left is DoubleToken)
                {                    
                    if (right is IntegerToken)
                        left = left as DoubleToken ^ right as IntegerToken;
                    else if (right is DoubleToken)
                        left = left as DoubleToken ^ right as DoubleToken;
                    else
                        Error("Error in Pow: wrong value type");
                }
            }
            return left;
        }//Done
        protected ValueToken Fact()
        {
            ValueToken result=null;
            if (CurrentToken is ValueToken)
            {
                result = CurrentToken as ValueToken;
                Eat(CurrentToken.GetTokenType);
            }
            else if (CurrentToken.GetTokenType == TokenType.ID)
            {
                if (Variables.ContainsKey((CurrentToken as IDToken).GetName))
                    result = Variables[(CurrentToken as IDToken).GetName];    
                else            
                    Error("Error: Variable is not declarated");
                Eat(TokenType.ID);
            }
            else if(CurrentToken.GetTokenType == TokenType.BOOL_TRUE)
            {
                Eat(TokenType.BOOL_TRUE);
                return new BoolToken(true);
            }
            else if (CurrentToken.GetTokenType == TokenType.BOOL_FALSE)
            {
                return new BoolToken(false);
            }
            else if (CurrentToken.GetTokenType == TokenType.L_PAR)
            {
                Eat(TokenType.L_PAR);
                result = Expression();
                Eat(TokenType.R_PAR);
            }
            else if (CurrentToken.GetTokenType == TokenType.READ_FUNCTION)
            {
                return ReadFunction();
            }
            else
                Error("Error : unexpected token");
            return result;
        }//unDone, need builtInFunctions
        protected void Error(string explain = "")
        {
            throw new Exception(explain);
        }//UnDone??????????
    }
}
