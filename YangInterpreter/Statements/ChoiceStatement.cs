using System;
using System.Collections.Generic;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// Choice Statement RFC 6020 7.9.
    /// 
    /// <summary>
    /// The "choice" statement defines a set of alternatives, only one of
    /// which may exist at any one time.The argument is an identifier,
    /// followed by a block of substatements that holds detailed choice
    /// information.The identifier is used to identify the choice node in
    /// the schema tree.A choice node does not exist in the data tree.
    /// </summary>
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | anyxml       | 7.10    | 0..n        |
    /// | case         | 7.9.2   | 0..n        |
    /// | config       | 7.19.1  | 0..1        |
    /// | container    | 7.5     | 0..n        |
    /// | default      | 7.9.3   | 0..1        |
    /// | description  | 7.19.3  | 0..1        |
    /// | if-feature   | 7.18.2  | 0..n        |
    /// | leaf         | 7.6     | 0..n        |
    /// | leaf-list    | 7.7     | 0..n        |
    /// | list         | 7.8     | 0..n        |
    /// | mandatory    | 7.9.4   | 0..1        |
    /// | reference    | 7.19.4  | 0..1        |
    /// | status       | 7.19.2  | 0..1        |
    /// | when         | 7.19.5  | 0..1        |
    /// +--------------+---------+-------------+
    ///

    public class ChoiceStatement : StatementBase
    {
        public ChoiceStatement() : base("choices") { }
        public ChoiceStatement(string Argument) : base("choices", Argument) { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.ChoiceStatementAllowedSubstatements;
        }
    }
}
