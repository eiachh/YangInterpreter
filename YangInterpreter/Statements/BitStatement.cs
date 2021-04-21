using System;
using System.Collections.Generic;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Bit Statement RFC 6020 9.7.4.
    ///
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | description  | 7.19.3  | 0..1        |
    /// | reference    | 7.19.4  | 0..1        |
    /// | status       | 7.19.2  | 0..1        |
    /// | position     | 9.7.4.2 | 0..1        |
    /// +--------------+---------+-------------+
    /// </summary>
    public class BitStatement : StatementBase
    {
        public BitStatement() : base("Bit") { }
        public BitStatement(string Value) : base("Bit", Value) { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.BitStatementAllowedSubstatements;
        }
    }
}
