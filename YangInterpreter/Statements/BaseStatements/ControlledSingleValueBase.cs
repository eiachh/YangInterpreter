using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.BaseStatements
{
    /// <summary>
    /// Base class for any statement that contains only a value and that value needs validation.
    /// </summary>
    public abstract class ControlledSingleValueBase : StatementWithSingleValueBase
    {
        public ControlledSingleValueBase(string Value) : base(Value) { this.Value = Value; }
        public ControlledSingleValueBase(string Name, string Value) : base(Name) { this.Value = Value; }
        protected abstract string ImproperValueErrorMessage { get;}

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
