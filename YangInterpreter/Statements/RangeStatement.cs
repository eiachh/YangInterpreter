using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;
using System.Text.RegularExpressions;

namespace YangInterpreter.Statements
{
    /// Range Statement RFC 6020 9.2.4.
    /// 
    /// <summary>
    /// The "range" statement, which is an optional substatement to the
    /// "type" statement, takes as an argument a range expression string. It
    /// is used to restrict integer and decimal built-in types, or types
    /// derived from those.
    /// </summary>
    /// 
    /// +---------------+---------+-------------+
    /// | substatement  | section | cardinality |
    /// +---------------+---------+-------------+
    /// | description   | 7.19.3  | 0..1        |
    /// | error-app-tag | 7.5.4.2 | 0..1        |
    /// | error-message | 7.5.4.1 | 0..1        |
    /// | reference     | 7.19.4  | 0..1        |
    /// +---------------+---------+-------------+
    ///
    public class RangeStatement : ControlledValueContainerStatement
    {
        public RangeStatement() : base("Range") { }
        public RangeStatement(string Value) : base("Range", Value) { }

        internal override bool IsQuotedValue => true;

        protected override string ImproperValueErrorMessage => "The given value: " + Value + " is not valid for Range statement!";

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.RangeStatementAllowedSubstatements;
        }
        protected override bool IsValidValue(string value)
        {
            value = value.Replace("\r\n", "").Replace("\n", "");
            return new Regex("^\\s*(?:(?:[0-9]+|min{1})\\.\\.(?:[0-9]+|max{1}))(?:\\s?\\|\\s?(?:(?:[0-9]+|min{1})\\.\\.(?:[0-9]+|max{1})))*\\s*$").Match(value).Success;
        }
    }
}
