using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Statements;
using YangInterpreter.Statements.Types;
using YangInterpreter.Statements.Property;
using YangInterpreter.Statements.SingleValueStatements;

namespace YangInterpreter.Interpreter
{
    internal class Interpreter
    {
        InterpreterOption Option = InterpreterOption.Normal;

        public string YangAsRawText { get; private set; }
        private static List<TokenTypes> MultilineTokens;

        /// <summary>
        /// List that contains all the tokens that requires step back to parent after processing. Mostly containers.
        /// </summary>
        private static List<TokenTypes> ItemsThatRequireParentFallback;

        private TokenTypes InterpreterStatus = TokenTypes.Start;
        Stack<TokenTypes> InterpreterStatusStack = new Stack<TokenTypes>();
        private BaseStatement InterpreterTracer;
        private BaseStatement TracerCurrentNode;

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

            ///Multiline tokens can only be simple tokens where token name is presented in the first line, token value is presented in the upcoming line(s)
            MultilineTokens = new List<TokenTypes>
            {
                TokenTypes.DescriptionMultiLine,
                TokenTypes.NamespaceMultiLine,
                TokenTypes.OrganizationMultiLine,
                TokenTypes.ValueForPreviousLineBeg,
                TokenTypes.ValueForPreviousLineMultiline,
                TokenTypes.ContactMultiLine,
                TokenTypes.ReferenceMultiline,
            };

            ItemsThatRequireParentFallback = new List<TokenTypes>
            {
                TokenTypes.Leaf,
                TokenTypes.Container,
                TokenTypes.LeafList,
                TokenTypes.List,
                TokenTypes.Choice,
                TokenTypes.ChoiceCase,
                TokenTypes.Typedef,
                TokenTypes.Type,
                TokenTypes.Grouping,
                TokenTypes.TypeEnum,
                TokenTypes.Revision,
                TokenTypes.Import,
                TokenTypes.SimpleBit,
            };
        }

        /// <summary>
        /// Converts the raw text into objects that the YangHandlerTool can use easily;
        /// </summary>
        internal BaseStatement ConvertText(string YangAsRawText)
        {
            var YangAsRawTextRowByRow = Regex.Split(YangAsRawText, "\r\n|\r|\n");
            var PreviousState = TokenTypes.Start;
            bool MultilineBegPresent = false;

            foreach (var RowOfYangText in YangAsRawTextRowByRow)
            {
                LineNumber++;
                string InnerBlock = ConvertLine(RowOfYangText, ref MultilineBegPresent, ref PreviousState);
                while (InnerBlock != string.Empty)
                {
                    InnerBlock = ConvertLine(InnerBlock, ref MultilineBegPresent, ref PreviousState);
                }
                
                /*CurrentRow = RowOfYangText;
                LineNumber++;
                var TokenForCurrentRow = TokenCreator.GetTokenForRow(RowOfYangText);
                if (TokenForCurrentRow != null)
                {
                    if (TokenForCurrentRow.TokenType == TokenTypes.ValueForPreviousLineBeg)
                        MultilineBegPresent = true;
                    if (MultiLineToken(TokenForCurrentRow))
                    {
                        if (TokenForCurrentRow.TokenType == TokenTypes.ValueForPreviousLineMultiline && !MultilineTokens.Contains(PreviousState))
                            NodeProcessionFail(TokenForCurrentRow, LineNumber);
                        PreviousState = TokenForCurrentRow.TokenType;
                        SetPreviousToken(TokenForCurrentRow);
                        continue;
                    }
                    if (PreviousToken != null)
                    {
                        if(PreviousState != TokenTypes.ValueForPreviousLineMultiline &&  TokenForCurrentRow.TokenType == TokenTypes.ValueForPreviousLineEnd && MultilineBegPresent)
                            NodeProcessionFail(TokenForCurrentRow, LineNumber);
                        var prevtoken = GetpreviousToken();
                        prevtoken.TokenValue += TokenForCurrentRow.TokenValue;
                        prevtoken.TokenType = prevtoken.TokenAsSingleLine;
                        TokenForCurrentRow = prevtoken;
                        MultilineBegPresent = false;
                    }
                    ProcessToken(TokenForCurrentRow, Metadata);
                }
                else
                    TokenCreationFail(LineNumber);*/
            }
            return InterpreterTracer;
        }

