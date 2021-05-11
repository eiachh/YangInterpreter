using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Require-Instance Statement RFC 6020 9.13.2. 
    /// <summary>
    /// The "require-instance" statement, which is a substatement to the
    /// "type" statement, MAY be present if the type is
    /// "instance-identifier". It takes as an argument the string "true" or
    /// "false". If this statement is not present, it defaults to "true".
    /// </summary>
    public class RequireInstanceStatement : ControlledValueChildlessStatement
    {
        public RequireInstanceStatement() : base("RequireInstance") { }
        public RequireInstanceStatement(string Argument) : base("RequireInstance") { base.Argument = Argument; }
        protected override string ImproperValueErrorMessage => "The given value can only be false/true but it was: " + Argument;

        protected override bool IsValidValue(string value)
        {
            return (value.ToLower() == "false" || value.ToLower() == "true");
        }
    }
}
