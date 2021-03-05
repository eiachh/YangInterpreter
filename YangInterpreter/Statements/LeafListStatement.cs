using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// Leaf-list Statement RFC 6020 7.7.
    /// 
    /// <summary>
    /// Where the "leaf" statement is used to define a simple scalar variable
    /// of a particular type, the "leaf-list" statement is used to define an
    /// array of a particular type.The "leaf-list" statement takes one
    /// argument, which is an identifier, followed by a block of
    /// substatements that holds detailed leaf-list information.
    /// </summary>
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | config       | 7.19.1  | 0..1        |
    /// | description  | 7.19.3  | 0..1        |
    /// | if-feature   | 7.18.2  | 0..n        |
    /// | max-elements | 7.7.4   | 0..1        |
    /// | min-elements | 7.7.3   | 0..1        |
    /// | must         | 7.5.3   | 0..n        |
    /// | ordered-by   | 7.7.5   | 0..1        |
    /// | reference    | 7.19.4  | 0..1        |
    /// | status       | 7.19.2  | 0..1        |
    /// | type         | 7.4     | 1           |
    /// | units        | 7.3.3   | 0..1        |
    /// | when         | 7.19.5  | 0..1        |
    /// +--------------+---------+-------------+
    /// 
    public class LeafListStatement : StatementBase
    {
        public LeafListStatement() : base("leaf-list") { }
        public LeafListStatement(string Value) : base("leaf-list",Value) { }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.LeafListStatementAllowedSubstatements;
        }
    }
}
