using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.Types
{
    /// <summary>
    /// The union built-in type represents a value that corresponds to one of
    /// its member types.
    /// 
    /// A member type can be of any built-in or derived type, except it MUST
    /// NOT be one of the built-in types "empty" or "leafref".
    /// </summary>
    public class UnionTypeStatement : TypeStatement
    {
        public UnionTypeStatement() : base(BuiltInTypes.union) { }
        public UnionTypeStatement(string Argument) : this() { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.UnionTypeStatementAllowedSubstatements;
        }
    }
}
