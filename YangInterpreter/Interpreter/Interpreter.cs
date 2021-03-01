using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Statements;
using YangInterpreter.Statements.Types;

namespace YangInterpreter.Interpreter
{
    internal class Interpreter
    {
        InterpreterOption Option = InterpreterOption.Normal;

        public string YangAsRawText { get; private set; }

        private TokenTypes InterpreterStatus = TokenTypes.Start;
        Stack<TokenTypes> InterpreterStatusStack = new Stack<TokenTypes>();
        private StatementBase InterpreterTracer;
        private StatementBase TracerCurrentNode;

        private Token PreviousToken;

        int LineNumber = 0;
        string CurrentRow = string.Empty;
        private bool ModulEnded = false;

        public Interpreter(InterpreterOption opt = InterpreterOption.Normal)
        {
            SubStatementAllowanceCollection.Init();

            Option = opt;
            InterpreterStatusStack.Push(TokenTypes.Start);
            TokenCreator.Init();
        }

        /// <summary>
        /// Converts the raw text into objects that the YangHandlerTool can use easily;
        /// </summary>
        internal StatementBase ConvertText(string YangAsRawText)
        {
            var YangAsRawTextRowByRow = Regex.Split(YangAsRawText, "\r\n|\r|\n");
            var PreviousState = TokenTypes.Start;
            bool MultilineBeginingWasPresent = false;

            foreach (var RowOfYangText in YangAsRawTextRowByRow)
            {
                LineNumber++;
                string InnerBlock = ConvertLine(RowOfYangText, ref MultilineBeginingWasPresent, ref PreviousState);
                while (InnerBlock != string.Empty)
                {
                    InnerBlock = ConvertLine(InnerBlock, ref MultilineBeginingWasPresent, ref PreviousState);
                }
            }
            if (InterpreterStatusStack.Pop() != TokenTypes.Start)
                throw new StatementEndIsMissing("The yang file ended but statement closing symbol(s): \"}\" were missing.");

            return InterpreterTracer;
        }

        /// <summary>
        /// Converts the given line into a interpreted token and processes it into the yang tree at the current state.
        /// </summary>
        /// <param name="RowOfYangText">The given row of the yang file</param>
        /// <param name="MultilineBeginingWasPresent">Determines wether the given row is a part of a Multiline token</param>
        /// <param name="PreviousState">The previous state of the interpreter which is the state after the previously processed row.</param>
        /// <returns></returns>
        internal string ConvertLine(string RowOfYangText,ref bool MultilineBeginingWasPresent, ref TokenTypes PreviousState)
        {
            CurrentRow = RowOfYangText;
            var TokenForCurrentRow = TokenCreator.GetTokenForRow(RowOfYangText,PreviousToken);
            if (TokenForCurrentRow != null)
            {
                if (TokenForCurrentRow.TokenTypeSpecialInfo == TokenTypes.ValueForPreviousLineBeg)
                    MultilineBeginingWasPresent = true;

                if (IsMultiLineToken(TokenForCurrentRow))
                    return HandleMultilineToken(TokenForCurrentRow, ref PreviousState);

                else if (PreviousToken != null)
                {
                    TokenForCurrentRow = MergeMultilineTokenEndingToken(TokenForCurrentRow, PreviousState, MultilineBeginingWasPresent);
                    MultilineBeginingWasPresent = false;
                }
                ProcessToken(TokenForCurrentRow);
                PreviousState = TokenTypes.Start;
            }
            else
                TokenCreationFail(LineNumber);
            return TokenForCurrentRow.InnerBlock;
        }

        private bool IsInvalidState(Token tokenForCurrentRow, TokenTypes previousState)
        {
            if ((previousState == TokenTypes.Start && tokenForCurrentRow.TokenTypeSpecialInfo == TokenTypes.ValueForPreviousLineEnd)
                || (previousState == TokenTypes.Start && tokenForCurrentRow.TokenTypeSpecialInfo == TokenTypes.ValueForPreviousLineMultiline))
                return true;
            else if (tokenForCurrentRow.TokenTypeSpecialInfo == TokenTypes.ValueForPreviousLineMultiline && !IsMultiLineTokentype(previousState))
                return true;
            return false;
        }

        private bool IsInvalidStateAfterMultilineToken(Token tokenForCurrentRow, TokenTypes previousState,bool multilineBegWasPresent)
        {
            return ((!(previousState == TokenTypes.ValueForPreviousLineBeg || previousState == TokenTypes.ValueForPreviousLineMultiline)
                        && tokenForCurrentRow.TokenTypeSpecialInfo == TokenTypes.ValueForPreviousLineEnd
                        && multilineBegWasPresent)

                        || ( previousState == TokenTypes.SameLineStart && tokenForCurrentRow.TokenTypeSpecialInfo != TokenTypes.ValueForPreviousLineEnd)
                        || ( previousState == TokenTypes.NextLineStart && tokenForCurrentRow.TokenTypeSpecialInfo != TokenTypes.ValueForPreviousLine));
        }

