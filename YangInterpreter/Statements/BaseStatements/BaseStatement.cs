using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using YangInterpreter.Interpreter;
using System.Linq;

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

    public abstract class BaseStatement
    {
        private string _value;
        internal TokenTypes GeneratedFrom;
        public string Name { get; set; }

        /// <summary>
        /// Represents the Value of the Statement if it can have, otherwise null.
        /// </summary>
        public virtual string Value { get => _value; set => _value = value; }
        public virtual BaseStatement Parent { get; set; }
        public virtual BaseStatement Root { get; set; }

        internal bool BuildIntoOutput = true;

        protected List<BaseStatement> StatementList = new List<BaseStatement>();

        /// <summary>
        /// Returns the Statement as an XML example Array.
        /// </summary>
        /// <returns></returns>
        public abstract XElement[] StatementAsXML();

        /// <summary>
        /// The Yang statement converted into string.
        /// </summary>
        /// <param name="indentationlevel"></param>
        /// <returns></returns>
        public abstract string StatementAsYangString(int indentationlevel);
        public virtual string StatementAsYangString()
        {
            return StatementAsYangString(0);
        }

        /// <summary>
        /// Empty Line is allowed in every statement.
        /// </summary>
        /// <param name="StatementToAdd"></param>
        /// <returns></returns>
        internal bool IsAddedSubstatementAllowedInCurrentStatement(Dictionary<Type, Tuple<int, int>> AllowanceList, BaseStatement StatementToAdd)
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
        private bool IsArgumentInRange(BaseStatement StatementToAdd, int AllowedAmount)
        {
            int AmountOfMatchingDescendants = 0;
            var DescendantList = Descendants(StatementToAdd.GetType());
            if(DescendantList != null)
                AmountOfMatchingDescendants = DescendantList.Count();
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
        /// This is here to force YangNode constructor with Name parameter.
        /// </summary>
        protected BaseStatement() { }
        public BaseStatement(string name) { Name = name; }

        /// <summary>
        /// Adds the given Statement as child.
        /// </summary>
        /// <param name="StatementToAdd"></param>
        /// <returns></returns>
        public virtual BaseStatement AddStatement(BaseStatement StatementToAdd)
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


        public BaseStatement FirstChild()
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
        public BaseStatement LastChild()
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
        /// Returns the required amount of tabs as text.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        protected string GetIdentation(int n)
        {
            return new String('\t', n);
        }

        /// <summary>
        /// Returns all Descendants of any level
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IEnumerable<BaseStatement> Descendants()
        {
            return Descendants(false);
        }

        /// <summary>
        /// Returns all Descendants of any level emptylines as well.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        internal IEnumerable<BaseStatement> Descendants(bool showEmptyLines = true)
        {
            List<BaseStatement> MatchingElements = new List<BaseStatement>();
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
        public IEnumerable<BaseStatement> Descendants(string Name)
        {
            List<BaseStatement> MatchingElements = new List<BaseStatement>();
            bool hasAny = false;
            foreach (var child in StatementList)
            {
                if (typeof(BaseStatement).IsInstanceOfType(child))
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
        public IEnumerable<BaseStatement> Elements(string Name)
        {
            List<BaseStatement> MatchingElements = new List<BaseStatement>();
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
        public IEnumerable<BaseStatement> Elements()
        {
            var asdds = StatementList.Where(statement => !typeof(EmptyLineStatement).IsAssignableFrom(statement.GetType()));
            return StatementList.Where(statement => !typeof(EmptyLineStatement).IsAssignableFrom(statement.GetType()));
        }

        /// <summary>
        /// Finds any descendants at any depth, that matches the given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<BaseStatement> Descendants(Type type)
        {
            List<BaseStatement> MatchingElements = new List<BaseStatement>();
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

        internal virtual string NameAndValueAsYangString(int indentationlevel, bool IsValueStartAtSameLine = true)
        {
            var indent = GetIndentation(indentationlevel);
            if (IsValueStartAtSameLine)
                return indent + Name.ToLower() + " \"" + MultilineIndentFixer(indentationlevel + 1, Value) + "\";";
            else
                return indent + Name.ToLower() + " " + Environment.NewLine + indent + "\t" + "\"" + MultilineIndentFixer(indentationlevel + 1, Value) + "\";";
        }

    }
}
