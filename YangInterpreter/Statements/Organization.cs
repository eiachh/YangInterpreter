using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

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
    public class Organization : Statement
    {
        public Organization() : base("Organization") { }
        public Organization(string Value) : base("Organization") { base.Value = Value; }

        public override XElement[] StatementAsXML()
        {
            return new XElement[] { new XElement("Organization",base.Value)};
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            if (GeneratedFrom == TokenTypes.OrganizationSameLineStart)
                return NameAndValueAsYangString(indentationlevel, ValueFormattingOption.SameLineStart);
            else
                return NameAndValueAsYangString(indentationlevel, ValueFormattingOption.NextLineStart);
        }

        /// <summary>
        /// Organization statement cannot have descendants.
        /// </summary>
        /// <param name="StatementToAdd"></param>
        /// <returns></returns>
        internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd) { return false; }
    }
}
