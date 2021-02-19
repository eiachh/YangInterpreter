using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Interpreter
{
    public enum TokenTypes
    {
        Start,
        YangVersion,
        Skip,
        Leaf,
        LeafList,
        Container,
        List,
        Grouping,
        Uses,
        Choice,
        ChoiceCase,
        Status,
        Length,
        Pattern,

        TypeLeafRef,
        TypeString,
        TypeBits,
        TypeEnum,
        TypeEmpty,
        Type,

        SimpleEnum,
        SimpleBit,

        Key,
        Position,

        PathSameLineStart,
        PathNextLineStart,
        PathMultiLine,

        ContactSameLineStart,
        ContactNextLineStart,
        ContactMultiLine,

        NamespaceSameLineStart,
        NamespaceNextLineStart,
        NamespaceMultiLine,

        OrganizationSameLineStart,
        OrganizationNextLineStart,
        OrganizationMultiLine,

        DescriptionSameLineStart,
        DescriptionNextLineStart,
        DescriptionMultiLine,

        ReferenceSameLineStart,
        ReferenceNextLineStart,
        ReferenceMultiline,

        ValueForPreviousLine,
        ValueForPreviousLineMultiline,
        ValueForPreviousLineBeg,
        ValueForPreviousLineEnd,

        ConfigStatement,
        Typedef,
        Range,
        NodeEndingBracket,
        Module,
        Prefix,
        Revision,
        Value,
        Import,


        Empty
    }
    public class Token
    {
        /// <summary>
        /// The type of the current token.
        /// </summary>
        public TokenTypes TokenType { get; set; }
        /// <summary>
        /// The name of the token.
        /// </summary>
        public string TokenName { get; set; }
        /// <summary>
        /// Value of the token.
        /// </summary>
        public string TokenValue { get; set; }

        /// <summary>
        /// The given token as SingleLine token if the current one is Multiline.
        /// </summary>
        public TokenTypes TokenAsSingleLine { get; set; }

        /// <summary>
        /// The token as a Type.
        /// </summary>
        public Type TokenAsType { get; set; }
        /// <summary>
        /// Inner block contains the inner string from a new block { } e.g: "import yang-interpreter { prefix "yani"; }"
        /// </summary>
        public string InnerBlock { get; set; } = string.Empty;

        /// <summary>
        /// Defines if the matched container type is childless. e.g.: type string;
        /// </summary>
        public bool IsChildlessContainer { get; set; } = false;
        public Token(TokenTypes _TokenType, string _TokenName,string _TokenValue, Type _TokenAsType,bool _ChildlessContainer = false)
        {
            TokenType = _TokenType;
            TokenName = _TokenName;
            TokenValue = _TokenValue;
            TokenAsType = _TokenAsType;
            IsChildlessContainer = _ChildlessContainer;
        }
        public Token(TokenTypes _TokenType, string _TokenName, string _TokenValue, Type _TokenAsType, TokenTypes _TokenAsSingleLine, bool _ChildlessContainer = false) :  this(_TokenType,_TokenName,_TokenValue,_TokenAsType, _ChildlessContainer)
        { 
            TokenAsSingleLine = _TokenAsSingleLine;
        }
    }
}
