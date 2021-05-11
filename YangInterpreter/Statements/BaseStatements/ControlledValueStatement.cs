using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.BaseStatements
{
    /// <summary>
    /// Base class for any statement that can have substatements and needs to validate its value. 
    /// </summary>
    public abstract class ControlledValueStatement : StatementBase
    {
        public ControlledValueStatement(string Name) : base(Name) { }
        public ControlledValueStatement(string Name, string Value) : base(Name) { this.Argument = Value; }
        protected abstract string ImproperValueErrorMessage { get; }
        public override string Argument
        {
            get => base.Argument;
            set
            {
                if (IsValidValue(value))
                    base.Argument = value;
                else
                {
                    base.Argument = value;
                    throw new ImproperValue(ImproperValueErrorMessage);
                }
            }
        }
        protected abstract bool IsValidValue(string value);
    }
}
