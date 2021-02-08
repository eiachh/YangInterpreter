using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements

{
    public class Revision : ContainerCapability
    {
        public Revision(string value) : base("revision") { Value = value; }

        public override XElement[] NodeAsXML()
        {
            return null;
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            var stringBuilder = indent + Name + " " + Value + " {" + Environment.NewLine;
            stringBuilder += GetStatementsAsYangString(indentationlevel + 1) + Environment.NewLine;
            stringBuilder += indent + "}";
            return stringBuilder;
        }
    }
}
