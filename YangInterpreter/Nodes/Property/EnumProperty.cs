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

        /// <summary>
        /// Converts Name + value into string as: Name "Value";
        /// </summary>
        /// <returns></returns>
        public override string PropertyAsYangText()
        {
            return Name.ToLower() +" "+ Value + ";";
        }
        /// <summary>
        /// Converts Name + value with indentation into string as: \t Name "Value";
        /// </summary>
        /// <returns></returns>
        public override string PropertyAsYangText(int indentationlevel)
        {
            return GetIdentation(indentationlevel) + PropertyAsYangText();
        }
    }
}
