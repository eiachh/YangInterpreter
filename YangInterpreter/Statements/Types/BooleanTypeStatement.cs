using System;
using System.Collections.Generic;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Types
{
    /// Boolean built-in-type RFC 6020 7.1.9
    /// 
    /// <summary>
    /// The boolean built-in type represents a boolean value.
    /// </summary>
    public class BooleanTypeStatement : TypeStatement
    {
        public BooleanTypeStatement() : base(BuiltInTypes.boolean) { }
        public BooleanTypeStatement(string Argument) : this() { }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return new Dictionary<Type, Tuple<int, int>>();
        }
    }
}
