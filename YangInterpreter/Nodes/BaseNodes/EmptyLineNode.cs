using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace YangInterpreter.Nodes.BaseNodes
{
    public class EmptyLineNode : YangNode
    {
        public EmptyLineNode() : base("") { }
        public EmptyLineNode(string empty) : this() { }
        public override XElement[] NodeAsXML()
        {
            return new XElement[] { new XElement(" ") };
        }

        public override string NodeAsYangString(int indentationlevel)
        {
            return "";
        }
    }
}
