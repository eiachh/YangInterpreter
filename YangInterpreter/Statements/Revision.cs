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

    public class Revision : ControlledValueStatement
    {
        protected override string ImproperValueErrorMessage => "The given value for reference was not a proper date format \"YYY-MM-DD\"";

        public Revision(string Value) : base("Revision") { this.Value = Value; }

        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return SubStatementAllowanceCollection.RevisionStatementAllowedSubstatements;
        }

        /// <summary>
        /// Revision Value has to be a valid date (of revision).
        /// </summary>
        /// <param name="_Value"></param>
        /// <returns></returns>
        protected override bool IsValidValue(string _Value)
        {
            Regex validator = new Regex("([0-9]{4}-[0|1][0-9]-(?:[0-2][0-9])|3[0-1])");
            return validator.Match(_Value).Success;
        }
    }
}
