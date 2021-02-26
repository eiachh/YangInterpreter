using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Revision Statement RFC 6020 7.19.3.
    ///
    /// <summary>
    /// The "description" statement takes as an argument a string that
    /// contains a human-readable textual description of this definition.
    /// The text is provided in a language (or languages) chosen by the
    /// module developer; for the sake of interoperability, it is RECOMMENDED
    /// to choose a language that is widely understood among the community of
    /// network administrators who will use the module.
    /// </summary>
    public class Description : ChildlessStatement
    {
        internal override bool IsQuotedValue => true;
        public Description() : base("Description") { }
        public Description(string Value) : base("Description", Value) { }
    }
}
