using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.Property
{
    public class NamespaceStatement : Statement
    {
        public NamespaceStatement() : base("Namespace") { }
        public NamespaceStatement(string _Value) : base("Namespace")
        {
            Value = _Value;
        }

        public override XElement[] NodeAsXML()
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            if (GeneratedFrom == TokenTypes.DescriptionSameLineStart)
                return NameAndValueAsYangString(indentationlevel, ValueFormattingOption.SameLineStart);
            else
                return NameAndValueAsYangString(indentationlevel, ValueFormattingOption.NextLineStart);
        }
    }
}
