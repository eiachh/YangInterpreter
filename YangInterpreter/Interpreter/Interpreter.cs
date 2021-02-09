using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Statements;
using YangInterpreter.Statements.Types;
using YangInterpreter.Statements.Property;

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
        private List<string> Metadata = new List<string>();
        private Statement InterpreterTracer;
        private Statement TracerCurrentNode;

        private Token PreviousToken;

        int LineNumber = 0;
        string CurrentRow = string.Empty;
        private bool ModulEnded = false;

        public Interpreter(InterpreterOption opt = InterpreterOption.Normal)
        {
            Option = opt;
            InterpreterStatusStack.Push(TokenTypes.Start);
            TokenCreator.Init();

            ///Multiline tokens can only be simple tokens where token name is presented in the first line, token value is presented in the upcoming line(s)
            ///For complex Nodes use "List metadata"
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
            };
        }

        /// <summary>
        /// Converts the raw text into objects that the YangHandlerTool can use easily;
        /// </summary>
        internal Statement ConvertText(string YangAsRawText)
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
                    if (TokenForCurrentRow.TokenType == TokenTypes.ValueForPreviousLineMultiline && !MultilineTokens.Contains(PreviousState))
                        NodeProcessionFail(TokenForCurrentRow, LineNumber);
                    PreviousState = TokenForCurrentRow.TokenType;
                    SetPreviousToken(TokenForCurrentRow);
                    return TokenForCurrentRow.InnerBlock;
                }
                if (PreviousToken != null)
                {
                    if (PreviousState != TokenTypes.ValueForPreviousLineMultiline && TokenForCurrentRow.TokenType == TokenTypes.ValueForPreviousLineEnd && MultilineBegPresent)
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

        private Statement AddNewStatement(Type type, Token InputToken, YangAddingOption opt = YangAddingOption.None)
        {
            Statement instantiatedobj;
            try
            {
                instantiatedobj = (Statement)Activator.CreateInstance(type, InputToken.TokenName);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;
                else
                    throw e;
            }
            
            instantiatedobj = ((Statement)TracerCurrentNode).AddStatement(instantiatedobj);
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
        /// Creates new metadata with the given input list.
        /// </summary>
        /// <param name="input"></param>
        private void CreateMetadata(List<string> input)
        {
            Metadata.Clear();
            Metadata.AddRange(input);
        }

        /// <summary>
        /// Returns true if stepping out of node is required after processing.
        /// </summary>
        /// <param name="currenttokentype"></param>
        /// <returns></returns>
        private bool ParentFallbackIsNeeded(TokenTypes currenttokentype)
        {
            return ItemsThatRequireParentFallback.Contains(currenttokentype);
        }

        #region Interpreter State Machine
        private void ProcessToken(Token InputToken, List<string> metadata)
        {
            ///Reachable Statuses from Start status
            if (InterpreterStatus == TokenTypes.Start && InputToken.TokenType == TokenTypes.Module)
            {
                InterpreterTracer = CreateMainModule(InputToken.TokenName);
                TracerCurrentNode = InterpreterTracer;
                NewInterpreterStatus(TokenTypes.Module);
            }

            ///EMPTY LINE FORMATTING
            else if (InputToken.TokenType == TokenTypes.Skip && TracerCurrentNode.GetType().BaseType == typeof(Statement)
                                                             && ((Statement)TracerCurrentNode).Count() > 1
                                                             && !ModulEnded)
            {
                AddNewStatement(typeof(EmptyLineNode), InputToken, YangAddingOption.ChildAndStatusless);
            }

            else if (InputToken.TokenType == TokenTypes.Skip) { }

            ///NODE END 
            else if (InputToken.TokenType == TokenTypes.NodeEndingBracket)
            {
                FallbackToPreviousInterpreterStatus();
            }

            ///MODULE ENDED AND THERE ARE UNPROCESSED NOT EMPTY ROWS
            else if (InputToken.TokenType != TokenTypes.Skip && ModulEnded)
            {
                NodeProcessionFail(InputToken, LineNumber);
            }

            ///MODULE
            else if (InterpreterStatus == TokenTypes.Module)
            {
                if (InputToken.TokenType == TokenTypes.NamespaceSameLineStart ||
                    InputToken.TokenType == TokenTypes.NamespaceNextLineStart)
                {
                    var namespaceStatement = AddNewStatement(typeof(NamespaceStatement), InputToken, YangAddingOption.ChildAndStatusless);
                    namespaceStatement.Value = InputToken.TokenValue;
                    namespaceStatement.GeneratedFrom = InputToken.TokenType;
                }
                else if (InputToken.TokenType == TokenTypes.Revision)
                {
                    AddNewStatement(typeof(Revision), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.OrganizationSameLineStart ||
                         InputToken.TokenType == TokenTypes.OrganizationNextLineStart)
                {
                    var orgStatement = AddNewStatement(typeof(Organization), InputToken, YangAddingOption.ChildAndStatusless);
                    orgStatement.Value = InputToken.TokenValue;
                    orgStatement.GeneratedFrom = InputToken.TokenType;
                }
                else if (InputToken.TokenType == TokenTypes.ContactSameLineStart ||
                         InputToken.TokenType == TokenTypes.ContactNextLineStart)
                {
                    var contactStatement = AddNewStatement(typeof(Contact), InputToken, YangAddingOption.ChildAndStatusless);
                    contactStatement.Value = InputToken.TokenValue;
                    contactStatement.GeneratedFrom = InputToken.TokenType;
                }
                else if (InputToken.TokenType == TokenTypes.DescriptionSameLineStart ||
                         InputToken.TokenType == TokenTypes.DescriptionNextLineStart)
                {
                    var descStatement = AddNewStatement(typeof(Description), InputToken, YangAddingOption.ChildAndStatusless);
                    descStatement.Value = InputToken.TokenValue;
                    descStatement.GeneratedFrom = InputToken.TokenType;
                }
                else if (InputToken.TokenType == TokenTypes.ReferenceSameLineStart ||
                         InputToken.TokenType == TokenTypes.ReferenceNextLineStart)
                {
                    var refStatement = AddNewStatement(typeof(Reference), InputToken, YangAddingOption.ChildAndStatusless);
                    refStatement.Value = InputToken.TokenValue;
                    refStatement.GeneratedFrom = InputToken.TokenType;
                }
                else if (InputToken.TokenType == TokenTypes.Container)
                {
                    AddNewStatement(typeof(Container), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.YangVersion)
                {
                    var node = AddNewStatement(typeof(YangVersionNode), InputToken, YangAddingOption.ChildAndStatusless) as YangVersionNode;
                    node.Value = InputToken.TokenValue;
                    if (!ValidateYangVersionCompatibility(node.Value) && Option == InterpreterOption.Normal)
                        throw new InvalidYangVersion("The version of this file is not 1 therefore not compatible with the interpreter. If you want to force run anyways use Interpreteroption force argument!");
                }
                else if (InputToken.TokenType == TokenTypes.Typedef)
                {
                    /*AddNewYangNode(typeof(Typedef), InputToken);
                    YangTypes.AddNewYangType(InputToken.TokenName, InputToken.TokenName, LineNumber, InterpreterTracer);*/
                }
                else if (InputToken.TokenType == TokenTypes.Grouping)
                {
                    AddNewStatement(typeof(Grouping), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Uses)
                {
                    AddNewStatement(typeof(Uses), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Import)
                {
                    AddNewStatement(typeof(Import), InputToken);
                    //((Module)InterpreterTracer).AddNamespace(InputToken.TokenValue, InputToken.TokenName);
                }
                else if(InputToken.TokenType == TokenTypes.Prefix)
                {
                    var prefixStatement = AddNewStatement(typeof(Prefix), InputToken, YangAddingOption.ChildAndStatusless);
                    prefixStatement.Value = InputToken.TokenValue;
                }
                else
                {
                    NodeProcessionFail(InputToken, LineNumber);
                }
            }

            ///IMPORT 
            else if(InterpreterStatus == TokenTypes.Import)
            {
                if (InputToken.TokenType == TokenTypes.Prefix)
                {
                    var prefixStatement = AddNewStatement(typeof(Prefix), InputToken, YangAddingOption.ChildAndStatusless);
                    prefixStatement.Value = InputToken.TokenValue;
                }
                else
                {
                    NodeProcessionFail(InputToken, LineNumber);
                }
            }

            ///CONTAINER
            else if (InterpreterStatus == TokenTypes.Container)
            {
                if (InputToken.TokenType == TokenTypes.Leaf)
                {
                    AddNewStatement(typeof(Leaf), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.LeafList)
                {
                    AddNewStatement(typeof(LeafList), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Container)
                {
                    AddNewStatement(typeof(Container), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.List)
                {
                    AddNewStatement(typeof(ListNode), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Uses)
                {
                    AddNewStatement(typeof(Uses), InputToken, YangAddingOption.ChildAndStatusless);
                }
                else if (InputToken.TokenType == TokenTypes.Choice)
                {
                    AddNewStatement(typeof(Choices), InputToken);
                }
                else
                {
                    NodeProcessionFail(InputToken, LineNumber);
                }
            }

            ///LEAF
            else if (InterpreterStatus == TokenTypes.Leaf)
            {
                /*if (InputToken.TokenType == TokenTypes.SimpleType)
                {
                    TracerCurrentNode.Type = new SimpleTypeNode(InputToken.TokenName);
                }*/
                if (InputToken.TokenType == TokenTypes.TypeEnum)
                {
                    AddNewStatement(typeof(EnumTypeNode), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.TypeEmpty)
                {
                    AddNewStatement(typeof(EmptyTypeNode), InputToken, YangAddingOption.ChildAndStatusless);
                }
                else if (InputToken.TokenType == TokenTypes.DescriptionSameLineStart ||
                         InputToken.TokenType == TokenTypes.DescriptionNextLineStart)
                {
                    var descStatement = AddNewStatement(typeof(Description), InputToken, YangAddingOption.ChildAndStatusless);
                    descStatement.Value = InputToken.TokenValue;
                    descStatement.GeneratedFrom = InputToken.TokenType;
                }
                else
                {
                    NodeProcessionFail(InputToken, LineNumber);
                }
            }

            ///LEAF-LIST
            else if (InterpreterStatus == TokenTypes.LeafList)
            {
                /*if (InputToken.TokenType == TokenTypes.SimpleType)
                {
                    TracerCurrentNode.Type = new SimpleTypeNode(InputToken.TokenName);
                }*/
                if (InputToken.TokenType == TokenTypes.TypeEnum)
                {
                    AddNewStatement(typeof(EnumTypeNode), InputToken, YangAddingOption.ChildAndStatusless);
                }
                else if (InputToken.TokenType == TokenTypes.DescriptionSameLineStart ||
                         InputToken.TokenType == TokenTypes.DescriptionNextLineStart)
                {
                    var descStatement = AddNewStatement(typeof(Description), InputToken, YangAddingOption.ChildAndStatusless);
                    descStatement.Value = InputToken.TokenValue;
                    descStatement.GeneratedFrom = InputToken.TokenType;
                }
                else
                {
                    NodeProcessionFail(InputToken, LineNumber);
                }
            }

            ///LIST
            else if (InterpreterStatus == TokenTypes.List)
            {
                if (InputToken.TokenType == TokenTypes.Key)
                {
                    ((ListNode)TracerCurrentNode).Key = InputToken.TokenValue;
                }
                else if (InputToken.TokenType == TokenTypes.Leaf)
                {
                    AddNewStatement(typeof(Leaf), InputToken);
                }
                else
                {
                    NodeProcessionFail(InputToken, LineNumber);
                }
            }

            ///Typedef
            else if (InterpreterStatus == TokenTypes.Typedef)
            {
                if (InputToken.TokenType == TokenTypes.Type)
                {
                    AddNewStatement(typeof(TypeNode), InputToken);
                    CreateMetadata(new List<string> { InputToken.TokenName });
                }
                else if (InputToken.TokenType == TokenTypes.DescriptionSameLineStart ||
                         InputToken.TokenType == TokenTypes.DescriptionNextLineStart)
                {
                    var descStatement = AddNewStatement(typeof(Description), InputToken, YangAddingOption.ChildAndStatusless);
                    descStatement.Value = InputToken.TokenValue;
                    descStatement.GeneratedFrom = InputToken.TokenType;
                }
                else
                {
                    NodeProcessionFail(InputToken, LineNumber);
                }
            }
            ///TYPE
            else if (InterpreterStatus == TokenTypes.Type)
            {
                /*if (InputToken.TokenType == TokenTypes.Range)
                {
                    Range rangeprop = new Range();
                    rangeprop.SetValue(InputToken.TokenValue);
                    ((TypeNode)TracerCurrentNode).Range = rangeprop;
                }
                else if (InputToken.TokenType == TokenTypes.Description)
                {
                    Description desc = new Description(InputToken.TokenValue);
                }
                else if (InputToken.TokenType == TokenTypes.SimpleEnum)
                {
                    AddNewYangNode(typeof(YangEnum), InputToken, YangAddingOption.ChildAndStatusless);

                    //((ContainerCapability)TracerCurrentNode).AddChild(new YangEnum(InputToken.TokenName));
                }*/

                if (InputToken.TokenType == TokenTypes.SimpleEnum)
                {
                    ((EnumTypeNode)TracerCurrentNode).AddEnumProperty(new EnumProperty(InputToken.TokenValue));
                }
                else
                {
                    NodeProcessionFail(InputToken, LineNumber);
                }
            }

            //TYPE-ENUM
            else if (InterpreterStatus == TokenTypes.TypeEnum)
            {
                /*if (InputToken.TokenType == TokenTypes.Range)
                {
                    Range rangeprop = new Range();
                    rangeprop.SetValue(InputToken.TokenValue);
                    ((TypeNode)TracerCurrentNode).Range = rangeprop;
                }
                else if (InputToken.TokenType == TokenTypes.Description)
                {
                    Description desc = new Description(InputToken.TokenValue);
                }
                else if (InputToken.TokenType == TokenTypes.SimpleEnum)
                {
                    AddNewYangNode(typeof(YangEnum), InputToken, YangAddingOption.ChildAndStatusless);

                    //((ContainerCapability)TracerCurrentNode).AddChild(new YangEnum(InputToken.TokenName));
                }*/

                if (InputToken.TokenType == TokenTypes.SimpleEnum)
                {
                    ((EnumTypeNode)TracerCurrentNode).AddEnumProperty(new EnumProperty(InputToken.TokenValue));
                }
                else
                {
                    NodeProcessionFail(InputToken, LineNumber);
                }
            }

            ///GROUPING
            else if (InterpreterStatus == TokenTypes.Grouping)
            {
                if (InputToken.TokenType == TokenTypes.Leaf)
                {
                    AddNewStatement(typeof(Leaf), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.LeafList)
                {
                    AddNewStatement(typeof(LeafList), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Container)
                {
                    AddNewStatement(typeof(Container), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.List)
                {
                    AddNewStatement(typeof(ListNode), InputToken);
                }
                else
                {
                    NodeProcessionFail(InputToken, LineNumber);
                }
            }


            ///CHOICES
            else if (InterpreterStatus == TokenTypes.Choice)
            {
                if (InputToken.TokenType == TokenTypes.ChoiceCase)
                {
                    AddNewStatement(typeof(ChoiceCase), InputToken);
                }
                else
                {
                    NodeProcessionFail(InputToken, LineNumber);
                }
            }

            ///CHOICE-CASE
            else if (InterpreterStatus == TokenTypes.ChoiceCase)
            {
                if (InputToken.TokenType == TokenTypes.Leaf)
                {
                    AddNewStatement(typeof(Leaf), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.LeafList)
                {
                    AddNewStatement(typeof(LeafList), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Container)
                {
                    AddNewStatement(typeof(Container), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.List)
                {
                    AddNewStatement(typeof(ListNode), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Uses)
                {
                    AddNewStatement(typeof(Uses), InputToken, YangAddingOption.ChildAndStatusless);
                }
                else if (InputToken.TokenType == TokenTypes.Choice)
                {
                    AddNewStatement(typeof(Choices), InputToken);
                }
                else
                {
                    NodeProcessionFail(InputToken, LineNumber);
                }
            }

            ///NAMESPACE
            /*else if (InterpreterStatus == TokenTypes.Namespace && InputToken.TokenType == TokenTypes.Prefix)
            {
                ///Metadata[0] contains fullnamespace
                ((Module)InterpreterTracer).Namespace = Metadata[0];
                ((Module)InterpreterTracer).Prefix = InputToken.TokenValue;

                //((Module)InterpreterTracer).AddNamespace(InputToken.TokenValue, Metadata[0]);
                FallbackToPreviousInterpreterStatus();
            }*/

            ///REVISION
            else if (InputToken.TokenType == TokenTypes.DescriptionSameLineStart ||
                         InputToken.TokenType == TokenTypes.DescriptionNextLineStart)
            {
                var descStatement = AddNewStatement(typeof(Description), InputToken, YangAddingOption.ChildAndStatusless);
                descStatement.Value = InputToken.TokenValue;
                descStatement.GeneratedFrom = InputToken.TokenType;
            }

            ///No search scheme match => Incorrect inputline;
            else
            {
                NodeProcessionFail(InputToken, LineNumber);
            }
        }
        #endregion

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
