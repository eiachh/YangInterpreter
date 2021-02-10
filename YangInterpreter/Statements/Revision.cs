using System;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using System.Text.RegularExpressions;
using YangInterpreter.Interpreter;
using System.Linq;

namespace YangInterpreter.Statements

{
    /// Revision Statement RFC 6020 7.1.9
    /// 
    /// <summary>
    /// The Name is a date string in the format "YYYY-MM-DD", followed by a
    /// block of substatements that holds detailed revision information.
    /// A module SHOULD have at least one initial "revision" statement.
    /// </summary>
    /// 
    ///+--------------+---------+-------------+
    ///| substatement | section | cardinality |
    ///+--------------+---------+-------------+
    ///| description  | 7.19.3  | 0..1        |
    ///| reference    | 7.19.4  | 0..1        |
    ///+--------------+---------+-------------+

    public class Revision : Statement
    {
        public Revision(string Value) : base("Revision") { this.Value = Value; }

        public override string Value
        {
            get => base.Value;
            set
            {
                if (IsValidValue(value))
                    base.Value = value;
                else
                    throw new ImproperValue("The given value for reference was not a proper date format \"YYY-MM-DD\"");
            }
        }

        public override Statement AddStatement(Statement StatementToAdd)
        {
            if(IsAddedSubstatementAllowedInCurrentStatement(StatementToAdd))
                return base.AddStatement(StatementToAdd);
            else
                throw new ImproperValue("Forbidden Statement was added to the revision node. Statement name: " + StatementToAdd.GetType().ToString() 
                                           + Environment.NewLine + "For valid arguments check RFC 6020 7.1.9.1 or the Revision class source code!");
        }

        public override XElement[] StatementAsXML()
        {
            XElement rev = new XElement("Revision", base.Value);
            foreach (var desc in Descendants())
            {
                rev.Add(desc.StatementAsXML());
            }
            return new XElement[] { rev };
        }

        public override string StatementAsYangString(int IndentationLevel)
        {
            var indent = GetIndentation(IndentationLevel);
            var stringBuilder = indent + Name + " " + Value + " {" + Environment.NewLine;
            stringBuilder += GetStatementsAsYangString(IndentationLevel + 1) + Environment.NewLine;
            stringBuilder += indent + "}";
            return stringBuilder;
        }

        /// <summary>
        /// Revision allowed to have 1 Description statement and 1 Reference statement.
        /// </summary>
        /// <param name="StatementToAdd"></param>
        /// <returns></returns>
        internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            if (!base.IsAddedSubstatementAllowedInCurrentStatement(StatementToAdd))
            {
                if (StatementToAdd.GetType() != typeof(Description) && StatementToAdd.GetType() != typeof(Reference))
                    throw new ArgumentException("Statement: " + StatementToAdd.GetType().ToString() + " cannot be added to a Revision statement.");
                if (StatementToAdd.GetType() == typeof(Description))
                {
                    var descendList = Descendants("description");
                    if (descendList?.Count() >= 1)
                        throw new ArgumentOutOfRangeException("One Description statement already exists in the current Revision statement!");
                }
                else if (StatementToAdd.GetType() == typeof(Reference))
                {
                    var descendList = Descendants("reference");
                    if (descendList?.Count() >= 1)
                        throw new ArgumentOutOfRangeException("One Reference statement already exists in the current Revision statement!");
                }
            }
            return true;
        }

        /// <summary>
        /// Revision Value has to be a valid date (of revision).
        /// </summary>
        /// <param name="_Value"></param>
        /// <returns></returns>
        private bool IsValidValue(string _Value)
        {
            Regex validator = new Regex("([0-9]{4}-[0|1][0-9]-(?:[0-2][0-9])|3[0-1])");
            return validator.Match(_Value).Success;
        }
    }
}
