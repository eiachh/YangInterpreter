using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using YangInterpreter.Interpreter;
using YangInterpreter.Statements.BaseStatements;

namespace YangInterpreter.Statements
{ 
    public class Reference : StatementWithSingleValueBase
    {
        public Reference() : base("Reference") { }
        public Reference(string Value) : this() { base.Value = Value; }
        internal override bool IsValueStartAtSameLine()
        {
            return GeneratedFrom == TokenTypes.ReferenceSameLineStart;
        }
    }
}
