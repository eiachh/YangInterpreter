using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    public class NamespaceStatement : ChildlessContainerStatement
    {
        internal override bool IsQuotedValue => true;
        public NamespaceStatement() : base("Namespace") { }
        public NamespaceStatement(string Value) : base("Namespace") { base.Value = Value; }
    }
}
