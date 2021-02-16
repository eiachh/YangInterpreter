using System;
using System.Collections.Generic;
using System.Text;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{
    public class ValueStatement : ControlledValueChildlessContainerStatement
    {
        /// Revision Statement RFC 6020 9.6.4.2.
        ///
        /// <summary>
        /// The "value" statement, which is optional, is used to associate an
        /// integer value with the assigned name for the enum. This integer
        /// value MUST be in the range -2147483648 to 2147483647, and it MUST be
        /// unique within the enumeration type.The value is unused by YANG and
        /// the XML encoding, but is carried as a convenience to implementors.
        /// </summary>
        public ValueStatement() : base("Value") { }
        public ValueStatement(string Value) : base("Value", Value) { this.Value = Value; }

        protected override string ImproperValueErrorMessage => "The given value for Value Statement was not a number!";

        protected override bool IsValidValue(string value)
        {
            int toParseInto;
            return int.TryParse(value, out toParseInto);
        }
    }
}