        private string HandleMultilineToken(Token tokenForCurrentRow, ref TokenTypes previousState)
        {
            if (IsInvalidState(tokenForCurrentRow, previousState))
                NodeProcessionFail(tokenForCurrentRow, LineNumber);

            previousState = tokenForCurrentRow.TokenTypeSpecialInfo;
            SetPreviousToken(tokenForCurrentRow);
            return tokenForCurrentRow.InnerBlock;
        }

        private Token MergeMultilineTokenEndingToken(Token tokenForCurrentRow, TokenTypes previousState, bool multilineBegPresent)
        {
            if (IsInvalidStateAfterMultilineToken(tokenForCurrentRow, previousState, multilineBegPresent))
            {
                NodeProcessionFail(tokenForCurrentRow, LineNumber);
            }
            var prevtoken = GetpreviousToken();

            prevtoken.TokenArgument += tokenForCurrentRow.TokenArgument;
            prevtoken.IsChildlessContainer = tokenForCurrentRow.IsChildlessContainer;
            prevtoken.TokenType = prevtoken.TokenTypeSpecialInfo;

            return prevtoken;
        }

        /// <summary>
        /// True if the given token is a multiline token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool IsMultiLineToken(Token token)
        {
            return IsMultiLineTokentype(token.TokenTypeSpecialInfo);
        }

        /// <summary>
        /// True if the given token is a multiline token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool IsMultiLineTokentype(TokenTypes tokenType)
        {
            return tokenType != TokenTypes.Empty && tokenType != TokenTypes.Skip && tokenType != TokenTypes.NodeEndingBracket
                   && tokenType != TokenTypes.ValueForPreviousLineEnd && tokenType != TokenTypes.ValueForPreviousLine;
        }

        /// <summary>
        /// Sets PreviousToken to the given token
        /// </summary>
        /// <param name="previoustoken"></param>
        private void SetPreviousToken(Token previoustoken)
        {
            if (PreviousToken == null)
            {
                if (!string.IsNullOrEmpty(previoustoken.TokenArgument))
                    previoustoken.TokenArgument += Environment.NewLine;
                PreviousToken = previoustoken;
            }
            else
            {
                PreviousToken.TokenArgument += previoustoken.TokenArgument + Environment.NewLine;
                PreviousToken.MultilinePreviousQuote = previoustoken.MultilinePreviousQuote;
            }
                
        }

        /// <summary>
        /// Returns the PreviousToken and sets the value of it to null.
        /// </summary>
        /// <returns></returns>
        private Token GetpreviousToken()
        {
            Token rettoken = PreviousToken;
            PreviousToken = null;
            return rettoken;
        }

        /// <summary>
        /// Creates the Root YangNode which is the module in the yang file.
        /// </summary>
        /// <param name="input"></param>
        private Module CreateMainModule(string name)
        {
            return new Module(name);
        }
        private void NewInterpreterStatus(TokenTypes status)
        {
            InterpreterStatusStack.Push(status);
            InterpreterStatus = status;
        }

        private static bool ValidateYangVersionCompatibility(string VersionAsString)
        {
            int ver = 0;
            int.TryParse(VersionAsString, out ver);
            if (ver == 1)
                return true;
            else
                return false;
        }

        private StatementBase AddNewStatement(Type type, Token InputToken, YangAddingOption opt = YangAddingOption.None)
        {
            StatementBase instantiatedobj;
            try
            {
                instantiatedobj = (StatementBase)Activator.CreateInstance(type, InputToken.TokenArgument);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;
                else
                    throw e;
            }
            
            instantiatedobj = TracerCurrentNode.AddStatement(instantiatedobj);
            if (opt == YangAddingOption.None && !typeof(ChildlessStatement).IsAssignableFrom(instantiatedobj.GetType()))
            {
                TracerCurrentNode = instantiatedobj;
                NewInterpreterStatus(InputToken.TokenTypeSpecialInfo);
            }
            else if (opt == YangAddingOption.ChildIncapable)
                NewInterpreterStatus(InputToken.TokenTypeSpecialInfo);

            return instantiatedobj;

        }

        /// <summary>
        /// Falls back to the parent node from the current node.
        /// </summary>
        /// <returns></returns>
        private TokenTypes FallbackToPreviousInterpreterStatus()
        {
            if (ParentFallbackIsNeeded(InterpreterStatus))
            {
                TracerCurrentNode = TracerCurrentNode.Parent;
            }
            //First pop the current TokenType
            var stackpop = InterpreterStatusStack.Pop();
            if (InterpreterStatus == stackpop)
            {
                InterpreterStatus = InterpreterStatusStack.Pop();
                InterpreterStatusStack.Push(InterpreterStatus);
            }
            if (InterpreterStatus == TokenTypes.Start)
                ModulEnded = true;

            return InterpreterStatus;
        }

