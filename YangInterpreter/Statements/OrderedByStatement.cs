using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Ordered-by Statement RFC 6020 7.7.5.
    /// 
    /// <summary>
    /// The "ordered-by" statement defines whether the order of entries
    /// within a list are determined by the user or the system.The argument
    /// is one of the strings "system" or "user". If not present, order
    /// defaults to "system".
    /// </summary>
    /// 
    public class OrderedByStatement : ControlledValueChildlessStatement
    {
        public OrderedByStatement(string Argument = "system") : base("ordered-by", Argument) { }
        protected override string ImproperValueErrorMessage => "The ordered-by statement can only have the values \"system, user\" but was: " + Argument;

        protected override bool IsValidValue(string value)
        {
            return value == "system" || value == "user";
        }
    }
}
