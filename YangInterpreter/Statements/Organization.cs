using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.Property
{
    public class Organization : Statement
    {
        public Organization() : base("Organization") { }
        public Organization(string _Value) : base("Organization")
        {
            Value = _Value;
        }

        public override XElement[] StatementAsXML()
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

        internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            return true;
        }
    }
}
