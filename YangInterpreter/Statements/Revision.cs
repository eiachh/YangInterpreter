using System;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using System.Text.RegularExpressions;
using YangInterpreter.Interpreter;
using System.Linq;
using System.Collections.Generic;

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

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.RevisionStatementAllowedSubstatements;
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
