using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.BaseStatements
{
    /// <summary>
    /// Base class for any statement that can have substatements and needs to validate its value. 
    /// </summary>
    public abstract class ControlledValueStatement : StatementBase
    {
        public ControlledValueStatement(string Name) : base(Name) { }
        public ControlledValueStatement(string Name, string Value) : base(Name) { this.Value = Value; }
        protected abstract string ImproperValueErrorMessage { get; }
        public override string Value
        {
            get => base.Value;
            set
            {
                if (IsValidValue(value))
                    base.Value = value;
                else
                {
                    base.Value = value;
                    throw new ImproperValue(ImproperValueErrorMessage);
                }
            }
        }
        protected abstract bool IsValidValue(string value);
    }
}
