using System;
using System.Collections.Generic;
using System.Text;

namespace YangInterpreter.Nodes.Property
{
    public class EnumProperty : YangPropertyBase 
    {
        public EnumProperty() : base("enum") { }
        public EnumProperty(string value) : base("enum") 
        {
            Value = value;
        }
    }
}
