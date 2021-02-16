using System;
using System.Collections.Generic;
using System.Text;

namespace YangInterpreter.Statements.BaseStatements
{
    public abstract class ChildlessContainerStatement : ContainerStatementBase
    {
        public ChildlessContainerStatement(string Name) : base(Name) { }
        public ChildlessContainerStatement(string Name, string Value) : base(Name) { base.Value = Value; }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return new Dictionary<Type, Tuple<int, int>>();
        }
    }
}
