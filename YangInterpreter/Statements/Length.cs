using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Length Statement RFC 6020 9.4.4. 
    /// 
    /// <summary>
    /// The "length" statement, which is an optional substatement to the
    /// "type" statement, takes as an argument a length expression string.
    /// It is used to restrict the built-in type "string", or types derived
    /// from "string".
    /// </summary>
    public class Length : ControlledValueContainerStatement
    {
        public Length() : base("Length") { }
        public Length(string Value) : base("Length",Value) { }

        protected override string ImproperValueErrorMessage { get => "The given value: "+ Value + " is not valid for Length statement!"; }

        protected override bool IsValidValue(string value)
        {
            value = value.Replace("\r\n", "").Replace("\n", "");
            return new Regex("^\\s*(?:(?:[0-9]+|min{1})\\.\\.(?:[0-9]+|max{1}))(?:\\s?\\|\\s?(?:(?:[0-9]+|min{1})\\.\\.(?:[0-9]+|max{1})))*\\s*$").Match(value).Success;
        }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            throw new NotImplementedException();
        }

        /*internal override bool IsValueStartAtSameLine()
        {
            return GeneratedFrom == Interpreter.TokenTypes.LengthSameLineStart;
        }*/
    }
}
