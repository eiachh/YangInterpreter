using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

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
    public class Description : Statement
    {
        public Description() : base("Description") { }
        public Description(string Value) : base("Description")
        {
            base.Value = Value;
        }

        public override XElement[] StatementAsXML()
        {
            return new XElement[] { new XElement("Description", base.Value) };
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            if(GeneratedFrom == TokenTypes.DescriptionSameLineStart)
                return NameAndValueAsYangString(indentationlevel,ValueFormattingOption.SameLineStart);
            else
                return NameAndValueAsYangString(indentationlevel, ValueFormattingOption.NextLineStart);
        }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return new Dictionary<Type, Tuple<int, int>>();
        }
    }
}
