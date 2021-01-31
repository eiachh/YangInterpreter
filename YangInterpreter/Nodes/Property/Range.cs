using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Nodes.Property;

namespace YangInterpreter.Nodes.Property
{
    public class Range : YangPropertyBase
    {
        public Range() : base("range") { }
        public override string GetValue()
        {
            return Value;
        }

        public override void SetValue(string _Value)
        {
            Value = _Value;
        }
    }
}
