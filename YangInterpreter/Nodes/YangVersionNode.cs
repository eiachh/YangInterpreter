using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Nodes.BaseNodes;

namespace YangInterpreter.Nodes
{
    public class YangVersionNode : YangNode
    {
        /// <summary>
        /// The yang version as string should be 1 or 1.1
        /// </summary>
        public string Value { get; set; }
        public YangVersionNode() : base("yang-version") { BuildIntoOutput = false; }
        public YangVersionNode(string value) : this() { Value = value; }

        public override XElement[] NodeAsXML()
        {
            throw new NotImplementedException();
        }

        public override string NodeAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            return indent + Name + " " + Value + ";";
        }
    }
}
