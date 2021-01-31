using System;
using System.Collections.Generic;
using System.Text;

namespace YangInterpreter.Nodes.Property
{
    public class Description : YangPropertyBase
    {
        public Description() : base("Description") { }
        public Description(string _Value) : base("Description")
        {
            Value = _Value;
        }
    }
}
