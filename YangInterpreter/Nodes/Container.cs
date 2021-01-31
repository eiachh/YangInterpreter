using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Nodes.BaseNodes;

namespace YangInterpreter.Nodes
{
    public class Container : ContainerCapability
    {
        public Container(string name) : base(name) { }

        public override string NodeAsYangString()
        {
            string retval = string.Format("container {0} {{\r\n", Name);
            foreach (var child in Children)
            {
                retval += child.NodeAsYangString(1)+"\r\n";
            }
            retval += "}";
            return retval;
        }

        public override string NodeAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            var strBuilder = indent + "container " + Name + " {" + Environment.NewLine;
            strBuilder += GetChildrenAsYangString(indentationlevel + 1);
            strBuilder += indent + "}";
            return strBuilder;
        }
    }
}