        internal string ConvertLine(string RowOfYangText,ref bool MultilineBegPresent, ref TokenTypes PreviousState)
        {

            CurrentRow = RowOfYangText;
            var TokenForCurrentRow = TokenCreator.GetTokenForRow(RowOfYangText);
            if (TokenForCurrentRow != null)
            {
                if (TokenForCurrentRow.TokenType == TokenTypes.ValueForPreviousLineBeg)
                    MultilineBegPresent = true;
                if (MultiLineToken(TokenForCurrentRow))
                {
                    if (TokenForCurrentRow.TokenType == TokenTypes.ValueForPreviousLineMultiline && !MultilineTokens.Contains(PreviousState) ||
                        (PreviousState != TokenTypes.ValueForPreviousLineBeg && TokenForCurrentRow.TokenType == TokenTypes.ValueForPreviousLineMultiline))
                        NodeProcessionFail(TokenForCurrentRow, LineNumber);
                    PreviousState = TokenForCurrentRow.TokenType;
                    SetPreviousToken(TokenForCurrentRow);
                    return TokenForCurrentRow.InnerBlock;
                }
                if (PreviousToken != null)
                {
                    if (!(PreviousState == TokenTypes.ValueForPreviousLineBeg || PreviousState == TokenTypes.ValueForPreviousLineMultiline) 
                        && TokenForCurrentRow.TokenType == TokenTypes.ValueForPreviousLineEnd && MultilineBegPresent)
                    {
                        NodeProcessionFail(TokenForCurrentRow, LineNumber);
                    }
                    var prevtoken = GetpreviousToken();
                    prevtoken.TokenValue += TokenForCurrentRow.TokenValue;
                    prevtoken.TokenType = prevtoken.TokenAsSingleLine;
                    TokenForCurrentRow = prevtoken;
                    MultilineBegPresent = false;
                }
                ProcessToken(TokenForCurrentRow);
                PreviousState = TokenTypes.Start;
            }
            else
                TokenCreationFail(LineNumber);
            return TokenForCurrentRow.InnerBlock;
        }

        /// <summary>
        /// True if the given token is a multiline token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool MultiLineToken(Token token)
        {
            return MultilineTokens.Contains(token.TokenType);
        }

        /// <summary>
        /// Sets PreviousToken to the given token
        /// </summary>
        /// <param name="previoustoken"></param>
        private void SetPreviousToken(Token previoustoken)
        {
            if (PreviousToken == null)
            {
                if (!string.IsNullOrEmpty(previoustoken.TokenValue))
                    previoustoken.TokenValue += Environment.NewLine;
                PreviousToken = previoustoken;
            }
            else
                PreviousToken.TokenValue += previoustoken.TokenValue + Environment.NewLine;
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

        private BaseStatement AddNewStatement(Type type, Token InputToken, YangAddingOption opt = YangAddingOption.None)
        {
            BaseStatement instantiatedobj;
            try
            {
                instantiatedobj = (BaseStatement)Activator.CreateInstance(type, InputToken.TokenName);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;
                else
                    throw e;
            }
            
            instantiatedobj = ((BaseStatement)TracerCurrentNode).AddStatement(instantiatedobj);
            if (opt == YangAddingOption.None)
            {
                TracerCurrentNode = instantiatedobj;
                NewInterpreterStatus(InputToken.TokenType);
            }
            else if (opt == YangAddingOption.ChildIncapable)
                NewInterpreterStatus(InputToken.TokenType);

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
            return TracerCurrentNode.GetType().BaseType == typeof(ContainerStatementBase))
        }
        private void ProcessToken(Token InputToken)
        {
            ///Reachable Statuses from Start status
            if (InterpreterStatus == TokenTypes.Start && InputToken.TokenType == TokenTypes.Module)
            {
                InterpreterTracer = CreateMainModule(InputToken.TokenName);
                TracerCurrentNode = InterpreterTracer;
                NewInterpreterStatus(TokenTypes.Module);
            }
            ///EMPTY LINE FORMATTING
            else if (InputToken.TokenType == TokenTypes.Skip && !ModulEnded)
                AddNewStatement(typeof(EmptyLineNode), InputToken, YangAddingOption.ChildAndStatusless);

            ///NODE END 
            else if (InputToken.TokenType == TokenTypes.NodeEndingBracket)
                FallbackToPreviousInterpreterStatus();

            ///MODULE ENDED AND THERE ARE UNPROCESSED NOT EMPTY ROWS
            else if (InputToken.TokenType != TokenTypes.Skip && ModulEnded)
                NodeProcessionFail(InputToken, LineNumber);

            ///ROW PARSE ERROR
            else if (InputToken.TokenAsType == null)
                NodeProcessionFail(InputToken, LineNumber);

            ///YANG VERSION COMPATIBILITY CHECK
            else if (InputToken.TokenAsType == typeof(YangVersionStatement))
            {
                var InstantiatedNewStatement = AddNewStatement(InputToken.TokenAsType, InputToken, YangAddingOption.ChildAndStatusless);
                InstantiatedNewStatement.Value = InputToken.TokenValue;
                if (!ValidateYangVersionCompatibility(InstantiatedNewStatement.Value) && Option == InterpreterOption.Normal)
                    throw new InvalidYangVersion("The version of this file is not 1 therefore not compatible with the interpreter. If you want to force run anyways use Interpreteroption force argument!");
            }

            ///CONTAINER BASED STATEMENT
            else if (InputToken.TokenAsType.BaseType == typeof(ContainerStatementBase) || InputToken.TokenAsType.BaseType == typeof(TypeStatement))
                AddNewStatement(InputToken.TokenAsType, InputToken);

            ///SINGLE VALUE HOLDER STATEMENT
            else if (InputToken.TokenAsType.BaseType == typeof(StatementWithSingleValueBase))
            {
                var InstantiatedNewStatement = AddNewStatement(InputToken.TokenAsType, InputToken, YangAddingOption.ChildAndStatusless);
                InstantiatedNewStatement.Value = InputToken.TokenValue;
                InstantiatedNewStatement.GeneratedFrom = InputToken.TokenType;
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
