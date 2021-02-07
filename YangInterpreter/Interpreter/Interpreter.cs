using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using YangInterpreter.Nodes.BaseNodes;
using YangInterpreter.Nodes;
using YangInterpreter.Nodes.Types;
using YangInterpreter.Nodes.Property;

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
        private YangNode InterpreterTracer;
        private YangNode TracerCurrentNode;

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
                TokenTypes.DescriptionWithValueNextLine,
                TokenTypes.NamespaceMultiline,
                TokenTypes.OrganizationMultiline,
                TokenTypes.ValueForPreviousLineBeg,
                TokenTypes.ValueForPreviousLineMultiline,
                TokenTypes.ContactMultiline,
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
            };
        }

        /// <summary>
        /// Converts the raw text into objects that the YangHandlerTool can use easily;
        /// </summary>
        internal YangNode ConvertText(string YangAsRawText)
        {
            var YangAsRawTextRowByRow = Regex.Split(YangAsRawText, "\r\n|\r|\n");
            var PreviousState = TokenTypes.Start;

            foreach (var RowOfYangText in YangAsRawTextRowByRow)
            {
                CurrentRow = RowOfYangText;
                LineNumber++;
                var TokenForCurrentRow = TokenCreator.GetTokenForRow(RowOfYangText);
                if (TokenForCurrentRow != null)
                {
                    if (MultiLineToken(TokenForCurrentRow))
                    {
                        if (TokenForCurrentRow.TokenType == TokenTypes.ValueForPreviousLineMultiline && PreviousState != TokenTypes.ValueForPreviousLineBeg)
                            NodeProcessionFail(TokenForCurrentRow,LineNumber);
                        PreviousState = TokenForCurrentRow.TokenType;
                        SetPreviousToken(TokenForCurrentRow);
                        continue;
                    }
                    if (PreviousToken != null)
                    {
                        var prevtoken = GetpreviousToken();
                        prevtoken.TokenValue += TokenForCurrentRow.TokenValue;
                        prevtoken.TokenType = prevtoken.TokenAsSingleLine;
                        TokenForCurrentRow = prevtoken;
                    }
                    ProcessToken(TokenForCurrentRow, Metadata);
                }
            }
            return InterpreterTracer;
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
                PreviousToken = previoustoken;
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
        /*private void AddNewYangNode(Type type, Token InputToken)
        {
            var instantiatedobj = (YangNode)Activator.CreateInstance(type, InputToken.TokenName);
            ((ContainerCapability)TracerCurrentNode).AddChild(instantiatedobj);
            TracerCurrentNode = instantiatedobj;
            NewInterpreterStatus(InputToken.TokenType);
        }*/

        /// <summary>
        /// Adds new yang node to the current node.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="InputToken"></param>
        /// <param name="opt"></param>
        private YangNode AddNewYangNode(Type type, Token InputToken, YangAddingOption opt = YangAddingOption.None)
        {
            var instantiatedobj = (YangNode)Activator.CreateInstance(type, InputToken.TokenName);
            instantiatedobj = ((ContainerCapability)TracerCurrentNode).AddChild(instantiatedobj);
            if (opt == YangAddingOption.None)
            {
                TracerCurrentNode = instantiatedobj;
                NewInterpreterStatus(InputToken.TokenType);
            }
            else if(opt == YangAddingOption.ChildIncapable)
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
            else if (InputToken.TokenType == TokenTypes.Skip && TracerCurrentNode.GetType().BaseType == typeof(ContainerCapability) 
                                                             && ((ContainerCapability)TracerCurrentNode).Count() > 1
                                                             && !ModulEnded) 
            {
                AddNewYangNode(typeof(EmptyLineNode), InputToken, YangAddingOption.ChildAndStatusless);
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
                if (InputToken.TokenType == TokenTypes.Namespace)
                {
                    ///Metadata[0] contains fullnamespace
                    CreateMetadata(new List<string> { InputToken.TokenValue });
                    NewInterpreterStatus(TokenTypes.Namespace);
                }
                else if (InputToken.TokenType == TokenTypes.Revision)
                {
                    AddNewYangNode(typeof(Revision), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Organization)
                {
                    ((Module)InterpreterTracer).Organization = InputToken.TokenValue;
                }
                else if (InputToken.TokenType == TokenTypes.Contact)
                {
                    ((Module)InterpreterTracer).Contact = InputToken.TokenValue;
                }
                else if (InputToken.TokenType == TokenTypes.Organization)
                {
                    ((Module)InterpreterTracer).Organization = InputToken.TokenValue;
                }
                else if (InputToken.TokenType == TokenTypes.Description)
                {
                    InterpreterTracer.AddProperty(new Description(InputToken.TokenValue));
                }
                else if (InputToken.TokenType == TokenTypes.Reference)
                {
                    InterpreterTracer.AddProperty(new Reference(InputToken.TokenValue));
                }
                else if (InputToken.TokenType == TokenTypes.Container)
                {
                    AddNewYangNode(typeof(Container), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.YangVersion)
                {
                    var node = AddNewYangNode(typeof(YangVersionNode), InputToken, YangAddingOption.ChildAndStatusless) as YangVersionNode;
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
                    AddNewYangNode(typeof(Grouping), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Uses)
                {
                    AddNewYangNode(typeof(Uses), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Import)
                {
                    ((Module)InterpreterTracer).AddNamespace(InputToken.TokenValue, InputToken.TokenName);
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
                    AddNewYangNode(typeof(Leaf), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.LeafList)
                {
                    AddNewYangNode(typeof(LeafList), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Container)
                {
                    AddNewYangNode(typeof(Container), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.List)
                {
                    AddNewYangNode(typeof(ListNode), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Uses)
                {
                    AddNewYangNode(typeof(Uses), InputToken, YangAddingOption.ChildAndStatusless);
                }
                else if (InputToken.TokenType == TokenTypes.Choice)
                {
                    AddNewYangNode(typeof(Choices), InputToken);
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
                    AddNewYangNode(typeof(EnumTypeNode), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.TypeEmpty)
                {
                    AddNewYangNode(typeof(EmptyTypeNode), InputToken,YangAddingOption.ChildAndStatusless);
                }
                else if (InputToken.TokenType == TokenTypes.Description)
                {
                    TracerCurrentNode.AddProperty(new Description(InputToken.TokenValue));
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
                    AddNewYangNode(typeof(EnumTypeNode), InputToken, YangAddingOption.ChildAndStatusless);
                }
                else if (InputToken.TokenType == TokenTypes.Description)
                {
                    TracerCurrentNode.AddProperty(new Description(InputToken.TokenValue));
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
                    AddNewYangNode(typeof(Leaf), InputToken);
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
                    AddNewYangNode(typeof(TypeNode), InputToken);
                    CreateMetadata(new List<string> { InputToken.TokenName });
                }
                else if (InputToken.TokenType == TokenTypes.Description)
                {
                    Description desc = new Description(InputToken.TokenValue);
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
                    AddNewYangNode(typeof(Leaf), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.LeafList)
                {
                    AddNewYangNode(typeof(LeafList), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Container)
                {
                    AddNewYangNode(typeof(Container), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.List)
                {
                    AddNewYangNode(typeof(ListNode), InputToken);
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
                    AddNewYangNode(typeof(ChoiceCase), InputToken);
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
                    AddNewYangNode(typeof(Leaf), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.LeafList)
                {
                    AddNewYangNode(typeof(LeafList), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Container)
                {
                    AddNewYangNode(typeof(Container), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.List)
                {
                    AddNewYangNode(typeof(ListNode), InputToken);
                }
                else if (InputToken.TokenType == TokenTypes.Uses)
                {
                    AddNewYangNode(typeof(Uses), InputToken, YangAddingOption.ChildAndStatusless);
                }
                else if (InputToken.TokenType == TokenTypes.Choice)
                {
                    AddNewYangNode(typeof(Choices), InputToken);
                }
                else
                {
                    NodeProcessionFail(InputToken, LineNumber);
                }
            }

            ///NAMESPACE
            else if (InterpreterStatus == TokenTypes.Namespace && InputToken.TokenType == TokenTypes.Prefix)
            {
                ///Metadata[0] contains fullnamespace
                ((Module)InterpreterTracer).Namespace = Metadata[0];
                ((Module)InterpreterTracer).Prefix = InputToken.TokenValue;

                //((Module)InterpreterTracer).AddNamespace(InputToken.TokenValue, Metadata[0]);
                FallbackToPreviousInterpreterStatus();
            }

            ///REVISION
            else if (InterpreterStatus == TokenTypes.Revision && InputToken.TokenType == TokenTypes.Description)
            {
                TracerCurrentNode.AddProperty(new Description(InputToken.TokenValue));
            }

            ///No search scheme match => Incorrect inputline;
            else
            {
                NodeProcessionFail(InputToken, LineNumber);
            }
        }
        #endregion

    private void NodeProcessionFail(Token tokenAtError,int rowNumber)
        {

            var asdasd = TracerCurrentNode;
            ErrorLogger erLogger = new ErrorLogger(rowNumber,CurrentRow,LoggingOptions.TextLog);
           var LogResult = erLogger.CreateLog(TracerCurrentNode, InterpreterStatusStack,tokenAtError);

            string ErrorExtraText = string.Empty;
            if (erLogger.LoggingOption == LoggingOptions.TextLog && LogResult)
                ErrorExtraText = "Check logfile for extra info." + Environment.NewLine + erLogger.LoggingPath;

            throw new InterpreterParseFail("Interpreter failed to parse row: "+ rowNumber + " " +ErrorExtraText);
        }
    }
}
