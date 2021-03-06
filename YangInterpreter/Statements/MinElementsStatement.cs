using System.Text.RegularExpressions;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Min-elements Statement RFC 6020 7.7.3. 
    /// 
    /// <summary>
    /// The "min-elements" statement, which is optional, takes as an argument
    /// a non-negative integer that puts a constraint on valid list entries.
    /// A valid leaf-list or list MUST have at least min-elements entries.
    /// If no "min-elements" statement is present, it defaults to zero.
    /// </summary>
    public class MinElementsStatement : ControlledValueChildlessStatement
    {
        public MinElementsStatement(string Argument = "0") : base("min-elements", Argument) { }

        protected override string ImproperValueErrorMessage => "Min elements`s argument can only be positive numbers or the string \"unbounded\" but it was: " + Value;

        protected override bool IsValidValue(string value)
        {
            return new Regex("^(\\+){0,1}[0-9]+$").Match(value).Success;
        }
    }
}
