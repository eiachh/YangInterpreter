using System;
using System.Collections.Generic;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// Typedef Statement RFC 6020 7.3.1.
    /// 
    /// <summary>
    /// The "typedef" statement defines a new type that may be used locally
    /// in the module, in modules or submodules which include it, and by
    /// other modules that import from it, according to the rules in
    /// Section 5.5. The new type is called the "derived type", and the type
    /// from which it was derived is called the "base type". All derived
    /// types can be traced back to a YANG built-in type.
    /// </summary>
    /// +--------------+---------+-------------+
    /// | substatement | section | cardinality |
    /// +--------------+---------+-------------+
    /// | default      | 7.3.4   | 0..1        |
    /// | description  | 7.19.3  | 0..1        |
    /// | reference    | 7.19.4  | 0..1        |
    /// | status       | 7.19.2  | 0..1        |
    /// | type         | 7.3.2   | 1           |
    /// | units        | 7.3.3   | 0..1        |
    /// +--------------+---------+-------------+
    /// 

    public class TypedefStatement : StatementBase
    {
        public TypedefStatement() : base("typedef") { }
        public TypedefStatement(string Argument) : base("typedef", Argument) { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.TypedefStatementAllowedSubstatements;
        }

        public override StatementBase AddStatement(StatementBase StatementToAdd)
        {
            if (TypeStatement.CountTypes(Elements()) >= 1 && typeof(TypeStatement).IsAssignableFrom(StatementToAdd.GetType()))
                throw new ArgumentOutOfRangeException(StatementToAdd.GetType().ToString(), "Cannot add more " + StatementToAdd.GetType().ToString() + " into " + GetType().ToString() + ", maximum amount reached: 1");
            return base.AddStatement(StatementToAdd);
        }
    }
}
