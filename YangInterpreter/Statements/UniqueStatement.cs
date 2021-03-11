using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Unique Statement RFC 6020 7.8.3.
    /// 
    /// <summary>
    /// The "unique" statement is used to put constraints on valid list
    /// entries.It takes as an argument a string that contains a space-
    /// separated list of schema node identifiers, which MUST be given in the
    /// descendant form(see the rule "descendant-schema-nodeid" in
    /// Section 12). Each such schema node identifier MUST refer to a leaf.
    /// </summary>
    public class UniqueStatement : ChildlessStatement
    {
        public UniqueStatement() : base("unique") { }
        public UniqueStatement(string Argument) : base("unique", Argument) { }
    }
}
