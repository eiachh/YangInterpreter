using System;
using System.Text.RegularExpressions;

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
        public SearchScheme(Regex _Reg, Type _TokenAsType, bool _ChildlessContainer = false, TokenTypes _TokenAsSingleLine = TokenTypes.Empty)
        {
            Reg = _Reg;
            TokenAsType = _TokenAsType;
            IsChildlessContainer = _ChildlessContainer;
            TokenAsSingleLine = _TokenAsSingleLine;
        }

        public SearchScheme(Regex _Reg, Type _TokenAsType, TokenTypes _TokenAsSingleLine) : this(_Reg, _TokenAsType, false, _TokenAsSingleLine)
        {
            Reg = _Reg;
            TokenAsType = _TokenAsType;
            TokenAsSingleLine = _TokenAsSingleLine;
        }
    }
}
