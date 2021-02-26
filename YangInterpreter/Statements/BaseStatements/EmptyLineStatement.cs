using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace YangInterpreter.Statements.BaseStatements
{
    public class EmptyLineStatement : ChildlessStatement
    {
        public EmptyLineStatement() : base("") { }
        public EmptyLineStatement(string empty) : base("","") { }
        public override XElement[] StatementAsXML()
        {
            return new XElement[] { new XElement(" ") };
        }
        internal override string StatementAsYangString()
        {
            return "";
        }
    }
}
