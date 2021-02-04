using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Nodes.BaseNodes;
using YangInterpreter.Nodes.Property;

namespace YangInterpreter.Nodes.Types
{
    public class EnumTypeNode : TypeNode
    {
        public int MyProperty { get; set; }
        private EnumPropertyGroup EnumPropGroup = new EnumPropertyGroup();
        public EnumTypeNode() : base(BuiltInTypes.enumeration)
        {
            PropertyList.Add(EnumPropGroup);
        }
        public EnumTypeNode(string value) : base(BuiltInTypes.enumeration) { }

        public void AddEnumProperty(EnumProperty prop)
        {
            EnumPropGroup.EnumList.Add(prop);
        }
        public override string NodeAsYangString(int indentationlevel)
        {
            return GetIndentation(indentationlevel) + "type enumeration {"+ Environment.NewLine + EnumPropGroup.PropertyAsYangText(indentationlevel);
        }

        public override string NodeAsYangString()
        {
            throw new NotImplementedException();
        }
    }
}
