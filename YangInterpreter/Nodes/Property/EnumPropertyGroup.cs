using System;
using System.Collections.Generic;
using System.Text;

namespace YangInterpreter.Nodes.Property
{
    internal class EnumPropertyGroup : YangPropertyBase
    {
        public List<EnumProperty> EnumList = new List<EnumProperty>();

        public override string PropertyAsYangText()
        {
            string retVal = "";
            foreach (var enumItem in EnumList)
            {
                retVal += enumItem.PropertyAsYangText() + "\r\n";
            }
            return retVal;
        }

        public override string PropertyAsYangText(int indentationlevel)
        {
            string indent = GetIdentation(indentationlevel);
            string PropAsTextBasic = PropertyAsYangText();

            PropAsTextBasic = indent + PropAsTextBasic;
            return PropAsTextBasic.Replace("\r\n", "\r\n" + indent);
        }
    }
}
