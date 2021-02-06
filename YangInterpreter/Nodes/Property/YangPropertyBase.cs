using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Nodes.BaseNodes;

namespace YangInterpreter.Nodes.Property
{
    public abstract class YangPropertyBase
    {
        protected string Name;
        protected string Value;

        public YangPropertyBase() { }
        protected YangPropertyBase(string _Name) { Name = _Name; }

        /// <summary>
        /// Returns the name of the property.
        /// </summary>
        /// <returns></returns>
        public virtual string GetName()
        {
            return Name;
        }

        /// <summary>
        /// Gets the value of the property.
        /// </summary>
        /// <returns></returns>
        public virtual string GetValue()
        {
            return Value;
        }

        /// <summary>
        /// Sets the value of the property.
        /// </summary>
        /// <param name="_Value"></param>
        public virtual void SetValue(string _Value)
        {
            Value = _Value;
        }
        /// <summary>
        /// Converts Name + value into string as: Name "Value";
        /// </summary>
        /// <returns></returns>
        public virtual string PropertyAsYangText()
        {
            return Name.ToLower() + " \"" + Value + "\"" + ";";
        }
        /// <summary>
        /// Converts Name + value with indentation into string as: \t Name "Value";
        /// </summary>
        /// <returns></returns>
        public virtual string PropertyAsYangText(int indentationlevel)
        {
            return GetIdentation(indentationlevel) + Name.ToLower() + " \"" + YangNode.MultilineIndentFixer(indentationlevel,Value) + "\"" + ";";
        }

        /// <summary>
        /// Returns the required amount of tabs as text.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        protected string GetIdentation(int n)
        {
            return new String('\t', n);
        }
    }
}
