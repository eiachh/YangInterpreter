using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Mandatory Statement RFC 6020 7.6.5. 
    /// 
    /// <summary>
    /// The "mandatory" statement, which is optional, takes as an argument
    /// the string "true" or "false", and puts a constraint on valid data.
    /// If not specified, the default is "false".
    /// </summary>
    public class MandatoryStatement : ControlledValueChildlessStatement
    {
        public MandatoryStatement() : base("mandatory","false") { }
        public MandatoryStatement(string Argument) : base("mandatory", Argument) { }
        protected override string ImproperValueErrorMessage => "The given value can be true/false but was: " + Argument;

        protected override bool IsValidValue(string value)
        {
            return value == "true" || value == "false";
        }
    }
}
