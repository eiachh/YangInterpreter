using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace YangInterpreter.Statements.BaseStatements
{
    public class EmptyLineNode : Statement
    {
        public EmptyLineNode() : base("") { }
        public EmptyLineNode(string empty) : this() { }
        public override XElement[] NodeAsXML()
        {
            return new XElement[] { new XElement(" ") };
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            return "";
        }

        internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            return true;
        }
    }
}
