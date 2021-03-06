using System.Text.RegularExpressions;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Max-elements Statement RFC 6020 7.7.4. 
    /// 
    /// <summary>
    /// The "max-elements" statement, which is optional, takes as an argument
    /// a positive integer or the string "unbounded", which puts a constraint
    /// on valid list entries.A valid leaf-list or list always has at most
    /// max-elements entries.
    /// If no "max-elements" statement is present, it defaults to
    /// "unbounded".
    /// </summary>
    public class MaxElementsStatement : ControlledValueChildlessStatement
    {
        public MaxElementsStatement(string Argument = "unbounded") : base("max-elements", Argument) { }

        protected override string ImproperValueErrorMessage => "Max elements`s argument can only be positive numbers or the string \"unbounded\" but it was: " + Value;

        protected override bool IsValidValue(string value)
        {
            if (value == "unbounded")
                return true;
            return new Regex("^(\\+){0,1}[0-9]+$").Match(value).Success;
        }
    }
}
