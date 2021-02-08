using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Statements.Property;

namespace YangInterpreter.Statements.Types
{
    public class EnumTypeNode : TypeNode
    {
        public int MyProperty { get; set; }
        private EnumPropertyGroup EnumPropGroup = new EnumPropertyGroup();
        public EnumTypeNode() : base(BuiltInTypes.enumeration)
        {
            StatementList.Add(EnumPropGroup);
        }
        public EnumTypeNode(string value) : base(BuiltInTypes.enumeration) { }

        public void AddEnumProperty(EnumProperty prop)
        {
            EnumPropGroup.EnumList.Add(prop);
        }
        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            return indent + "type enumeration {"+ Environment.NewLine + EnumPropGroup.StatementAsYangString(indentationlevel + 1) 
                          + Environment.NewLine + indent + "}";
        }

        public override string StatementAsYangString()
        {
            return StatementAsYangString(0);
        }

        public override XElement[] NodeAsXML()
        {
            return EnumPropGroup.NodeAsXML();
        }
    }
}
