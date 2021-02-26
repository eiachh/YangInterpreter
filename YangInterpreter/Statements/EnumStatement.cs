using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// Enum Statement RFC 6020 7.1.9
    ///
    /// <summary>
    /// The "enum" statement, which is a substatement to the "type"
    /// statement, MUST be present if the type is "enumeration". It is
    /// repeatedly used to specify each assigned name of an enumeration type.
    /// It takes as an argument a string which is the assigned name.The
    /// string MUST NOT be empty and MUST NOT have any leading or trailing
    /// whitespace characters.The use of Unicode control codes SHOULD be
    /// avoided.

    /// </summary>
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | description  | 7.19.3  | 0..1        |
    /// | reference    | 7.19.4  | 0..1        |
    /// | status       | 7.19.2  | 0..1        |
    /// | value        | 9.6.4.2 | 0..1        |
    /// +--------------+---------+-------------+
    public class EnumStatement : StatementBase
    {
        public EnumStatement() : base("Enum") { }
        public EnumStatement(string Value) : base("Enum", Value) { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.EnumStatementAllowedSubstatements;
        }
    }
}
