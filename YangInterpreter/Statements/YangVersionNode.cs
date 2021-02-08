using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    public class YangVersionNode : Statement
    {
        public YangVersionNode() : base("yang-version") { BuildIntoOutput = false; }
        public YangVersionNode(string value) : this() { Value = value; }

        public override XElement[] NodeAsXML()
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            return indent + Name + " " + Value + ";";
        }
    }
}
