using System;
using System.Collections.Generic;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// Augment Statement RFC 6020 7.15. 
    /// 
    /// <summary>
    /// The "augment" statement allows a module or submodule to add to the
    /// schema tree defined in an external module, or the current module and
    /// its submodules, and to add to the nodes from a grouping in a "uses"
    /// statement.The argument is a string that identifies a node in the
    /// schema tree.This node is called the augment’s target node.The
    /// target node MUST be either a container, list, choice, case, input,
    /// output, or notification node.It is augmented with the nodes defined
    /// in the substatements that follow the "augment" statement.
    /// </summary>
    /// 
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | anyxml       | 7.10    | 0..n        |
    /// | case         | 7.9.2   | 0..n        |
    /// | choice       | 7.9     | 0..n        |
    /// | container    | 7.5     | 0..n        |
    /// | description  | 7.19.3  | 0..1        |
    /// | if-feature   | 7.18.2  | 0..n        |
    /// | leaf         | 7.6     | 0..n        |
    /// | leaf-list    | 7.7     | 0..n        |
    /// | list         | 7.8     | 0..n        |
    /// | reference    | 7.19.4  | 0..1        |
    /// | status       | 7.19.2  | 0..1        |
    /// | uses         | 7.12    | 0..n        |
    /// | when         | 7.19.5  | 0..1        |
    /// +--------------+---------+-------------+
    /// 
    public class AugmentStatement : StatementBase
    {
        public AugmentStatement() : base("augment") { }
        public AugmentStatement(string Argument) : base("augment", Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.AugmentStatementAllowedSubstatements;
        }
    }
}
