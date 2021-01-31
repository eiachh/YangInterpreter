using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Nodes.BaseNodes;

namespace YangInterpreter.Nodes

{
    public class Revision : YangNode
    {
        public string Value { get; set; }
        public Revision(string value) : base("revision") { Value = value; }

        public override XElement[] NodeAsXML()
        {
            return null;
        }

        public override string NodeAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            var stringBuilder = indent + Name + " " + Value + " {" + Environment.NewLine;
            stringBuilder += GetPropertyListAsYangText(indentationlevel + 1) + Environment.NewLine;
            stringBuilder += indent + "}";
            return stringBuilder;
        }
    }
}
