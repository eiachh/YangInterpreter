﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using YangInterpreter.Interpreter;
using System.Linq;

namespace YangInterpreter.Statements.BaseStatements
{
    internal enum YangAddingOption
    {
        ChildAndStatusless,
        ChildIncapable,
        None
    }

    public abstract class StatementBase
    {
        private string _argument;
        internal TokenTypes GeneratedFrom;
        public string Name {  get; internal set; }

        /// <summary>
        /// Represents the Value of the Statement if it can have, otherwise null.
        /// </summary>
        public virtual string Argument { get => _argument; set => _argument = value; }
        public virtual StatementBase Parent { get; internal set; }
        public virtual StatementBase Root { get; internal set; }

        internal bool BuildIntoOutput = true;
        /// <summary>
        /// Defines if the value should be beetween quote symbols at toString()
        /// </summary>
        internal virtual bool IsQuotedValue { get; } = false;

        protected List<StatementBase> StatementList = new List<StatementBase>();

        protected StatementBase() { }
        public StatementBase(string name) { Name = name; }
        public StatementBase(string name, string argument) { Name = name; Argument = argument; }

        /// <summary>
        /// Returns the Statement as an XML example Array.
        /// </summary>
        /// <returns></returns>
        public virtual XElement[] StatementAsXML()
        {
            XElement ThisStatementAsXml = new XElement(Name, Argument );
            foreach (var desc in Elements())
            {
                ThisStatementAsXml.Add(desc.StatementAsXML());
            }
            return new XElement[] { ThisStatementAsXml };
        }

        /// <summary>
        /// The Yang statement converted into string.
        /// </summary>
        /// <param name="indentationlevel"></param>
        /// <returns></returns>
        internal virtual string StatementAsYangString(int IndentationLevel)
        {
            if (Elements().Count() > 0)
            {
                if (!IsQuotedValue)
                {
                    var indent = GetIndentation(IndentationLevel);
                    var stringBuilder = indent + Name.ToLower() + " " + Argument + " {" + Environment.NewLine;
                    stringBuilder += GetStatementsAsYangString(IndentationLevel + 1) + Environment.NewLine;
                    stringBuilder += indent + "}";
                    return stringBuilder;
                }
                else
                {
                    if (IsValueStartAtSameLine())
                    {
                        var indent = GetIndentation(IndentationLevel);
                        var stringBuilder = indent + Name.ToLower() + " \"" + MultilineIndentFixer(IndentationLevel + 1, Argument) + "\"; {" + Environment.NewLine;
                        stringBuilder += GetStatementsAsYangString(IndentationLevel + 1) + Environment.NewLine;
                        stringBuilder += indent + "}";
                        return stringBuilder;
                    }
                    else
                    {
                        var indent = GetIndentation(IndentationLevel);
                        var stringBuilder = indent + Name.ToLower() + Environment.NewLine + "\t" + indent + "\"" + MultilineIndentFixer(IndentationLevel + 1, Argument) + "\"; {" + Environment.NewLine;
                        stringBuilder += GetStatementsAsYangString(IndentationLevel + 1) + Environment.NewLine;
                        stringBuilder += indent + "}";
                        return stringBuilder;
                    }

                }
            }
            else
            {
                if (!IsQuotedValue)
                {
                    var indent = GetIndentation(IndentationLevel);
                    return indent + Name.ToLower() + " " + Argument.ToLower() + ";";
                }
                else
                {
                    if (IsValueStartAtSameLine())
                    {
                        var indent = GetIndentation(IndentationLevel);
                        return indent + Name.ToLower() + " \"" + MultilineIndentFixer(IndentationLevel + 1, Argument) + "\";";
                    }
                    else
                    {
                        var indent = GetIndentation(IndentationLevel);
                        return indent + Name.ToLower() + Environment.NewLine + "\t" + indent + "\"" + MultilineIndentFixer(IndentationLevel + 1, Argument) + "\";";
                    }
                }
            }
        }

        internal virtual string StatementAsYangString()
        {
            return StatementAsYangString(0);
        }

        /// <summary>
        /// Empty Line is allowed in every statement.
        /// </summary>
        /// <param name="StatementToAdd"></param>
        /// <returns></returns>
        internal bool IsAddedSubstatementAllowedInCurrentStatement(Dictionary<Type, Tuple<int, int>> AllowanceList, StatementBase StatementToAdd)
        {
            //Item1 is min, Item 2 is maximum amount
            Tuple<int, int> AllowedAmount;
            if (!SubStatementAllowanceCollection.IsInitialized)
                SubStatementAllowanceCollection.Init();
            AllowanceList.TryGetValue(StatementToAdd.GetType(), out AllowedAmount);
            if (AllowedAmount is null)
            {
                AllowanceList.TryGetValue(StatementToAdd.GetType().BaseType, out AllowedAmount);
                if (AllowedAmount is null)
                    AllowedAmount = new Tuple<int, int>(-2, -2);
            }

            if (AllowedAmount?.Item2 == 0)
                return false;
            else if (AllowedAmount?.Item2 == -1)
                return true;
            else
                return IsArgumentInRange(StatementToAdd, AllowedAmount.Item2);
        }


