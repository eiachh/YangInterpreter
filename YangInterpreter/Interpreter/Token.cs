﻿using System;
using System.Collections.Generic;
using System.Text;

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
        TypeEnum,
        TypeEmpty,
        Type,
        SimpleEnum,
        TypeMultiline,
        TypeWithRange,
        Key,

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
        public TokenTypes TokenAsSingleLine { get; set; }
        public Token(TokenTypes _TokenType, string _TokenName,string _TokenValue)
        {
            TokenType = _TokenType;
            TokenName = _TokenName;
            TokenValue = _TokenValue;
        }
        public Token(TokenTypes _TokenType, string _TokenName, string _TokenValue,TokenTypes _TokenAsSingleLine) :  this(_TokenType,_TokenName,_TokenValue)
        { 
            TokenAsSingleLine = _TokenAsSingleLine;
        }
    }
}
