using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Statements.Property;

namespace YangInterpreter.Statements.Types
{
    public class EnumTypeStatement : TypeStatement
    {
        public EnumTypeStatement() : base(BuiltInTypes.enumeration) { }
        public EnumTypeStatement(string value) : base(BuiltInTypes.enumeration) { }
        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            return indent + "type enumeration {" + Environment.NewLine + GetStatementsAsYangString(indentationlevel)
                          + Environment.NewLine + indent + "}";
        }

        public override string StatementAsYangString()
        {
            return StatementAsYangString(0);
        }

        public override XElement[] StatementAsXML()
        {
            XElement ThisAsXML = new XElement("EnumTypeStatement");
            foreach (var descend in Descendants())
            {
                foreach (var descandAsXml in descend.StatementAsXML())
                {
                    ThisAsXML.Add(descandAsXml);
                }
                
            }
            return new XElement[] { ThisAsXML };
        }
    }
}
