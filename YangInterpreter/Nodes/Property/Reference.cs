using System;
using System.Collections.Generic;
using System.Text;

namespace YangInterpreter.Nodes.Property
{
    public class Reference : YangPropertyBase
    {
        public Reference() : base("reference") { }
        public Reference(string value) : this() { Value = value; }
    }
}
