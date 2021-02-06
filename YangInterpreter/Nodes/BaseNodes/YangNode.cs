using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using YangInterpreter.Nodes.Property;

namespace YangInterpreter.Nodes.BaseNodes
{
    public enum YangAddingOption
    {
        ChildAndStatusless,
        ChildIncapable,
        None
    }
    public abstract class YangNode
    {
        public string Name { get; set; }
        public virtual YangNode Parent { get; set; }

        internal bool BuildIntoOutput = true;

        protected List<YangPropertyBase> PropertyList = new List<YangPropertyBase>();
        public abstract XElement[] NodeAsXML();
        public abstract string NodeAsYangString(int indentationlevel);
        public virtual string NodeAsYangString()
        {
            return NodeAsYangString(0);
        }

        /// <summary>
        /// This is here to force YangNode constructor with Name parameter.
        /// </summary>
        private YangNode() { }
        public YangNode(string name) { Name = name; }


        public void AddPropertyOnTop(YangPropertyBase item)
        {
            PropertyList.Insert(0, item);
        }
        public void AddProperty(YangPropertyBase item)
        {
            PropertyList.Add(item);
        }

        /// <summary>
        /// Returns any property of the current node if the name contains the given name.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IEnumerable<YangPropertyBase> GetPropertyByName(string Name)
        {
            List<YangPropertyBase> matchingProps = new List<YangPropertyBase>();
            bool hasAny = false;
            foreach (var prop in PropertyList)
            {
                if (prop.GetName().ToLower().Contains(Name.ToLower()))
                {
                    hasAny = true;
                    matchingProps.Add(prop);
                }
            }
            if (!hasAny)
                return null;
            else
                return matchingProps;
        }

        protected static string GetIndentation(int n)
        {
            return new String('\t', n);
        }

        protected virtual string GetPropertyListAsYangText(int indentation)
        {
            string retVal = "";
            foreach (var prop in PropertyList)
            {
                retVal += prop.PropertyAsYangText(indentation) + Environment.NewLine;
            }
            retVal.TrimEnd();
            return retVal;
        }
        protected virtual void GetPropertyListAsYangText()
        {
            GetPropertyListAsYangText(0);
        }

        /// <summary>
        /// With the given indentationLevel fixes the indent for the given string at every NewLine symbol.
        /// </summary>
        /// <param name="indentationLevel"></param>
        /// <param name="valueToFix"></param>
        /// <returns></returns>
        internal static string MultilineIndentFixer(int indentationLevel,string valueToFix)
        {
            var indent = GetIndentation(indentationLevel);
            valueToFix = Regex.Replace(valueToFix, "(?<!\r)\n", "\r\n");
            valueToFix = valueToFix.Replace("\r\n", "\r\n" + indent);
            return valueToFix;
        }
    }
}
