using System;
using System.Collections.Generic;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    /// Revision Statement RFC 6020 7.19.3.
    ///
    /// <summary>
    /// The "status" statement takes as an argument one of the strings
    /// "current", "deprecated", or "obsolete".
    /// </summary>
    public class StatusStatement : StatementBase
    {
        public StatusStatement() : base("Status") { }
        public StatusStatement(string Value) : base("Status", Value) { this.Value = Value; }

        public override string Value 
        { 
            get => base.Value;
            set
            {
                if (IsValidStatusValue(value))
                    base.Value = value;
                else
                    throw new ImproperValue("The value of status can be: current, deprecated, obsolete, but it was: " + value);
            }
        }

        /// <summary>
        /// Returns wether the desired value is acceptable az status value or not.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsValidStatusValue(string value)
        {
            return (value == "current" || value == "deprecated" || value == "obsolete");
        }

        /// <summary>
        /// Status cannot contain any node. Implemented for empty container formatting.
        /// </summary>
        /// <returns></returns>
        internal override Dictionary<Type, Tuple<int, int>> GetAllowanceSubStatementDictionary()
        {
            return new Dictionary<Type, Tuple<int, int>>();
        }
    }
}
