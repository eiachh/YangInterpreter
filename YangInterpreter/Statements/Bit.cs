using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

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
    public class Bit : StatementBase
    {
        public Bit() : base("Bit") { }
        public Bit(string Value) : base("Bit", Value) { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.BitStatementAllowedSubstatements;
        }
    }
}
