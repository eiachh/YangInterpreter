using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    /// Revision Statement RFC 6020 7.19.3.
    ///
   
    /// </summary>
    public class Bit : Statement
    {
        public Bit() : base("Bit") { }
        public Bit(string Value) : base("Bit")
        {
            base.Value = Value;
        }

        public override XElement[] StatementAsXML()
        {
            return new XElement[] { new XElement("Bit", base.Value) };
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            if (GeneratedFrom == TokenTypes.DescriptionSameLineStart)
                return NameAndValueAsYangString(indentationlevel, ValueFormattingOption.SameLineStart);
            else
                return NameAndValueAsYangString(indentationlevel, ValueFormattingOption.NextLineStart);
        }

        /// <summary>
        /// No child statement is allowed for Description statement.
        /// </summary>
        /// <param name="StatementToAdd"></param>
        /// <returns></returns>
        //internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd) { return false; }
    }
}
