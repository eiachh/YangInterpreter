using System;

namespace YangInterpreter.Interpreter
{
    public enum TokenTypes
    {
        Start,
        ValueForPreviousLineBeg,
        ValueForPreviousLineEnd,
        SameLineStart,
        ValueForPreviousLineMultiline,
        NextLineStart,
        ValueForPreviousLine,
        Skip,
        NodeEndingBracket,
        Empty
    }
    public class Token
    {
        /// <summary>
        /// The type of the current token.
        /// </summary>
        public TokenTypes TokenType { get; set; } = TokenTypes.Empty;

        /// <summary>
        /// Value of the argument in the parsed line.
        /// </summary>
        public string TokenArgument { get; set; }

        /// <summary>
        /// The given token as SingleLine token if the current one is Multiline.
        /// </summary>
        public TokenTypes TokenTypeSpecialInfo { get; set; }

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
        /// <summary>
        /// Stores the quote type of the previous line to prevent malformed multiline quotes e.g.: 'aaa \r\n bbb";
        /// </summary>
        public string MultilinePreviousQuote { get; set; }
        public Token(string _TokenValue, Type _TokenAsType,bool _ChildlessContainer = false)
        {
            TokenArgument = _TokenValue;
            TokenAsType = _TokenAsType;
            IsChildlessContainer = _ChildlessContainer;
        }
        public Token(string _TokenArgument, Type _TokenAsType, TokenTypes _TokenTypeSpecialInfo, bool _ChildlessContainer = false) :  this(_TokenArgument,_TokenAsType, _ChildlessContainer)
        { 
            TokenTypeSpecialInfo = _TokenTypeSpecialInfo;
        }
    }
}
