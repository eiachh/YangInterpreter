using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// When Statement RFC 6020 7.19.5.
    /// 
    /// <summary>
    /// The "when" statement makes its parent data definition statement
    /// conditional.The node defined by the parent data definition
    /// statement is only valid when the condition specified by the "when"
    /// statement is satisfied.The statement’s argument is an XPath
    /// expression(see Section 6.4), which is used to formally specify this
    /// condition.If the XPath expression conceptually evaluates to "true"
    /// for a particular instance, then the node defined by the parent data
    /// definition statement is valid; otherwise, it is not.
    /// </summary>
    public class WhenStatement : ChildlessStatement
    {
        public WhenStatement() : base("when") { }

        public WhenStatement(string Value) : base("when", Value) { }
    }
}
