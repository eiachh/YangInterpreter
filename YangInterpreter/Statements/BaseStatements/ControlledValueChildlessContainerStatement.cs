using System;
using System.Collections.Generic;
using System.Text;

namespace YangInterpreter.Statements.BaseStatements
{
    /// <summary>
    /// Base class for any statement that needs to validate its value and needs an empty container formatting.
    /// </summary>
    public abstract class ControlledValueChildlessContainerStatement : ControlledValueContainerStatement
    {
        public ControlledValueChildlessContainerStatement(string Name) : base(Name) { }
        public ControlledValueChildlessContainerStatement(string Name, string Value) : base(Name) { base.Value = Value; }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return new Dictionary<Type, Tuple<int, int>>();
        }
    }
}
