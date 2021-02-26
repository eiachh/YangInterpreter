using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    public class Contact : ChildlessStatement
    {
        public Contact() : base("Contact") { }
        public Contact(string Value) : base("Contact", Value) { }

        internal override bool IsQuotedValue => true;
    }
}
