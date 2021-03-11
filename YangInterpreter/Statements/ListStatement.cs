using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter
{
    /// List Statement RFC 6020 7.8.
    /// 
    /// <summary>
    /// The "list" statement is used to define an interior data node in the
    /// schema tree.A list node may exist in multiple instances in the data   
    /// tree.Each such instance is known as a list entry.The "list"
    /// statement takes one argument, which is an identifier, followed by a
    /// block of substatements that holds detailed list information.
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
    /// | key          | 7.8.2   | 0..1        |
    /// | leaf         | 7.6     | 0..n        |
    /// | leaf-list    | 7.7     | 0..n        |
    /// | list         | 7.8     | 0..n        |
    /// | max-elements | 7.7.4   | 0..1        |
    /// | min-elements | 7.7.3   | 0..1        |
    /// | must         | 7.5.3   | 0..n        |
    /// | ordered-by   | 7.7.5   | 0..1        |
    /// | reference    | 7.19.4  | 0..1        |
    /// | status       | 7.19.2  | 0..1        |
    /// | typedef      | 7.3     | 0..n        |
    /// | unique       | 7.8.3   | 0..n        |
    /// | uses         | 7.12    | 0..n        |
    /// | when         | 7.19.5  | 0..1        |
    /// +--------------+---------+-------------+
    /// 
    public class ListStatement : StatementBase
    {
        public ListStatement() : base("list") { }
        public ListStatement(string Argument) : base("list", Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.ListStatementAllowedSubstatements;
        }
    }
}
