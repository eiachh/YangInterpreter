using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Revision Statement RFC 6020 7.19.3.
    ///
    /// <summary>
    /// The "status" statement takes as an argument one of the strings
    /// "current", "deprecated", or "obsolete".
    /// </summary>
    public class StatusStatement : ControlledValueChildlessStatement
    {
        public StatusStatement() : base("status") { }
        public StatusStatement(string Argument) : base("status", Argument) { }

        protected override string ImproperValueErrorMessage => "The value of status can be: current, deprecated, obsolete, but it was: " + Value;


        protected override bool IsValidValue(string value)
        {
            return (value == "current" || value == "deprecated" || value == "obsolete");
        }
    }
}
