using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{ 
    public class Reference : Statement
    {
        public Reference() : base("reference") { }
        public Reference(string value) : this() { Value = value; }

        public override XElement[] StatementAsXML()
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            if (GeneratedFrom == TokenTypes.ReferenceSameLineStart)
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
