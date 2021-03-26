using System;
using System.Collections.Generic;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// DeviateStatement RFC 6020 7.18.3.2. 
    /// 
    /// <summary>
    /// The "deviate" statement defines how the device’s implementation of
    /// the target node deviates from its original definition.The argument
    /// is one of the strings "not-supported", "add", "replace", or "delete".
    /// </summary>
    /// 
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | config       | 7.19.1  | 0..1        |
    /// | default      | 7.6.4   | 0..1        |
    /// | mandatory    | 7.6.5   | 0..1        |
    /// | max-elements | 7.7.4   | 0..1        |
    /// | min-elements | 7.7.3   | 0..1        |
    /// | must         | 7.5.3   | 0..n        |
    /// | type         | 7.4     | 0..1        |
    /// | unique       | 7.8.3   | 0..n        |
    /// | units        | 7.3.3   | 0..1        |
    /// +--------------+---------+-------------+
    /// 
    public class DeviateStatement : StatementBase
    {
        public DeviateStatement() : base("deviate") { }
        public DeviateStatement(string Argument) : base("deviate", Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.DeviateStatementAllowedSubstatements;
        }
    }
}
