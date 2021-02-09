using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements.Property
{
    public class EnumProperty : Statement
    {
        public EnumProperty() : base("enum") { }
        public EnumProperty(string value) : base("enum") 
        {
            Value = value;
        }

        public override XElement[] NodeAsXML()
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            throw new NotImplementedException();
        }

        internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            throw new NotImplementedException();
        }
    }
}
