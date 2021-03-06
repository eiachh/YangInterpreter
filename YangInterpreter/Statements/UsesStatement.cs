using System;
using System.Collections.Generic;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// Uses Statement RFC 6020 7.12.
    /// 
    /// <summary>
    /// The "uses" statement is used to reference a "grouping" definition.
    /// It takes one argument, which is the name of the grouping.
    /// </summary>
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | augment      | 7.15    | 0..1        |
    /// | description  | 7.19.3  | 0..1        |
    /// | if-feature   | 7.18.2  | 0..n        |
    /// | refine       | 7.12.2  | 0..1        |
    /// | reference    | 7.19.4  | 0..1        |
    /// | status       | 7.19.2  | 0..1        |
    /// | when         | 7.19.5  | 0..1        |
    /// +--------------+---------+-------------+

    public class UsesStatement : StatementBase
    {
        public UsesStatement() : base("uses") { }
        public UsesStatement(string Argument) : base("uses", Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.UsesStatementAllowedSubstatements;
        }
    }
}
