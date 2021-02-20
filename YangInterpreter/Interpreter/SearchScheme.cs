using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Interpreter
{
    /// <summary>
    /// This class contains the regex for the interpreter of how to read the file and other informations about the regex.
    /// </summary>
    public class SearchScheme
    {
        /// <summary>
        /// The regex that will match the selected TokenType.
        /// </summary>
        public Regex Reg { get; set; }
        /// <summary>
        /// The TokenType gives information of what row the regex will match.
        /// </summary>
        public TokenTypes TokenType { get; set; }

        /// <summary>
        /// This links to the single line version of this token which is used after solving multiline tokens.
        /// </summary>
        public TokenTypes TokenAsSingleLine { get; set; }

        /// <summary>
        /// The token as a Type.
        /// </summary>
        public Type TokenAsType{ get; set; }

        /// <summary>
        /// Defines if the matched container type is childless. e.g.: type string;
        /// </summary>
        public bool IsChildlessContainer { get; set; } = true;
        public SearchScheme(Regex _Reg, TokenTypes _TokenType, Type _TokenAsType, bool _ChildlessContainer = false)
        {
            Reg = _Reg;
            TokenType = _TokenType;
            TokenAsType = _TokenAsType;
            IsChildlessContainer = _ChildlessContainer;
        }
        public SearchScheme(Regex _Reg, TokenTypes _TokenType, Type _TokenAsType, TokenTypes _TokenAsSingleLine, bool _ChildlessContainer = false) : this(_Reg, _TokenType, _TokenAsType, _ChildlessContainer)
        {
            TokenAsSingleLine = _TokenAsSingleLine;
        }
    }
}
