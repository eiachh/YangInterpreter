using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Nodes.Property;
using YangInterpreter.Nodes.BaseNodes;

namespace YangInterpreter
{
    public class Leaf : SingleItemContainer
    {
        public bool Config { get; set; }

        public Leaf(string leafname) : base(leafname)
        {
            Config = false;
        }
        public Leaf(string leafname, bool config)                          : this(leafname)              {Config = config;}

        public override XElement[] NodeAsXML()
        {
            return new XElement[] { new XElement(Name, "Example Content") };
        }

        public override string NodeAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            var strBuilder = indent + "leaf " + Name + " {" + Environment.NewLine;
            strBuilder += GetChildrenAsYangString(indentationlevel + 1);
            strBuilder += indent + "}";
            return strBuilder;
        }
    }
}
