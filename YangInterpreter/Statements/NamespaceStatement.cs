using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Statements.BaseStatements;
using YangInterpreter.Interpreter;

namespace YangInterpreter.Statements
{
    public class NamespaceStatement : StatementWithSingleValueBase
    {
        public NamespaceStatement() : base("Namespace") { }
        public NamespaceStatement(string Value) : base("Namespace") { base.Value = Value; }
        internal override bool IsValueStartAtSameLine()
        {
            return GeneratedFrom == TokenTypes.DescriptionSameLineStart;
        }
    }
}
