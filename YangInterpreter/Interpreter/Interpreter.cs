using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using YangInterpreter.Statements;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Interpreter
{
    internal class Interpreter
    {
        internal string YangAsRawText { get; private set; }
        
        private InterpreterOption Option = InterpreterOption.Normal;
        internal LoggingOptions LoggerOption = LoggingOptions.TextLog;

        private Type InterpreterStatus = typeof(Interpreter);
        private Stack<Type> InterpreterStatusStack = new Stack<Type>();
        private StatementBase InterpreterTracer;
        private StatementBase TracerCurrentNode;
        private Token PreviousToken;

        private int LineNumber = 0;
        private string CurrentRow = string.Empty;

        private bool ModulEnded = false;
        internal Interpreter(InterpreterOption opt = InterpreterOption.Normal)
        {
            SubStatementAllowanceCollection.Init();

            Option = opt;
            InterpreterStatusStack.Push(typeof(Interpreter));
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
            if (InterpreterStatusStack.Pop() != typeof(Interpreter))
                throw new StatementEndIsMissing("The yang file ended but statement closing symbol(s): \"}\" were missing.");

            return InterpreterTracer;
        }

        /// <summary>
        /// Converts the given line into a interpreted token and processes it into the yang tree at the current state.
        /// </summary>
        /// <param name="InputBlock">The given row of the yang file</param>
        /// <param name="MultilineBeginingWasPresent">Determines wether the given row is a part of a Multiline token</param>
        /// <param name="PreviousState">The previous state of the interpreter which is the state after the previously processed row.</param>
        /// <returns></returns>
        internal string ConvertLine(string InputBlock,ref bool MultilineBeginingWasPresent, ref TokenTypes PreviousState)
        {
            CurrentRow = InputBlock;
            var TokenForCurrentRow = TokenCreator.GetTokenForRow(InputBlock,PreviousToken);
            if (TokenForCurrentRow != null)
            {
                if (TokenForCurrentRow.TokenTypeSpecialInfo == TokenTypes.ValueForPreviousLineBeg)
                    MultilineBeginingWasPresent = true;

                if (IsMultiLineToken(TokenForCurrentRow))
                    return HandleMultilineToken(TokenForCurrentRow, ref PreviousState);

                else if (PreviousToken != null)
                {
                    TokenForCurrentRow = MergeMultilineEndingToken(TokenForCurrentRow, PreviousState, MultilineBeginingWasPresent);
                    MultilineBeginingWasPresent = false;
                }
                ProcessToken(TokenForCurrentRow);
                PreviousState = TokenTypes.Start;
            }
            else
                TokenCreationFail(LineNumber);
            return TokenForCurrentRow.InnerBlock;
        }

        /// <summary>
        /// Returns true if the parsed token state of the interpreter is invalid related to the previous state.
        /// </summary>
        /// <param name="tokenForCurrentRow"></param>
        /// <param name="previousState"></param>
        /// <returns></returns>
        private bool IsInvalidState(Token tokenForCurrentRow, TokenTypes previousState)
        {
            if ((previousState == TokenTypes.Start && tokenForCurrentRow.TokenTypeSpecialInfo == TokenTypes.ValueForPreviousLineEnd)
                || (previousState == TokenTypes.Start && tokenForCurrentRow.TokenTypeSpecialInfo == TokenTypes.ValueForPreviousLineMultiline))
                return true;
            else if (tokenForCurrentRow.TokenTypeSpecialInfo == TokenTypes.ValueForPreviousLineMultiline && !IsMultiLineTokentype(previousState))
                return true;
            return false;
        }

        /// <summary>
        /// Returns true if the parsed token state of the interpreter is invalid related to the previous state if we are in multiline mode.
        /// </summary>
        /// <param name="tokenForCurrentRow"></param>
        /// <param name="previousState"></param>
        /// <param name="multilineBegWasPresent"></param>
        /// <returns></returns>
        private bool IsInvalidStateAfterMultilineToken(Token tokenForCurrentRow, TokenTypes previousState,bool multilineBegWasPresent)
        {
            return ((!(previousState == TokenTypes.ValueForPreviousLineBeg || previousState == TokenTypes.ValueForPreviousLineMultiline)
                        && tokenForCurrentRow.TokenTypeSpecialInfo == TokenTypes.ValueForPreviousLineEnd
                        && multilineBegWasPresent)

                        || ( previousState == TokenTypes.SameLineStart && !(tokenForCurrentRow.TokenTypeSpecialInfo == TokenTypes.ValueForPreviousLineEnd || tokenForCurrentRow.TokenTypeSpecialInfo == TokenTypes.ValueForPreviousLine))
                        || ( previousState == TokenTypes.NextLineStart && tokenForCurrentRow.TokenTypeSpecialInfo != TokenTypes.ValueForPreviousLine));
        }

        /// <summary>
        /// Processes a token when we are in multiline mode.
        /// </summary>
        /// <param name="tokenForCurrentRow"></param>
        /// <param name="previousState"></param>
        /// <returns></returns>
        private string HandleMultilineToken(Token tokenForCurrentRow, ref TokenTypes previousState)
        {
            if (IsInvalidState(tokenForCurrentRow, previousState))
                NodeProcessionFail(tokenForCurrentRow, LineNumber);

            previousState = tokenForCurrentRow.TokenTypeSpecialInfo;
            SetPreviousToken(tokenForCurrentRow);
            return tokenForCurrentRow.InnerBlock;
        }

        /// <summary>
        /// Merges the last token of the multiline with the main part of the multiline token.
        /// </summary>
        /// <param name="tokenForCurrentRow"></param>
        /// <param name="previousState"></param>
        /// <param name="multilineBegPresent"></param>
        /// <returns></returns>
        private Token MergeMultilineEndingToken(Token tokenForCurrentRow, TokenTypes previousState, bool multilineBegPresent)
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
                PreviousToken = previoustoken;
            }
            else
            {
                PreviousToken.TokenArgument += previoustoken.TokenArgument;
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
        private ModuleStatement CreateMainModule(string name)
        {
            return new ModuleStatement(name);
        }
        private void NewInterpreterStatus(Type type)
        {
            InterpreterStatusStack.Push(type);
            InterpreterStatus = type;
        }

        private bool ValidateYangVersionCompatibility(string VersionAsString)
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
                NewInterpreterStatus(InputToken.TokenAsType);
            }
            else if (opt == YangAddingOption.ChildIncapable)
                NewInterpreterStatus(InputToken.TokenAsType);

            return instantiatedobj;

        }

        /// <summary>
        /// Falls back to the parent node from the current node.
        /// </summary>
        /// <returns></returns>
        private void FallbackToPreviousInterpreterStatus()
        {
            if (ParentFallbackIsNeeded())
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
            if (InterpreterStatus == typeof(Interpreter))
                ModulEnded = true;
        }

        /// <summary>
        /// Returns true if stepping out of node is required after processing.
        /// </summary>
        /// <param name="currenttokentype"></param>
        /// <returns></returns>
        private bool ParentFallbackIsNeeded()
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
                InstantiatedNewStatement.Argument = InputToken.TokenArgument;
                if (!ValidateYangVersionCompatibility(InstantiatedNewStatement.Argument) && Option == InterpreterOption.Normal)
                    throw new InvalidYangVersion("The version of this file is not 1 therefore not compatible with the interpreter. If you want to force run anyways use Interpreteroption force argument!");
            }

            ///Reachable Statuses from Start status
            else if (InterpreterStatus == typeof(Interpreter) && InputToken.TokenAsType == typeof(ModuleStatement) && !ModulEnded)
            {
                InterpreterTracer = CreateMainModule(InputToken.TokenArgument);
                TracerCurrentNode = InterpreterTracer;
                NewInterpreterStatus(typeof(ModuleStatement));
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

        /// <summary>
        /// Starts generating the error output if token generating was successful.
        /// </summary>
        /// <param name="tokenAtError"></param>
        /// <param name="rowNumber"></param>
        private void NodeProcessionFail(Token tokenAtError, int rowNumber)
        {
            ErrorLogger erLogger = new ErrorLogger(rowNumber, CurrentRow, LoggerOption);
            var LogResult = erLogger.CreateLog(TracerCurrentNode, InterpreterStatusStack, tokenAtError);

            string ErrorExtraText = string.Empty;
            if (erLogger.LoggingOption == LoggingOptions.TextLog && LogResult)
                ErrorExtraText = "Check logfile for extra info." + Environment.NewLine + erLogger.LoggingPath;

            throw new InterpreterParseFail("Interpreter failed to parse row: " + rowNumber + " " + ErrorExtraText);
        }

        /// <summary>
        /// Starts generat the error output if the token generation failed.
        /// </summary>
        /// <param name="rowNumber"></param>
        private void TokenCreationFail(int rowNumber)
        {
            ErrorLogger erLogger = new ErrorLogger(rowNumber, CurrentRow, LoggerOption);
            var LogResult = erLogger.CreateLog(TracerCurrentNode, InterpreterStatusStack);

            string ErrorExtraText = string.Empty;
            if (erLogger.LoggingOption == LoggingOptions.TextLog && LogResult)
                ErrorExtraText = "Check logfile for extra info." + Environment.NewLine + erLogger.LoggingPath;

            throw new InterpreterParseFail("Interpreter failed to parse row: " + rowNumber + " " + ErrorExtraText);
        }
    }
}
