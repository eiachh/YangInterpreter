using System;
using System.Collections.Generic;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Notification Statement RFC 6020 7.14.
    /// 
    /// <summary>
    /// The "notification" statement is used to define a NETCONF
    /// notification.It takes one argument, which is an identifier,
    /// followed by a block of substatements that holds detailed notification
    /// information.The "notification" statement defines a notification  
    /// node in the schema tree.
    /// </summary>
    /// 
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | anyxml       | 7.10    | 0..n        |
    /// | choice       | 7.9     | 0..n        |
    /// | container    | 7.5     | 0..n        |
    /// | description  | 7.19.3  | 0..1        |
    /// | grouping     | 7.11    | 0..n        |
    /// | if-feature   | 7.18.2  | 0..n        |
    /// | leaf         | 7.6     | 0..n        |
    /// | leaf-list    | 7.7     | 0..n        |
    /// | list         | 7.8     | 0..n        |
    /// | reference    | 7.19.4  | 0..1        |
    /// | status       | 7.19.2  | 0..1        |
    /// | typedef      | 7.3     | 0..n        |
    /// | uses         | 7.12    | 0..n        |
    /// +--------------+---------+-------------+
    /// 

    public class NotificationStatement : StatementBase
    {
        public NotificationStatement() : base("notification") { }
        public NotificationStatement(string Argument) : base("notification", Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.NotificationStatementAllowedSubstatements;
        }
    }
}
