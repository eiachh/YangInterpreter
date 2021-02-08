using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.Property
{
    public class Prefix : Statement
    {
        public Prefix() : base("Prefix") { }
        public Prefix(string _Value) : base("Prefix")
        {
            Value = _Value;
        }

        public override XElement[] NodeAsXML()
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            return indent + "prefix " + Value + ";";
        }
    }
}
