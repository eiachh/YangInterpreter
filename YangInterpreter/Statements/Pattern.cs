using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// Pattern Statement RFC 6020 9.4.6. 
    /// 
    /// <summary>
    /// The "pattern" statement, which is an optional substatement to the
    /// "type" statement, takes as an argument a regular expression string,
    /// as defined in [XSD-TYPES]. It is used to restrict the built-in type
    /// "string", or types derived from "string", to values that match the
    /// pattern.
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
    public class Pattern : StatementBase
    {      
        public Pattern() : base("Pattern") { }
        public Pattern(string Value) : base("Pattern", Value) { }

        internal override bool IsQuotedValue => true;

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.PatternStatementAllowedSubstatements;
        }
    }
}
