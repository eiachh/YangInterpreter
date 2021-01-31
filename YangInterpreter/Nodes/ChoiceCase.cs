using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Nodes.BaseNodes;

namespace YangInterpreter.Nodes
{
    class ChoiceCase : ContainerCapability
    {
        public ChoiceCase(string name) : base(name) { }

        public override string NodeAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            var strBuilder = indent + "case " + Name + " {" + Environment.NewLine;
            strBuilder += GetChildrenAsYangString(indentationlevel + 1);
            strBuilder += indent + "}";
            return strBuilder;
        }
    }
}
