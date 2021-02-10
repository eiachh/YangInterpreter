﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements.Property
{
    public class Contact : Statement
    {
        public Contact() : base("Contact") { }
        public Contact(string _Value) : base("Contact")
        {
            Value = _Value;
        }

        public override XElement[] StatementAsXML()
        {
            throw new NotImplementedException();
        }

        public override string StatementAsYangString(int indentationlevel)
        {
            if (GeneratedFrom == TokenTypes.ContactSameLineStart)
                return NameAndValueAsYangString(indentationlevel, ValueFormattingOption.SameLineStart);
            else
                return NameAndValueAsYangString(indentationlevel, ValueFormattingOption.NextLineStart);
        }

        /*internal override bool IsAddedSubstatementAllowedInCurrentStatement(Statement StatementToAdd)
        {
            throw new NotImplementedException();
        }*/
    }
}
