using System;
using System.Collections.Generic;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter
{
    /// Revision Statement RFC 6020 7.1.9
    /// 
    /// <summary>
    /// The "leaf" statement is used to define a leaf node in the schema
    /// tree.It takes one argument, which is an identifier, followed by a
    /// block of substatements that holds detailed leaf information.
    /// </summary>
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | config       | 7.19.1  | 0..1        |
    /// | default      | 7.6.4   | 0..1        |
    /// | description  | 7.19.3  | 0..1        |
    /// | if-feature   | 7.18.2  | 0..n        |
    /// | mandatory    | 7.6.5   | 0..1        |
    /// | must         | 7.5.3   | 0..n        |
    /// | reference    | 7.19.4  | 0..1        |
    /// | status       | 7.19.2  | 0..1        |
    /// | type         | 7.6.3   | 1           |
    /// | units        | 7.3.3   | 0..1        |
    /// | when         | 7.19.5  | 0..1        |
    /// +--------------+---------+-------------+
    ///
    public class LeafStatement : StatementBase
    {
        public LeafStatement() : base("leaf") { }
        public LeafStatement(string Argument) : base("leaf", Argument) { }

        public override StatementBase AddStatement(StatementBase StatementToAdd)
        {
            if (TypeStatement.CountTypes(Elements()) >= 1 && typeof(TypeStatement).IsAssignableFrom(StatementToAdd.GetType()))
                throw new ArgumentOutOfRangeException(StatementToAdd.GetType().ToString(), "Cannot add more " + StatementToAdd.GetType().ToString() + " into " + GetType().ToString() + ", maximum amount reached: 1");
            return base.AddStatement(StatementToAdd);
        }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.LeafStatementAllowedSubstatements;
        }
    }
}
