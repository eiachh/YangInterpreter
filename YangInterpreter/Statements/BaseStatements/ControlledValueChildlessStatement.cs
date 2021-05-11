using System;
using System.Collections.Generic;
using System.Text;

namespace YangInterpreter.Statements.BaseStatements
{
    /// <summary>
    /// Base class for any statement that needs to validate its value and needs an empty container formatting.
    /// </summary>
    public abstract class ControlledValueChildlessStatement : ControlledValueStatement
    {
        public ControlledValueChildlessStatement(string Name) : base(Name) { }
        public ControlledValueChildlessStatement(string Name, string Value) : base(Name) { base.Argument = Value; }
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return new Dictionary<Type, Tuple<int, int>>();
        }
    }
}
