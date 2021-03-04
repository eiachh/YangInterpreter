using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Leaf`s Default Statement RFC 6020 7.6.4.  OR Choice`s Default Statement RFC 6020 7.9.3. 
    /// 
    /// <summary>
    /// <para>Leaf`s Default</para>
    /// The "default" statement, which is optional, takes as an argument a
    /// string that contains a default value for the leaf.
    /// The value of the "default" statement MUST be valid according to the
    /// type specified in the leaf’s "type" statement.
    /// <para>OR</para> 
    /// <para>Choice`s Default</para>
    /// The "default" statement indicates if a case should be considered as
    /// the default if no child nodes from any of the choice’s cases exist.
    /// The argument is the identifier of the "case" statement.If the
    /// "default" statement is missing, there is no default case.
    /// </summary>
    public class DefaultStatement : ChildlessStatement
    {
        public DefaultStatement() : base("default") { }
        public DefaultStatement(string Argument) : base("default", Argument) { }
    }
}
