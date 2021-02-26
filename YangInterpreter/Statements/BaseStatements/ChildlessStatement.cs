using System;
using System.Collections.Generic;
using System.Text;

namespace YangInterpreter.Statements.BaseStatements
{
    public abstract class ChildlessStatement : StatementBase
    {
        public ChildlessStatement(string Name) : base(Name) { }
        public ChildlessStatement(string Name, string Value) : base(Name) { base.Value = Value; }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return new Dictionary<Type, Tuple<int, int>>();
        }
    }
}
