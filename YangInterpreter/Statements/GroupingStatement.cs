using System;
using System.Collections.Generic;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// Grouping Statement RFC 6020 7.11.
    /// 
    /// <summary>
    /// The "grouping" statement is used to define a reusable block of nodes,
    /// which may be used locally in the module, in modules that include it,
    /// and by other modules that import from it, according to the rules in
    /// Section 5.5. It takes one argument, which is an identifier, followed
    /// by a block of substatements that holds detailed grouping information.
    /// </summary>
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | anyxml       | 7.10    | 0..n        |
    /// | choice       | 7.9     | 0..n        |
    /// | container    | 7.5     | 0..n        |
    /// | description  | 7.19.3  | 0..1        |
    /// | grouping     | 7.11    | 0..n        |
    /// | leaf         | 7.6     | 0..n        |
    /// | leaf-list    | 7.7     | 0..n        |
    /// | list         | 7.8     | 0..n        |
    /// | reference    | 7.19.4  | 0..1        |
    /// | status       | 7.19.2  | 0..1        |
    /// | typedef      | 7.3     | 0..n        |
    /// | uses         | 7.12    | 0..n        |
    /// +--------------+---------+-------------+
    /// 

    public class GroupingStatement : StatementBase
    {
        public GroupingStatement() : base("grouping") { }
        public GroupingStatement(string Argument) : base("grouping", Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.GroupingStatementAllowedSubstatements;
        }
    }
}
