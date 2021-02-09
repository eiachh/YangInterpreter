using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using YangInterpreter.Statements.Property;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.BaseStatements
{
    public enum YangAddingOption
    {
        ChildAndStatusless,
        ChildIncapable,
        None
    }
    internal enum ValueFormattingOption
    {
        SameLineStart,
        NextLineStart,
    }

    public abstract class Statement
    {
        private string _value;
        internal TokenTypes GeneratedFrom;
        public string Name { get; set; }
        public virtual string Value { get => _value; set => _value = value; }
        public virtual Statement Parent { get; set; }
        public virtual Statement Root { get; set; }

        internal bool BuildIntoOutput = true;

        protected List<Statement> StatementList = new List<Statement>();
        public abstract XElement[] NodeAsXML();
        public abstract string StatementAsYangString(int indentationlevel);
        public virtual string StatementAsYangString()
        {
            return StatementAsYangString(0);
        }

        internal abstract bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd);

        /// <summary>
        /// This is here to force YangNode constructor with Name parameter.
        /// </summary>
        protected Statement() { }
        public Statement(string name) { Name = name; }

        /// <summary>
        /// Adds the given Statement as child.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual Statement AddStatement(Statement item)
        {
            item.Root = Root;
            StatementList.Add(item);
            item.Parent = this;
            return item;
        }
        /// <summary>
        /// Returns the amount of children immadiately under this node.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return StatementList.Count;
        }

        /// <summary>
        /// Returns any property of the current node if the name contains the given name.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        /*public IEnumerable<Statement> GetPropertyByName(string Name)
        {
            List<Statement> matchingProps = new List<Statement>();
            bool hasAny = false;
            foreach (var prop in StatementList)
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
        }*/

        protected static string GetIndentation(int n)
        {
            return new String('\t', n);
        }

        /*protected virtual string GetPropertyListAsYangText(int indentation)
        {
            string retVal = "";
            foreach (var prop in StatementList)
            {
                retVal += prop.StatementAsYangString(indentation) + Environment.NewLine;
            }
            retVal.TrimEnd();
            return retVal;
        }
        protected virtual void GetPropertyListAsYangText()
        {
            GetPropertyListAsYangText(0);
        }*/

        /// <summary>
        /// With the given indentationLevel fixes the indent for the given string at every NewLine symbol.
        /// </summary>
        /// <param name="indentationLevel"></param>
        /// <param name="valueToFix"></param>
        /// <returns></returns>
        internal static string MultilineIndentFixer(int indentationLevel,string valueToFix)
        {
            var indent = GetIndentation(indentationLevel);
            valueToFix = valueToFix.Replace("\t", "");
            valueToFix = Regex.Replace(valueToFix, "(?<!\r)\n", "\r\n");
            valueToFix = valueToFix.Replace("\r\n", "\r\n" + indent);
            return valueToFix;
        }


        public Statement FirstChild()
        {
            if (StatementList.Count > 0)
            {
                return StatementList[0];
            }
            else
            {
                return null;
            }
        }
        public Statement LastChild()
        {
            if (StatementList.Count > 0)
            {
                return StatementList[StatementList.Count - 1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the name of the property.
        /// </summary>
        /// <returns></returns>
        public virtual string GetName()
        {
            return Name;
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

        /// <summary>
        /// Looks for the Node with the given name in any depth any child Node.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IEnumerable<Statement> DescendantsNode(string Name)
        {
            List<Statement> MatchingElements = new List<Statement>();
            bool hasAny = false;
            foreach (var child in StatementList)
            {
                if (child.GetType().IsInstanceOfType(typeof(Statement)))
                {
                    var Descendants = child.DescendantsNode(Name);
                    if (Descendants != null)
                    {
                        hasAny = true;
                        MatchingElements.AddRange(Descendants);
                    }
                }
                if (child.Name.ToLower().Contains(Name.ToLower()))
                {
                    hasAny = true;
                    MatchingElements.Add(child);
                }
            }
            if (hasAny)
                return MatchingElements;
            else
                return null;
        }

        /// <summary>
        /// Returns the substatement nodes as string properly formatted for YANG syntax.
        /// </summary>
        /// <param name="indentationLevel"></param>
        /// <returns></returns>
        protected string GetStatementsAsYangString(int indentationLevel)
        {
            var strBuilder = "";
            foreach (var child in StatementList)
            {
                if (child.BuildIntoOutput)
                    strBuilder += child.StatementAsYangString(indentationLevel) + Environment.NewLine;
            }
            strBuilder = strBuilder.TrimEnd();
            return strBuilder;
        }

        internal virtual string NameAndValueAsYangString(int indentationlevel, ValueFormattingOption formattingOption = ValueFormattingOption.SameLineStart)
        {
            var indent = GetIndentation(indentationlevel);
            if (formattingOption == ValueFormattingOption.SameLineStart)
                return indent + Name.ToLower() + " \"" + MultilineIndentFixer(indentationlevel, Value) + "\"";
            else
                return indent + Name.ToLower() + " " + Environment.NewLine + indent + indent + "\"" + MultilineIndentFixer(indentationlevel + 1, Value) + "\";";
        }
    }
}