        /// <summary>
        /// Returns true if stepping out of node is required after processing.
        /// </summary>
        /// <param name="currenttokentype"></param>
        /// <returns></returns>
        private bool ParentFallbackIsNeeded(TokenTypes currenttokentype)
        {
            return (TracerCurrentNode.Elements().Count() > 0);
        }
        private void ProcessToken(Token InputToken)
        {
            ///MODULE ENDED AND THERE ARE UNPROCESSED NOT EMPTY ROWS
            if (ModulEnded && InputToken.TokenTypeSpecialInfo != TokenTypes.Skip)
                NodeProcessionFail(InputToken, LineNumber);

            ///EMPTY LINE FORMATTING
            else if (InputToken.TokenTypeSpecialInfo == TokenTypes.Skip)
                AddNewStatement(typeof(EmptyLineStatement), InputToken, YangAddingOption.ChildAndStatusless);

            ///NODE END 
            else if (InputToken.TokenTypeSpecialInfo == TokenTypes.NodeEndingBracket)
                FallbackToPreviousInterpreterStatus();

            ///ROW PARSE ERROR
            else if (InputToken.TokenAsType == null)
                NodeProcessionFail(InputToken, LineNumber);

            ///YANG VERSION COMPATIBILITY CHECK
            else if (InputToken.TokenAsType == typeof(YangVersionStatement))
            {
                var InstantiatedNewStatement = AddNewStatement(InputToken.TokenAsType, InputToken, YangAddingOption.ChildAndStatusless);
                InstantiatedNewStatement.Value = InputToken.TokenArgument;
                if (!ValidateYangVersionCompatibility(InstantiatedNewStatement.Value) && Option == InterpreterOption.Normal)
                    throw new InvalidYangVersion("The version of this file is not 1 therefore not compatible with the interpreter. If you want to force run anyways use Interpreteroption force argument!");
            }

            ///Reachable Statuses from Start status
            else if (InterpreterStatus == TokenTypes.Start && InputToken.TokenAsType == typeof(Module) && !ModulEnded)
            {
                InterpreterTracer = CreateMainModule(InputToken.TokenArgument);
                TracerCurrentNode = InterpreterTracer;
                NewInterpreterStatus(TokenTypes.SameLineStart);
            }

            ///STATEMENT WITH NO ELEMENT
            else if (InputToken.IsChildlessContainer)
            {
                var InstantiatedNewStatement = AddNewStatement(InputToken.TokenAsType, InputToken, YangAddingOption.ChildAndStatusless);
                InstantiatedNewStatement.GeneratedFrom = InputToken.TokenTypeSpecialInfo;
            }
               

            ///STATEMENT BASE
            else if (typeof(StatementBase).IsAssignableFrom(InputToken.TokenAsType))
            {
                var InstantiatedNewStatement = AddNewStatement(InputToken.TokenAsType, InputToken);
                InstantiatedNewStatement.GeneratedFrom = InputToken.TokenTypeSpecialInfo;
            }

            ///No search scheme match => Incorrect inputline;
            else
                NodeProcessionFail(InputToken, LineNumber);
        }

        private void NodeProcessionFail(Token tokenAtError, int rowNumber)
        {
            ErrorLogger erLogger = new ErrorLogger(rowNumber, CurrentRow, LoggingOptions.TextLog);
            var LogResult = erLogger.CreateLog(TracerCurrentNode, InterpreterStatusStack, tokenAtError);

            string ErrorExtraText = string.Empty;
            if (erLogger.LoggingOption == LoggingOptions.TextLog && LogResult)
                ErrorExtraText = "Check logfile for extra info." + Environment.NewLine + erLogger.LoggingPath;

            throw new InterpreterParseFail("Interpreter failed to parse row: " + rowNumber + " " + ErrorExtraText);
        }


        private void TokenCreationFail(int rowNumber)
        {
            ErrorLogger erLogger = new ErrorLogger(rowNumber, CurrentRow, LoggingOptions.TextLog);
            var LogResult = erLogger.CreateLog(TracerCurrentNode, InterpreterStatusStack);

            string ErrorExtraText = string.Empty;
            if (erLogger.LoggingOption == LoggingOptions.TextLog && LogResult)
                ErrorExtraText = "Check logfile for extra info." + Environment.NewLine + erLogger.LoggingPath;

            throw new InterpreterParseFail("Interpreter failed to parse row: " + rowNumber + " " + ErrorExtraText);
        }
    }
}