        /// <summary>
        /// True if the maximum allowed substatement > Allowed amount.
        /// </summary>
        /// <param name="StatementToAdd"></param>
        /// <param name="AllowedAmount"></param>
        /// <returns></returns>
        private bool IsArgumentInRange(StatementBase StatementToAdd, int AllowedAmount)
        {
            int AmountOfMatchingDescendants = 0;
            var ElementsList = Elements(StatementToAdd.GetType());
            if(ElementsList != null)
                AmountOfMatchingDescendants = ElementsList.Count();
            if (AmountOfMatchingDescendants < AllowedAmount)
                return true;
            if (AllowedAmount < 0)
                AllowedAmount = 0;
            throw new ArgumentOutOfRangeException(StatementToAdd.GetType().ToString(),"Cannot add more "+ StatementToAdd.GetType().ToString() + " into "+GetType().ToString() + ", maximum amount reached: " + AllowedAmount);
        }

        /// <summary>
        /// Subclasses are forced to return a list with allowed nodes and the min and max amount of them.
        /// </summary>
        /// <returns></returns>
        internal abstract Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary();

        /// <summary>
        /// Adds the given Statement as child.
        /// </summary>
        /// <param name="StatementToAdd"></param>
        /// <returns></returns>
        public virtual StatementBase AddStatement(StatementBase StatementToAdd)
        {
            if (!IsAddedSubstatementAllowedInCurrentStatement(GetAllowanceSubStatementDictionary(),StatementToAdd))
                throw new ArgumentException("The given statement \""+StatementToAdd.GetType().ToString()+ "\"cannot be added to\"" + GetType().ToString());
            StatementToAdd.Root = Root;
            StatementList.Add(StatementToAdd);
            StatementToAdd.Parent = this;
            return StatementToAdd;
        }
        /// <summary>
        /// Returns the amount of children immadiately under this node.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return StatementList.Count(element => !typeof(EmptyLineStatement).IsAssignableFrom(element.GetType()));
        }

        protected static string GetIndentation(int n)
        {
            return new String('\t', n);
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
            valueToFix = valueToFix.Replace("\t", "");
            valueToFix = Regex.Replace(valueToFix, "(?<!\r)\n", "\r\n");
            valueToFix = valueToFix.Replace("\r\n", "\r\n" + indent);
            return valueToFix;
        }


        public StatementBase FirstChild()
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
        public StatementBase LastChild()
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
        /// Same as this.StatementAsYangString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return StatementAsYangString();
        }

        /// <summary>
        /// Returns all Descendants of any level
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IEnumerable<StatementBase> Descendants()
        {
            return Descendants(false);
        }

        /// <summary>
        /// Returns all Descendants of any level emptylines as well.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        internal IEnumerable<StatementBase> Descendants(bool showEmptyLines = true)
        {
            List<StatementBase> MatchingElements = new List<StatementBase>();
            foreach (var child in StatementList)
            {
                var descendants = child.Descendants();
                MatchingElements.AddRange(descendants);
                if(showEmptyLines)
                    MatchingElements.Add(child);
                else
                {
                    if (!typeof(EmptyLineStatement).IsAssignableFrom(child.GetType()))
                        MatchingElements.Add(child);
                }
            }
            return MatchingElements;
        }

        /// <summary>
        /// Looks for the Node with the given name in any depth any child Node.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IEnumerable<StatementBase> Descendants(string Name)
        {
            List<StatementBase> MatchingElements = new List<StatementBase>();
            bool hasAny = false;
            foreach (var child in StatementList)
            {
                if (typeof(StatementBase).IsInstanceOfType(child))
                {
                    var Descendants = child.Descendants(Name);
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
        /// Returns direct list of children of current node with matching name.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IEnumerable<StatementBase> Elements(string Name)
        {
            List<StatementBase> MatchingElements = new List<StatementBase>();
            bool hasAny = false;
            foreach (var child in StatementList)
            {
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
        /// Returns direct list of children of current node with matching name.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IEnumerable<StatementBase> Elements()
        {
            var asdds = StatementList.Where(statement => !typeof(EmptyLineStatement).IsAssignableFrom(statement.GetType()));
            return StatementList.Where(statement => !typeof(EmptyLineStatement).IsAssignableFrom(statement.GetType()));
        }

        /// <summary>
        /// Returns direct list of children of current node with same type.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IEnumerable<StatementBase> Elements(Type type)
        {
            List<StatementBase> MatchingElements = new List<StatementBase>();
            bool hasAny = false;
            foreach (var child in StatementList)
            {
                if (child.GetType() == type)
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
        /// Finds any descendants at any depth, that matches the given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<StatementBase> Descendants(Type type)
        {
            List<StatementBase> MatchingElements = new List<StatementBase>();
            bool hasAny = false;
            foreach (var child in StatementList)
            {

                var Descendants = child.Descendants(type);
                if (Descendants != null)
                {
                    hasAny = true;
                    MatchingElements.AddRange(Descendants);
                }

                if (child.GetType() == type)
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
            foreach (var child in Elements())
            {
                if (child.BuildIntoOutput)
                    strBuilder += child.StatementAsYangString(indentationLevel) + Environment.NewLine;
            }
            strBuilder = strBuilder.TrimEnd();
            return strBuilder;
        }

        internal virtual bool IsValueStartAtSameLine()
        {
            return GeneratedFrom == TokenTypes.Empty || GeneratedFrom == TokenTypes.SameLineStart;
        }

        internal virtual string NameAndValueAsYangString(int indentationlevel, bool IsValueStartAtSameLine = true)
        {
            var indent = GetIndentation(indentationlevel);
            if (IsValueStartAtSameLine)
                return indent + Name.ToLower() + " \"" + MultilineIndentFixer(indentationlevel + 1, Argument) + "\";";
            else
                return indent + Name.ToLower() + " " + Environment.NewLine + indent + "\t" + "\"" + MultilineIndentFixer(indentationlevel + 1, Argument) + "\";";
        }

    }
}
