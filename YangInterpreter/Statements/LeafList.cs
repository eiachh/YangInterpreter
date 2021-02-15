using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements;

namespace YangInterpreter
{
    public class LeafList : Leaf
    {
        public LeafList(string leafname) : base(leafname) { }

        public override XElement[] StatementAsXML()
        {
            return new XElement[] { new XElement(Name, "Example Content1"), new XElement(Name, "Example Content2") };
        }
    }
}
