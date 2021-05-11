using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Yin-Element Statement RFC 6020 7.17.2.2. 
    /// 
    /// <summary>
    /// The "yin-element" statement, which is optional, takes as an argument
    /// the string "true" or "false". This statement indicates if the
    /// argument is mapped to an XML element in YIN or to an XML attribute
    /// (see Section 11).
    /// </summary>
    public class YinElementStatement : ControlledValueChildlessStatement
    {
        public YinElementStatement() : base("yin-element") { }
        public YinElementStatement(string Argument) : base("yin-element", Argument) { }
        protected override string ImproperValueErrorMessage => "The given value can be true/false but was: " + Argument;

        protected override bool IsValidValue(string value)
        {
            return value == "false" || value == "true";
        }
    }
}
