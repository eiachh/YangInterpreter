using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Units Statement RFC 6020 7.3.3.
    /// 
    /// <summary>
    /// The "units" statement, which is optional, takes as an argument a
    /// string that contains a textual definition of the units associated
    /// with the type.
    /// </summary>
    public class UnitsStatement : ChildlessStatement
    {
        public UnitsStatement() : base("units") { }
        public UnitsStatement(string Argument) : base("units", Argument) { }
    }
}
