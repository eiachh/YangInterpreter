using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using System.Text.RegularExpressions;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements

{
    /// Revision Statement RFC 6020 7.1.9
    /// 
    /// The Name is a date string in the format "YYYY-MM-DD", followed by a
    /// block of substatements that holds detailed revision information.
    /// A module SHOULD have at least one initial "revision" statement.
    /// 
    ///+--------------+---------+-------------+
    ///| substatement | section | cardinality |
    ///+--------------+---------+-------------+
    ///| description  | 7.19.3  | 0..1        |
    ///| reference    | 7.19.4  | 0..1        |
    ///+--------------+---------+-------------+

    public class Revision : Statement
    {
        public Revision(string value) : base("Revision") { Value = value; }

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

        public override XElement[] NodeAsXML()
        {
            return null;
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            var indent = GetIndentation(indentationlevel);
            var stringBuilder = indent + Name + " " + Value + " {" + Environment.NewLine;
            stringBuilder += GetStatementsAsYangString(indentationlevel + 1) + Environment.NewLine;
            stringBuilder += indent + "}";
            return stringBuilder;
        }

        internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            //if (item.GetType() != typeof(Description) && item.GetType() != typeof(refere))
            return true;
        }

        private bool IsValidValue(string _Value)
        {
            Regex validator = new Regex("([0-9]{4}-[0|1][0-9]-(?:[0-2][0-9])|3[0-1])");
            return validator.Match(_Value).Success;
        }
    }
}
