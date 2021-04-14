using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Organization statement RFC 6020  7.1.7.
    ///
    /// <summary>
    /// The "organization" statement defines the party responsible for this
    /// module.The argument is a string that is used to specify a textual
    /// description of the organization(s) under whose auspices this module
    /// was developed.
    ///</summary>
    ///
    public class OrganizationStatement : ChildlessStatement
    {
        public OrganizationStatement() : base("organization") { }
        public OrganizationStatement(string Argument) : base("organization", Argument) { }
    }
}
