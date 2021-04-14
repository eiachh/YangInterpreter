using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Namespace Statement RFC 6020 7.1.3. 
    /// 
    /// <summary>
    /// The "namespace" statement defines the XML namespace that all
    /// identifiers defined by the module are qualified by, with the
    /// exception of data node identifiers defined inside a grouping(see
    /// Section 7.12 for details). The argument to the "namespace" statement
    /// is the URI of the namespace.
    /// </summary>
    public class NamespaceStatement : ChildlessStatement
    {
        internal override bool IsQuotedValue => true;
        public NamespaceStatement() : base("namespace") { }
        public NamespaceStatement(string Argument) : base("namespace", Argument) { }
    }
}
