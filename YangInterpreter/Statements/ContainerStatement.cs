using System;
using System.Collections.Generic;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// Container Statement RFC 6020 7.5.
    /// 
    /// <summary>
    /// The "container" statement is used to define an interior data node in
    /// the schema tree.It takes one argument, which is an identifier,
    /// followed by a block of substatements that holds detailed container
    /// information.
    /// </summary>
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | anyxml       | 7.10    | 0..n        |
    /// | choice       | 7.9     | 0..n        |
    /// | config       | 7.19.1  | 0..1        |
    /// | container    | 7.5     | 0..n        |
    /// | description  | 7.19.3  | 0..1        |
    /// | grouping     | 7.11    | 0..n        |
    /// | if-feature   | 7.18.2  | 0..n        |
    /// | leaf         | 7.6     | 0..n        |
    /// | leaf-list    | 7.7     | 0..n        |
    /// | list         | 7.8     | 0..n        |
    /// | must         | 7.5.3   | 0..n        |
    /// | presence     | 7.5.5   | 0..1        |
    /// | reference    | 7.19.4  | 0..1        |
    /// | status       | 7.19.2  | 0..1        |
    /// | typedef      | 7.3     | 0..n        |
    /// | uses         | 7.12    | 0..n        |
    /// | when         | 7.19.5  | 0..1        |
    /// +--------------+---------+-------------+
    /// 
    public class ContainerStatement : StatementBase
    {
        public ContainerStatement() : base("container") { }
        public ContainerStatement(string Argument) : base("container", Argument) { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.ContainerStatementAllowedSubstatements;
        }
    }
}
